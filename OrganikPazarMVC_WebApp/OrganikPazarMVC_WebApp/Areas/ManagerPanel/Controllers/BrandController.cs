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
    public class BrandController : Controller
    {
        // GET: ManagerPanel/Brand
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brands brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    TempData["basarili"] = "Marka ekleme başarılı";
                }
                catch
                {
                    TempData["basarisiz"] = "Marka ekleme başarısız";
                }

            }
            return View(brand);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Brands brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            db.Brands.Remove(brand);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Brands brand = db.Brands.Find(id);
            if (brand == null)
            {
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brands brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(brand).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["basarili"] = "Marka düzenleme işlemi başarılı";
                }
                catch
                {
                    TempData["basarili"] = "Marka düzenleme işlemi başarılı";
                }
            }
            return View(brand);
        }

        public ActionResult BeActive(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Brands b = db.Brands.Find(id);

            if (b != null)
            {
                b.IsActive = true;
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

            Brands b = db.Brands.Find(id);

            if (b != null)
            {
                b.IsActive = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}