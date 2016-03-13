using kontorsprylar.ViewModels;
using Microsoft.AspNet.Http;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;

namespace kontorsprylar.Models
{
    public class DataManager
    {
        //static List<ShoppingCartVM> kundvagn = new List<ShoppingCartVM>();
        private StoredDbContext context;

        public DataManager(StoredDbContext context)
        {
            this.context = context;
        }

        //Används av produkter som visas på första sidan
        public ProductViewModel[] GetProductPresentationData()
        {
            //Här kanske vi ska tänka att produkter har en frontpageprop för att kunna beställa med .Where
            return context.Products
                .OrderByDescending(p => p.Price)
                .Select(p => new ProductViewModel
                {
                    ImgLink = p.ImgLink,
                    ProductName = p.ProductName
                }).ToArray();
        }

        public ShopingCart DeleteFromCart(ShopingCart kundVagn,int productID)
        {
            int saveIndex = -1;
            for(int i = 0; i < kundVagn.KundVagn.Count; i++)
            {
                if (kundVagn.KundVagn[i].ProductID == productID)
                {
                    saveIndex = i;
                    break;
                }
    
            }
            if (saveIndex > -1)
            {
                kundVagn.KundVagn.RemoveAt(saveIndex);
            }
            return kundVagn;
        }

        public ShopingCart GetMyShoppingCart(ShopingCart kundVagn,int pID)
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
                kundVagn.KundVagn.Add(product);
            }
            return kundVagn;
        }

        //Lägga order och returnera ett nytt ordernummer!
        internal int PlaceTheOrder(UserLoginModel user, ShopingCart shoppingList)
        {
            var newOrder = new Order();
            newOrder.CustomerID = user.UserID;
            newOrder.DeliveryAddress = context.Users
                .Where(u => u.UserID == user.UserID)
                .Select(u => u.Address).ToString();
            newOrder.OrderTime = DateTime.Now;
            newOrder.DeliveryService = "Postpaket";
            context.Orders.Add(newOrder);
            context.SaveChanges();

            foreach (var p in shoppingList.KundVagn)
            {
                ProductsInOrder tempPID = new ProductsInOrder {
                    OrderID = newOrder.OrderID,
                    BuyPrice = p.Price,
                    OrderQuantity = p.ProductQuantity,
                    ProductID = p.ProductID };
                context.ProductsInOrders.Add(tempPID);
                context.SaveChanges();
            }
            return newOrder.OrderID;
        }

        public UserLoginModel GetUser(string eMail)
        {
            return context.Users
                .Where(u => u.Email == eMail)
                .Select(u => new UserLoginModel {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserID = u.UserID,
                    Email = u.Email,
                    Accessability = u.Accessability,
                    Password = u.Password,
                    PasswordSalt = u.PasswordSalt,
                })
                .SingleOrDefault();
        }

        public List<AdminCategoryViewModel> GetAdminCategories()
        {
            return context.Categories
                .OrderBy(c => c.CategoryID)
                .Select(c => new AdminCategoryViewModel
                {
                    ID = c.CategoryID,
                    Name = c.CategoryName,
                    TopID = c.TopCategoryID,
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
            { Products = selectedProducts, CategoryToShow = categories, Specifications = categorySpecifications };

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

        public List<CategoryMenuViewModel> GetCategoriesToList(int categoryIDtoHighlight)
        {
            // Hämta kategorier för att lägga i en lista
            var categories = context.Categories
            .Select(c => new CategoryMenuViewModel
            {
                ID = c.CategoryID,
                Name = c.CategoryName,
                TopID = c.TopCategoryID,
                IsActive = c.CategoryID == categoryIDtoHighlight ? true : false,
            })
            .ToList();

            // Skapa trädstrukturen för kategorierna (tre nivåer)
            var parentNodes = categories
                .Where(c => c.TopID == 0)
                .Select(c => new CategoryMenuViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    TopID = c.TopID,
                    IsActive = c.ID == categoryIDtoHighlight ? true : false,
                    SubCategories = categories.Where(s => s.TopID == c.ID)
                        .Select(s => new CategoryMenuViewModel
                        {
                            ID = s.ID,
                            Name = s.Name,
                            TopID = s.TopID,
                            IsActive = s.ID == categoryIDtoHighlight ? true : false,
                            SubCategories = categories.Where(s2 => s2.TopID == s.ID)
                                 .Select(s2 => new CategoryMenuViewModel
                                 {
                                     ID = s2.ID,
                                     Name = s2.Name,
                                     TopID = s2.TopID,
                                     IsActive = s2.ID == categoryIDtoHighlight ? true : false,
                                 }).ToList()
                        }).ToList()
                })
                .ToList();
            
            return parentNodes;
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

        public UserLoginModel AddCustomer(RegistrateViewModel viewModel)
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
            return new UserLoginModel { UserID = user.UserID, Accessability = user.Accessability, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Password=user.Password, PasswordSalt = user.PasswordSalt};

        }

        public void AddProduct(AddProductViewModel viewModel, string imgLink)
        {
            var product = new Product();
            product.ProductName = viewModel.ProductName;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.CampaignPrice = viewModel.CampaignPrice;
            product.StockQuantity = viewModel.StockQuantity;
            product.ImgLink = imgLink;
            product.ForSale = viewModel.ForSale;
            product.CategoryID = viewModel.CategoryID;

            context.Products.Add(product);
            context.SaveChanges();
        }

        public void UploadBlob(CloudBlobContainer container, string key, string filePath, bool deleteAfter)
        {

            //Skapa en blob-referens att skriva filen till
            CloudBlockBlob b = container.GetBlockBlobReference(key);

            using (var fs = File.OpenRead(filePath)) //, FileMode.Open, FileAccess.Read, FileShare.None)
            {
                string extension = filePath.Split('.').Last();
                b.Properties.ContentType = "image/" + extension;

                b.UploadFromStream(fs);
            }
            if (deleteAfter)
                File.Delete(filePath);
      

        }
    }
}