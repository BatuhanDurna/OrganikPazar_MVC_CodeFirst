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
    public class UnitController : Controller
    {
        // GET: ManagerPanel/Unit
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            return View(db.Units.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Units u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Units.Add(u);
                    db.SaveChanges();
                    TempData["basarili"] = "Birim ekleme başarılı";
                }
                catch
                {
                    TempData["basarisiz"] = "Birim ekleme başarısız";
                }

            }
            return View(u);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units u = db.Units.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Units u = db.Units.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            db.Units.Remove(u);
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
            Units u = db.Units.Find(id);
            if (u == null)
            {
                return RedirectToAction("Index");
            }
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Units u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["basarili"] = "Birim düzenleme işlemi başarılı";
                }
                catch
                {
                    TempData["basarili"] = "Birim düzenleme işlemi başarılı";
                }
            }
            return View(u);
        }

        public ActionResult BeActive(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Units u = db.Units.Find(id);

            if (u != null)
            {
                u.IsActive = true;
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

            Units u = db.Units.Find(id);

            if (u != null)
            {
                u.IsActive = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}