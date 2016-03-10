using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class ProductsInCategory
    {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
    }
}
