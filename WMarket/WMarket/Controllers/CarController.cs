using WMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackExchange.Redis;


namespace WMarket.Controllers
{
    public class CarController : Controller
    {
        public ActionResult Index()
        {
            var productos = consultarProductos();

            /*Ejemplo temporal
            var pro1 = new ProCarrito();
            pro1.Id_prod = 999;
            pro1.Id_cli = 34;
            pro1.Nombre = "Laptop";
            pro1.Cantidad = 2;
            pro1.Precio = 100;
            pro1.Modo = 1;

            productos.Add(pro1);*/



            return View(productos);
        }

        public ActionResult create(Producto product)
        {
            

            try
            {
                //guardar producto
                var productos = consultarProductos();
                productos.Add(product);
                Session["Productos"] = productos;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Comprar(Usuario nUser)
        {

            ViewData["ListaProductos"] = null;
            return View();
        }


        private List<Producto> consultarProductos()
        {
            var productos = Session["Productos"] as List<Producto>;

            if (productos == null)
            {
                productos = new List<Producto>();
                Session["Productos"] = new List<Producto>();
            }
            return productos;
        }

    }
}


