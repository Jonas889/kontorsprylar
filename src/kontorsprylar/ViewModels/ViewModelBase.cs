using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public abstract class ViewModelBase
    {
        public List<CategoryMenuViewModel> categories { get; set; }
    }
}
