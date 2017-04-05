using WMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMarket.Controllers
{
    public class ControladorCar : Controller
    {
        public ActionResult Index()
        {
            var ProductosCar = consultarProductos();

            return View(ProductosCar); 
        }

        public ActionResult Create()
        {
            return View(new CarritoCompras()); 
        }

        public ActionResult Create(CarritoCompras producto)
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


        private List<CarritoCompras> consultarProductos()
        {
            var ProductosCar = Session ["Productos"] as List<CarritoCompras>;
            
            if (ProductosCar == null)
            {
                ProductosCar = new List<CarritoCompras>();
                Session["Productos"] = new List<CarritoCompras>();
            }
            return ProductosCar;
        }
    
    }
}