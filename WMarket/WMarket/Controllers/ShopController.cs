using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WebmarketContext;

namespace WMarket.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            WebmarketDataContext context = new WebmarketDataContext();
            List<Models.Producto> listaProductos = new List<Models.Producto>();

            var prods = from pdt in context.Productos
                        where pdt.Cantidad > 0
                        select pdt;

            foreach(var prod in prods)
            {
                Models.Producto producto = new Models.Producto(prod.Id, prod.IdProveedor, prod.Nombre, prod.Descripcion, prod.Marca, prod.Cantidad);
                listaProductos.Add(producto);

                producto = null;

            }
            
            return View(listaProductos);
        }
    }
}