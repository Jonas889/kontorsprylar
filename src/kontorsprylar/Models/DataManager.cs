using kontorsprylar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCrypto;

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

        public List<ProductsInCategoryViewModel> GetProductsInCategory()
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

            var products = context.Products
                .OrderBy(p => p.ProductID)
                .Select(p => new ProductsInCategoryViewModel
                {
                    ID = p.ProductID,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CampaignPrice = p.CampaignPrice,
                    StockQuantity = p.StockQuantity,
                    PictureSrc = p.ImgLink,
                    DiscountPercentage = (1 - (p.CampaignPrice / p.Price)),
                    ForSale = p.ForSale,
                    //Categories = categoriesVM.Where(c => (context.ProductsInCategory.Where(m => m.ProductID == p.ProductID).Select(m => m.CategoryID).ToList().Contains(c.ID))),

                    //Specifications = context.Specifications.Where(s => s.CategoryID)
        }).ToList();

            return products;
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