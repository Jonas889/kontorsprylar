﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float CampaignPrice { get; set; }
        public string ImgLink { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public bool ForSale { get; set; }
        public int CategoryID { get; set; }
       
    }
}
