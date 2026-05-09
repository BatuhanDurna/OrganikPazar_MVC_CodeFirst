using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Controllers
{
    public class CategoryUserController : Controller
    {
        // GET: CategoryUser
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _CreateMenu()
        {
            var categories = db.Categories.Where(x => x.IsActive == true).ToList();

            return View(categories);
        }
        

    }
}