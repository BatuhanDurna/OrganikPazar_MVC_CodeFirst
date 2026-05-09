using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Filters;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthenticationFilter]
    public class ProduceController : Controller
    {
        // GET: ManagerPanel/Produce
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            List<Produces> p = db.Produces.Where(x => x.IsDeleted == false).ToList();
            return View(p);
        }
        public ActionResult AllIndex()
        {
            List<Produces> p = db.Produces.Where(x => x.IsDeleted == true).ToList();
            return View(p);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Produces p = db.Produces.Find(id);

            if (p != null)
            {
                p.IsDeleted = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult BackUp(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Produces p = db.Produces.Find(id);

            if (p != null)
            {
                p.IsDeleted = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.Brand_ID = new SelectList(db.Brands, "ID", "BrandName");
            ViewBag.Unit_ID = new SelectList(db.Units, "ID", "UnitName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Produces model, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (picture != null)
                    {
                        FileInfo fi = new FileInfo(picture.FileName);
                        string extension = fi.Extension;
                        string name = Guid.NewGuid().ToString();
                        string fullName = name + extension;
                        picture.SaveAs(Server.MapPath("~/Areas/ManagerPanel/Assets/ProductImage/" + fullName));
                        model.ImagePath = fullName;
                    }
                    else
                    {
                        model.ImagePath = "organikNone.png";
                    }
                    db.Produces.Add(model);
                    db.SaveChanges();
                    TempData["basarili"] = "Ürün ekleme işlemi başarılı";
                }
                catch
                {
                    TempData["basarisiz"] = "Ürün ekleme işlemi başarısız";
                }
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "CategoryName", model.Category_ID);
            ViewBag.Brand_ID = new SelectList(db.Brands, "ID", "BrandName", model.Brand_ID);
            ViewBag.Unit_ID = new SelectList(db.Units, "ID", "UnitName", model.Unit_ID);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Produce");
            }
            Produces p = db.Produces.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Produce");
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.Brand_ID = new SelectList(db.Brands, "ID", "BrandName");
            ViewBag.Unit_ID = new SelectList(db.Units, "ID", "UnitName");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(Produces model, HttpPostedFileBase picture, int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    if (picture != null)
                    {
                        FileInfo fi = new FileInfo(picture.FileName);
                        string uzanti = fi.Extension;
                        string isim = Guid.NewGuid().ToString();
                        string tamisim = isim + uzanti;
                        picture.SaveAs(Server.MapPath("~/Assets/ProductImages/" + tamisim));
                        model.ImagePath = tamisim;
                    }
                    TempData["basarili"] = "Ürün güncelleme işlemi başarılı";
                    db.SaveChanges();
                }
                catch { TempData["basarisiz"] = "Ürün güncelleme işlemi başarısız"; }
            }
            ViewBag.Category_ID = new SelectList(db.Categories, "ID", "CategoryName", model.Category_ID);
            ViewBag.Brand_ID = new SelectList(db.Brands, "ID", "BrandName", model.Brand_ID);
            ViewBag.Unit_ID = new SelectList(db.Units, "ID", "UnitName", model.Unit_ID);
            return View(model);
        }

        public ActionResult BeActive(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Produces p = db.Produces.Find(id);

            if (p != null)
            {
                p.IsActive = true;
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

            Produces p = db.Produces.Find(id);

            if (p != null)
            {
                p.IsActive = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}