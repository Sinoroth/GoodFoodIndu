using Mocanu.Data;
using Mocanu.Models.LModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mocanu.Controllers
{
    public class FoodViewController : Controller
    {
        public ActionResult FoodPage()
        {
            ViewBag.Message = "";

            return View();
        }

    }
}