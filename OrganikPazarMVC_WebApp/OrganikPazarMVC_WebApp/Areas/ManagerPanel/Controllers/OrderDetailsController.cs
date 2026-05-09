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
    public class OrderDetailsController : Controller
    {
        // GET: ManagerPanel/OrderDetails
        OrganikPazarDBModel db = new OrganikPazarDBModel();
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Where(x => x.IsApprove == false).ToList();

            return View(orderDetails);
        }
        public ActionResult ApprovedIndex()
        {
            var orderDetails = db.OrderDetails.Where(x => x.IsApprove == true).ToList();

            return View(orderDetails);
        }
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            OrderDetails od = db.OrderDetails.Find(id);

            if (od != null)
            {
                od.IsApprove = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}