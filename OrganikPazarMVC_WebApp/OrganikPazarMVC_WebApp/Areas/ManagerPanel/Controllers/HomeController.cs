using OrganikPazarMVC_WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganikPazarMVC_WebApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthenticationFilter]
    public class HomeController : Controller
    {
        // GET: ManagerPanel/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}