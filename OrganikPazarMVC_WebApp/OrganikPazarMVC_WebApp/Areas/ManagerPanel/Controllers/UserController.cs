using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Filters;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthenticationFilter]
    public class UserController : Controller
    {
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        // GET: ManagerPanel/User
        public ActionResult Index()
        {
            List<Users> user = db.Users.Where(x => x.IsDeleted == false).ToList();
            return View(user);
        }
        public ActionResult AllIndex()
        {
            List<Users> user = db.Users.Where(x => x.IsDeleted == true).ToList();
            return View(user);
        }

        //Burada Bir Hata Var Ama ne olduğunu anlamadım ?
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Users u = db.Users.Find(id);

            if (u != null)
            {
                u.IsDeleted = true;
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

            Users u = db.Users.Find(id);

            if (u != null)
            {
                u.IsDeleted = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult BeActive(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Users u = db.Users.Find(id);

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

            Users u = db.Users.Find(id);

            if (u != null)
            {
                u.IsActive = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "User");
            }

            Users u = db.Users.Find(id);

            if (u == null)
            {
                return RedirectToAction("Index", "User");
            }

            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(Users model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Users user = db.Users.Find(model.ID);

                    if (user == null)
                    {
                        return RedirectToAction("Index");
                    }

                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Mail = model.Mail;
                    user.Password = model.Password;

                    db.SaveChanges();

                    TempData["basarili"] = "Kullanıcı güncelleme başarılı";
                }
                catch 
                {
                    TempData["basarisiz"] = "Kullanıcı güncelleme başarısız";
                }
                
            }
            return View(model);
        }
    }
}