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

            //Ejemplo temporal
            var pro1 = new ProCarrito();
            pro1.Id_prod = 999;
            pro1.Id_cli = 34;
            pro1.Nombre = "Laptop";
            pro1.Cantidad = 2;
            pro1.Precio = 100;
            pro1.Modo = 1;

            productos.Add(pro1);



            return View(productos);
        }

        public ActionResult create(int id_prod, int id_cli, String nombre, decimal precio,
        int cantidad, int modo)
        {
            var product = new ProCarrito();
            product.Id_prod = id_prod;
            product.Id_cli = id_cli;
            product.Nombre = nombre;
            product.Precio = precio;
            product.Cantidad = cantidad;
            product.Modo = modo;

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


        private List<ProCarrito> consultarProductos()
        {
            var productos = Session["Productos"] as List<ProCarrito>;

            if (productos == null)
            {
                productos = new List<ProCarrito>();
                Session["Productos"] = new List<ProCarrito>();
            }
            return productos;
        }

    }
}


