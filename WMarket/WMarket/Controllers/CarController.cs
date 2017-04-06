using WMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMarket.Controllers
{
    public class CarController : Controller
    {
        public ActionResult Index()
        {
            var ProductosCar = consultarProductos();

            return View(ProductosCar); 
        }

        public ActionResult Create()
        {
            return View(new Carrito()); 
        }

        public ActionResult Create(Carrito producto)
        {
            try {
                //guardar producto
                var product = consultarProductos();
                product.Add(producto);
                Session["Productos"] = product;
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }


        private List<Carrito> consultarProductos()
        {
            var ProductosCar = Session ["Productos"] as List<Carrito>;
            
            if (ProductosCar == null)
            {
                ProductosCar = new List<Carrito>();
                Session["Productos"] = new List<Carrito>();
            }
            return ProductosCar;
        }
    
    }
}