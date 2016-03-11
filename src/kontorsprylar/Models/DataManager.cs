﻿using kontorsprylar.ViewModels;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kontorsprylar.Models
{
    public class DataManager
    {
        static List<ShoppingCartVM> kundvagn = new List<ShoppingCartVM> { new ShoppingCartVM { ProductName = "test", Price = 2.1f }, new ShoppingCartVM { ProductName = "test2", Price = 3f } };
        //static List<ShoppingCartVM> kundvagn = new List<ShoppingCartVM>();
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

        public List<ShoppingCartVM> GetMyShoppingCart(int pID)
        {
            if (pID != 0)
            {
                ShoppingCartVM product = context.Products
                    .Where(p => p.ProductID == pID)
                    .Select(p => new ShoppingCartVM
                    {
                        ProductName = p.ProductName,
                        Price = p.CampaignPrice > 0 ? p.CampaignPrice : p.Price,
                        ProductID = p.ProductID,
                        ProductQuantity = 1
                    }).SingleOrDefault();
                kundvagn.Add(product);
            }
            return kundvagn;
        }

        public List<ShoppingCartVM> GetMyShoppingCart()
        {
            return kundvagn;
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

        public string[] GetAdmin(string eMail)
        {
            return context.Users
                .Where(u => u.Email == eMail)
                .Select(u => new string[] { u.Password, u.PasswordSalt, u.Accessability })
                .SingleOrDefault();
        }

        // Visar en kategorisida med produkter, samt specificationer knytna till kategorin
        public ProductsInCategoryViewModel GetProductsInCategory(int categoryIDtoShow)
        {
            // Hämta alla kategorier
            List<CategoryMenuViewModel> categories = GetCategoriesToList(categoryIDtoShow);

            // Välj ut kategori baserat på ID
            var categoryFromQuery = categories
                .Where(c => c.ID == categoryIDtoShow).ToList();

            // Hämta alla specificationer
            List<Specification> specifications = GetAllSpecifications();

            // Sortera ut specs baserat på kategori
            var categorySpecifications = specifications
                .Where(s => s.CategoryID == categoryIDtoShow).ToList();

            // Hämta alla produkter och lägg till specs
            List<ProductViewModel> allProducts = GetAllProducts(specifications);

            // Sortera alla produkter på kategoriID
            var selectedProducts = allProducts
                .Where(p => p.CategoryID == categoryIDtoShow).ToList();

            ProductsInCategoryViewModel categoryToShow = new ProductsInCategoryViewModel
            { Products = selectedProducts, CategoryToShow = categoryFromQuery, Specifications = categorySpecifications };

            return categoryToShow;
        }

        private List<ProductViewModel> GetAllProducts(List<Specification> specifications)
        {
            // Hämta alla produkter från DB och lägg till specification-lista
            return context.Products
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
                    Specifications = specifications.Where(s => s.ProductID == p.ProductID).ToList()
                }).ToList();
        }

        private List<Specification> GetAllSpecifications()
        {
            // Hämta specificationer för att lägga i en lista
            return context.Specifications
                .Select(s => new Specification
                {
                    CategoryID = s.CategoryID,
                    ProductID = s.ProductID,
                    SpecKey = s.SpecKey,
                    SpecValue = s.SpecValue
                }).ToList();
        }

        private List<CategoryMenuViewModel> GetCategoriesToList(int categoryIDtoShow)
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
            return categories
            .Select(c => new CategoryMenuViewModel
            {
                ID = c.ID,
                Name = c.Name,
                TopID = c.TopID,
                IsActive = c.ID == categoryIDtoShow ? true : false,
                SubCategories = categories.Where(o => o.TopID == c.ID).ToList()
            })
            .ToList();

        }

        public ProductDetailPageVM GetProduct(int productIDtoShow)
        {
            // Hämta alla specificationer
            List<Specification> specifications = GetAllSpecifications();

            // Hämta alla produkter och lägg till specs
            List<ProductViewModel> allProducts = GetAllProducts(specifications);

            // Sortera alla produkter på productID
            var selectedProduct = allProducts
                .Where(p => p.ID == productIDtoShow).First();

            // Hämta alla kategorier
            List<CategoryMenuViewModel> categories = GetCategoriesToList(selectedProduct.CategoryID);

            ProductDetailPageVM productToShow = new ProductDetailPageVM
            { Product = selectedProduct, CategoryToShow = categories };

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
            user.Accessability = "user";
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
            context.SaveChanges();
        }
    }
   
}