using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Data;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Controllers
{
    public class SignController : Controller
    {
        // GET: Sign
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Users u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    u.CreationTime = DateTime.Now;
                    u.IsActive = true;
                    u.IsDeleted = false;

                    db.Users.Add(u);
                    db.SaveChanges();
                    TempData["basarili"] = "Başarıyla Kayıt Olundu !";
                }
                catch
                {
                    TempData["basarisiz"] = "Kayıt Olma İşlemi Başarısız";
                }
            }
            return View(u);
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Index");
            }

            HttpCookie cookie = Request.Cookies["user"];

            if (cookie != null)
            {
                string mail = cookie.Value;

                Users user = db.Users.FirstOrDefault(x => x.Mail == mail);

                if (user != null)
                {
                    Session["user"] = user;
                    return RedirectToAction("Index", "Index");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            Users user = db.Users.FirstOrDefault(x => x.Mail == model.Mail && x.Password == model.Password && x.IsActive == true);

            if (user == null)
            {
                TempData["basarisiz"] = "Kullanıcı Bulunamadı Tekrar Deneyin";
                return View(model);
            }

            Session["user"] = user;

            if (model.RememberMe == true)
            {
                HttpCookie cookie = new HttpCookie("user");

                cookie.Value = user.Mail;

                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();

            if (Request.Cookies["user"] != null)
            {
                HttpCookie cookie = new HttpCookie("user");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("SignIn", "Sign");
        }
    }
}