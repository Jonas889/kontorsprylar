using kontorsprylar.ViewModels;
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

        public void AddCustomer(RegistrateViewModel viewModel)
        {
            var user = new User();
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Address = viewModel.Street + ";" + viewModel.Zip + ";" + viewModel.City;
            user.CellPhone = viewModel.CellPhone;
            user.Phone = viewModel.Phone;
            user.Email = viewModel.Email;
            //user.Password = vad ?;
            //user.PasswordSalt = vad ?:
            user.CompanyName = viewModel.CompanyName;

            //lägg till i databasen?
        }


    }
}