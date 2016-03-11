using kontorsprylar.ViewModels;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kontorsprylar.Models
{
    public class DataManager
    {
        private StoredDbContext context;

        public DataManager(StoredDbContext context)
        {
            this.context = context;
        }

        //Används av produkter som visas på första sidan
        public ProductViewModel[] GetProductPresentationData()
        {
            //Här kanske vi sak tänka att produkter har en frontpageprop för att kunna beställa med .Where
            return context.Products
                .OrderByDescending(p => p.Price)
                .Select(p => new ProductViewModel
                {
                    ImgLink = p.ImgLink,
                    ProductName = p.ProductName
                }).ToArray();
        }

        public string[] GetUser(string eMail)
        {
            return context.Users
                .Where(u => u.Email == eMail)
                .Select(u => new string[] { u.Password, u.PasswordSalt })
                .SingleOrDefault();
        }

        public List<CategoryMenuViewModel> GetCategoryMenu()
        {
            var query = context.Categories
                .OrderBy(c => c.CategoryID)
                .Select(c => new CategoryMenuViewModel
                {
                    ID = c.CategoryID,
                    Name = c.CategoryName,
                    TopID = c.TopCategoryID,
                })
                .ToList();

            return query
                .Select(c => new CategoryMenuViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    TopID = c.TopID,
                    SubCategories = query.Where(o => o.TopID == c.ID).ToList()
                })
                .ToList();
        }

        // Visar en kategorisida med produkter, samt specificationer knytna till kategorin
        public ProductsInCategoryViewModel GetProductsInCategory(int categoryIDtoShow)
        {
            // Hämta kategorier för att lägga i en lista
            var categories = context.Categories
            .Select(c => new CategoryMenuViewModel
            {
                ID = c.CategoryID,
                Name = c.CategoryName,
                TopID = c.TopCategoryID
            })
            .ToList();
            // Skapa trädstrukturen för kategorierna
            var categoriesVM = categories
            .Select(c => new CategoryMenuViewModel
            {
                ID = c.ID,
                Name = c.Name,
                TopID = c.TopID,
                SubCategories = categories.Where(o => o.TopID == c.ID).ToList()
            })
            .ToList();
            // Välj ut kategori baserat på ID
            var categoryFromQuery = categoriesVM
                .Where(c => c.ID == categoryIDtoShow).ToList();

            // Hämta specificationer för att lägga i en lista
            var specificationsVM = context.Specifications
                .Select(s => new Specification
                {
                    CategoryID = s.CategoryID,
                    ProductID = s.ProductID,
                    SpecKey = s.SpecKey,
                    SpecValue = s.SpecValue
                }).ToList();
            // Sortera ut specs baserat på kategori
            var categorySpecifications = specificationsVM
                .Where(s => s.CategoryID == categoryIDtoShow).ToList();

            // Hämta alla produkter från DB och lägg till kategori- och specification-lista
            var allProducts = context.Products
                .OrderBy(p => p.ProductID)
                .Select(p => new ProductViewModel
                {
                    ID = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CampaignPrice = p.CampaignPrice,
                    ImgLink = p.ImgLink,
                    Description = p.Description,
                    StockQuantity = p.StockQuantity,
                    //DiscountPercentage = (1 - (p.CampaignPrice / p.Price)),
                    ForSale = p.ForSale,
                    CategoryID = p.CategoryID,
                    //Categories = categoriesVM.Where(c => (context.ProductsInCategory.Where(m => m.ProductID == p.ProductID).Select(m => m.CategoryID).ToList().Contains(c.ID))).ToList(),
                    //Categories = categoriesVM.Where(c => c.productIDs.Contains(p.ProductID)).ToList(),
                    Specifications = specificationsVM.Where(s => s.ProductID == p.ProductID).ToList()
                }).ToList();

            // Sortera alla produkter på kategoriID
            var selectedProducts = allProducts
                .Where(p => p.CategoryID == categoryIDtoShow).ToList();

            ProductsInCategoryViewModel categoryToShow = new ProductsInCategoryViewModel
            { Products = selectedProducts, CategoryToShow = categoryFromQuery, Specifications = categorySpecifications };

            return categoryToShow;
        }

        public ProductDetailPageVM GetProduct(int productIDtoShow)
        {
            // Hämta kategorier för att lägga i en lista
            var categories = context.Categories
            .Select(c => new CategoryMenuViewModel
            {
                ID = c.CategoryID,
                Name = c.CategoryName,
                TopID = c.TopCategoryID,
            })
            .ToList();
            // Skapa trädstrukturen för kategorierna och välj ut baserat på produktID
            var categoriesVM = categories
                .Select(c => new CategoryMenuViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    TopID = c.TopID,
                    SubCategories = categories.Where(o => o.TopID == c.ID).ToList()
                })
                .ToList();
            var categoryToShow = categoriesVM
                .Where(c => c.ID == (context.Products.Where(p => p.ProductID == productIDtoShow)
                .Select(p => p.CategoryID)).First())
                .ToList();

            // Hämta specificationer för att lägga i en lista
            var productSpecifications = context.Specifications
                .Where(s => s.ProductID == productIDtoShow)
                .Select(s => new Specification
                {
                    CategoryID = s.CategoryID,
                    ProductID = s.ProductID,
                    SpecKey = s.SpecKey,
                    SpecValue = s.SpecValue
                }).ToList();

            // Hämta alla produkter från DB och lägg till kategori- och specification-lista
            var productQuery = context.Products
                .OrderBy(p => p.ProductID)
                .Where(p => p.ProductID == productIDtoShow)
                .Select(p => new ProductViewModel
                {
                    ID = p.ProductID,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    //CampaignPrice = p.CampaignPrice,
                    StockQuantity = p.StockQuantity,
                    ImgLink = p.ImgLink,
                    //DiscountPercentage = (1 - (p.CampaignPrice / p.Price)),
                    ForSale = p.ForSale,
                    CategoryID = p.CategoryID,
                    //Categories = categoriesVM.Where(c => (context.ProductsInCategory.Where(m => m.ProductID == p.ProductID).Select(m => m.CategoryID).ToList().Contains(c.ID))).ToList(),
                    //Categories = categoriesVM.Where(c => c.productIDs.Contains(p.ProductID)).ToList(),
                    Specifications = productSpecifications
                }).First();

            ProductDetailPageVM productToShow = new ProductDetailPageVM
            { Product = productQuery, CategoryToShow = categoryToShow };

            return productToShow;
        }

        public void AddCustomer(RegistrateViewModel viewModel)
        {
            PBKDF2 crypt = new PBKDF2();
            var user = new User();
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Address = viewModel.Street + ";" + viewModel.Zip + ";" + viewModel.City;
            user.CellPhone = viewModel.CellPhone;
            user.Phone = viewModel.Phone;
            user.Email = viewModel.Email;
            user.Password = crypt.Compute(viewModel.Password);
            user.PasswordSalt = crypt.Salt;
            user.CompanyName = viewModel.CompanyName;
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void AddProduct(AddProductViewModel viewModel)
        {
            var product = new Product();
            product.ProductName = viewModel.ProductName;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.CampaignPrice = viewModel.CampaignPrice;
            product.StockQuantity = viewModel.StockQuantity;
            product.ImgLink = viewModel.ImgLink;
            product.ForSale = viewModel.ForSale;

            context.Products.Add(product);
        }
    }
}