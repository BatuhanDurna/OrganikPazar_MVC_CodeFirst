using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Filters;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthenticationFilter]
    public class CategoryController : Controller
    {
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        // GET: ManagerPanel/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["basarili"] = "Kategori ekleme başarılı";
                }
                catch
                {
                    TempData["basarisiz"] = "Kategori ekleme başarısız";
                }

            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["basarili"] = "Kategori düzenleme işlemi başarılı";
                }
                catch 
                {
                    TempData["basarili"] = "Kategori düzenleme işlemi başarılı";
                }
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult BeActive(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Category c = db.Categories.Find(id);

            if (c != null)
            {
                c.IsActive = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult BePassive(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Category c = db.Categories.Find(id);

            if (c != null)
            {
                c.IsActive = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}