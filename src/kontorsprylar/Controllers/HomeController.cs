using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.ViewModels;
using kontorsprylar.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class HomeController : Controller
    {
        static StoredDbContext context = new StoredDbContext();

        DataManager dataManager = new DataManager(context);
        public HomeController(StoredDbContext newContext)
        {
            context = newContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //var model = dataManager.GetProductPresentationData();
            //detta ersätter vi med datamanager osv senare
            var modelList = new List<ProductViewModel>();
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList[0].ImgLink = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            modelList[1].ImgLink = "http://www.aquaticadventures.com/silver_bank/sb_gallery/g/fin_slap_main.jpg";
            modelList[2].ImgLink = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList[3].ImgLink = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            modelList[4].ImgLink = "http://www.aquaticadventures.com/silver_bank/sb_gallery/g/fin_slap_main.jpg";
            modelList[5].ImgLink = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList[6].ImgLink = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            modelList[7].ImgLink = "http://www.aquaticadventures.com/silver_bank/sb_gallery/g/fin_slap_main.jpg";
            modelList[8].ImgLink = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";

            modelList[0].ProductName = "Namn1";
            modelList[1].ProductName = "Namn2";
            modelList[2].ProductName = "Namn3";
            modelList[3].ProductName = "Namn4";
            modelList[4].ProductName = "Namn5";
            modelList[5].ProductName = "Namn6";
            modelList[6].ProductName = "Namn7";
            modelList[7].ProductName = "Namn8";
            modelList[8].ProductName = "Namn9";

            return View(modelList);
            
        }

    }
}
