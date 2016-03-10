using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class Specification
    {   //Vad betyder Key? /Sten
       [Key]
        public int CategoryID { get; set; }
        public string SpecKey { get; set; }
        public string SpecValue { get; set; }
    }
}

