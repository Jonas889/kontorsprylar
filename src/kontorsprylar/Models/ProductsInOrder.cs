using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class ProductsInOrder
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public float BuyPrice { get; set; }
        public int OrderQuantity { get; set; }
        [Key]
        public int PIOID { get; set; }
    }
}
