﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //detta ersätter vi med datamanager osv senare
            var modelList = new List<ProductViewModel>();
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList.Add(new ProductViewModel());
            modelList[0].ImgSrc = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            modelList[1].ImgSrc = "http://www.aquaticadventures.com/silver_bank/sb_gallery/g/fin_slap_main.jpg";
            modelList[2].ImgSrc = "http://cdn07.dayviews.com/59/_u2/_u6/_u8/_u5/_u7/u268576/fs_483471291_94249_1296166959/Soluppgangen_var_iaf_hemskt_fin_sprang_ut_med_kameran_nagra_fa_minuter_innan_det_var_dags_att_aka.jpg";
            return View(modelList);
        }
    }
}