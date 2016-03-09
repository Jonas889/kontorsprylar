using kontorsprylar.ViewModels;
using System.Linq;
using System;

namespace kontorsprylar.Models
{
    public class DataManager
    {
        StoredDbContext context;

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

        public  GetUser()
        {
            
        }
    }
}
