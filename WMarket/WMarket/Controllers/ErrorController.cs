using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;

namespace WMarket.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(MensajeError mError)
        {
            ViewData["mError"] = mError;
            return View();
        }
        
        public ActionResult ErrorVolver (MensajeError mError)
        {
            return RedirectToAction(mError.View, mError.Controller);
        }
    }
}