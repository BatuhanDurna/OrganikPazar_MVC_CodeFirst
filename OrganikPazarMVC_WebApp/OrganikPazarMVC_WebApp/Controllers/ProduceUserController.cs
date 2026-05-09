using Newtonsoft.Json;
using OrganikPazar_Odev.Models;
using OrganikPazarMVC_WebApp.Data;
using OrganikPazarMVC_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace OrganikPazarMVC_WebApp.Controllers
{
    public class ProduceUserController : Controller
    {
        // GET: ProduceUser
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Cart()
        {
            List<CartItem> Items = new List<CartItem>();
            if (Request.Cookies["cart"] != null)
            {
                HttpCookie cookie = Request.Cookies["cart"];
                Items = JsonConvert.DeserializeObject<List<CartItem>>(cookie.Value);
            }
            return View(Items);
        }
        public ActionResult AddToCard(int id)
        {
            Produces p = db.Produces.Find(id);

            if (p == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (Request.Cookies["cart"] != null)
            {
                HttpCookie cookie = Request.Cookies["cart"];
                List<CartItem> item = new List<CartItem>();
                if (cookie.Value != null)
                {
                    item = JsonConvert.DeserializeObject<List<CartItem>>(cookie.Value);
                }
                int count = item.Where(x => x.ID == id).Count();
                if (count > 0)
                {
                    item.First(x => x.ID == id).Quantity += 1;
                }
                else
                {
                    item.Add(new CartItem() { ID = p.ID, Name = p.Name, Price = p.Price, Quantity = 1 });
                }
                var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii };
                string jsoncart = JsonConvert.SerializeObject(item, Formatting.None, settings);
                cookie.Value = jsoncart;
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = new HttpCookie("cart");
                List<CartItem> Items = new List<CartItem>();
                Items.Add(new CartItem() { ID = p.ID, Name = p.Name, Price = p.Price, Quantity = 1 });
                var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii };
                string jsoncart = JsonConvert.SerializeObject(Items, Formatting.None, settings);
                cookie.Value = jsoncart;
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Index");
        }

        public ActionResult ClearCart()
        {
            if (Request.Cookies["cart"] != null)
            {
                Response.Cookies.Remove("cart");
                HttpCookie cookie = new HttpCookie("cart");
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Cart");
        }
        public ActionResult Increase(int id)
        {
            if (Request.Cookies["cart"] != null)
            {
                HttpCookie cookie = Request.Cookies["cart"];
                List<CartItem> items = new List<CartItem>();
                if (cookie.Value != null)
                {
                    items = JsonConvert.DeserializeObject<List<CartItem>>(cookie.Value);
                }
                int count = items.Where(x => x.ID == id).Count();
                if (count > 0)
                {
                    items.First(x => x.ID == id).Quantity += 1;
                }

                var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii };
                string jsoncart = JsonConvert.SerializeObject(items, Formatting.None, settings);
                cookie.Value = jsoncart;
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);

            }
            return RedirectToAction("Cart", "ProduceUser");
        }

        public ActionResult Decrease(int id)
        {
            if (Request.Cookies["cart"] != null)
            {
                HttpCookie cookie = Request.Cookies["cart"];
                List<CartItem> items = new List<CartItem>();
                if (cookie.Value != null)
                {
                    items = JsonConvert.DeserializeObject<List<CartItem>>(cookie.Value);
                }
                int count = items.Where(x => x.ID == id).Count();
                if (count > 0)
                {
                    CartItem ci = items.First(x => x.ID == id);
                    if (ci.Quantity == 1)
                    {
                        items.Remove(ci);
                    }
                    else
                    {
                        items.First(x => x.ID == id).Quantity -= 1;
                    }
                }

                var settings = new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii };
                string jsoncart = JsonConvert.SerializeObject(items, Formatting.None, settings);
                cookie.Value = jsoncart;
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);

            }
            return RedirectToAction("Cart", "ProduceUser");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string[] date = model.Expiry.Split('/');
                List<CartItem> Items = new List<CartItem>();
                if (Request.Cookies["cart"] != null)
                {
                    HttpCookie cookie = Request.Cookies["cart"];
                    Items = JsonConvert.DeserializeObject<List<CartItem>>(cookie.Value);
                }

                decimal price = Items.Sum(x => x.Price * x.Quantity);
                string priceStr = price.ToString().Replace(",", ".");
                string apiurl = "https://localhost:44309/API/PayAPI?merchandID=123456&merchandPassword=3366&price=" + priceStr + "&CardNumber=" + model.Cardnumber + "&Cvv=" + model.CVV + "&month=" + date[0] + "&year=" + date[1]; 
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiurl).Result;
                var strinResp = response.Content.ReadAsStringAsync();

                if (strinResp.Result == "\"500\"" || strinResp.Result == "\"501\"" || strinResp.Result == "\"502\"" || strinResp.Result == "\"503\"" || strinResp.Result == "\"504\"" || strinResp.Result == "\"505\"")
                {
                    ViewBag.durum = "Ödeme sisteminde geçici bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
                }
                else if (strinResp.Result == "\"200\"")
                {
                    ViewBag.durum = "Kart numarası boş olamaz";
                }
                else if (strinResp.Result == "\"201\"")
                {
                    ViewBag.durum = "CVV boş olamaz";
                }
                else if (strinResp.Result == "\"202\"")
                {
                    ViewBag.durum = "Ay boş olamaz";
                }
                else if (strinResp.Result == "\"203\"")
                {
                    ViewBag.durum = "Yıl boş olamaz";
                }
                else if (strinResp.Result == "\"300\"")
                {
                    ViewBag.durum = "Kart numarası hatalı";
                }
                else if (strinResp.Result == "\"301\"")
                {
                    ViewBag.durum = "Son kullanma tarihi hatalı";
                }
                else if (strinResp.Result == "\"302\"")
                {
                    ViewBag.durum = "CVV Hatalı";
                }
                else if (strinResp.Result == "\"777\"")
                {
                    ViewBag.durum = "Tutar Hatası";
                }
                else if (strinResp.Result == "\"888\"")
                {
                    ViewBag.durum = "Kart bakiyesi yetersiz";
                }
                else if (strinResp.Result == "\"999\"")
                {
                    //order ekle view de ekle
                    Users userId = (Users)Session["user"];

                    foreach (CartItem item in Items)
                    {

                        OrderDetails od = new OrderDetails
                        {
                            User_ID = userId.ID,
                            Produce_ID = item.ID,
                            Quantity = item.Quantity,
                            IsApprove = false,
                            TotalPrice = item.Price * item.Quantity
                        };

                        db.OrderDetails.Add(od);
                    }
                    db.SaveChanges();

                    Response.Cookies.Remove("cart");
                    HttpCookie cookie = new HttpCookie("cart");
                    cookie.Value = null;
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("PaymentSuccess");
                }                
            }
            else
            {
                ViewBag.status = "Lütfen işaretli alanları doldurun";
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult PaymentSuccess()
        {
            return View();
        }

        public void SaveOrderDetails()
        {

        }
    }
}