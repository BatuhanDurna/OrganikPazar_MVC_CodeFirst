using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Data;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            var model = db.Produces.Where(x => x.IsDeleted == false && x.IsActive == true)
                .Select(x => new ProduceUserViewModel
                {
                    Produce_ID = x.ID,
                    ProduceName = x.Name,
                    Price = x.Price,
                    ImagePath = x.ImagePath,

                    Category_ID = x.Category_ID,
                    CategoryName = x.category.CategoryName,
                    category = x.category,

                    User_ID = x.User_ID,
                    CreationTime = x.user != null ? x.user.CreationTime : DateTime.MinValue
                }).ToList();

            return View(model);
        }

        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            List<Produces> produceList = db.Produces.Where(x => x.IsActive == true && x.IsDeleted == false && x.Category_ID == id).ToList();

            return View(produceList);
        }

    }
}