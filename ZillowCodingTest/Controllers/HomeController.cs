using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZillowCodingTest.Models;
using ZillowCodingTest.Services;

namespace ZillowCodingTest.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPropertyData(searchresultsRequest location)
        {
            if (ModelState.IsValid)
            {
                //Normally I would inject this through Unity as a singleton
                ZillowSearchClient client = new ZillowSearchClient();

                searchresults results = client.GetPropertyDetails(location.address, location.citystatezip);
                return PartialView("Results", results);
            }
            else
            {
                return PartialView("Index");
            }

        }
    }
}