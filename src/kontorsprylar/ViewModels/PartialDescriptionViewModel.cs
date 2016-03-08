using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class PartialDescriptionViewModel
    {
        public string ProductName = "TV-apparat (exkl. antennen på bild)";// { get; set; }
        public float Price = 100.00F;  // { get; set; }
        public float CampaignPrice { get; set; }
        public string ImgLink = "tv.jpg"; // { get; set; }
        public string Description = "Liten och praktisk tv. Svartvit bild. Stöd för två kanaler."; // { get; set; }
        public int StockQuantity = 56; // { get; set; }
    }
}
