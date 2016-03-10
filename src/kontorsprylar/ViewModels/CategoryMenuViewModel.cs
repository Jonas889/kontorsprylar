using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class CategoryMenuViewModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int TopID { get; set; }
        public List<CategoryMenuViewModel> SubCategories { get; set; }
        public List<int> productIDs { get; set; }
    }
}
