using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WebmarketContext;
using Neo4jClient;

namespace WMarket.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(Usuario nUser)
        {
            WebmarketDataContext context = new WebmarketDataContext();
            List<Models.Producto> listaProductos = new List<Models.Producto>();

            var prods = from pdt in context.Productos
                        where pdt.Cantidad > 0
                        select pdt;

            foreach (var prod in prods)
            {
                Models.Producto producto = new Models.Producto(prod.Id, prod.IdProveedor, prod.Nombre, prod.Descripcion, prod.Marca, prod.Precio, prod.Cantidad);
                listaProductos.Add(producto);

                producto = null;

            }

            ViewData["ListaProductos"] = listaProductos;
            ViewData["Usuario"] = nUser;
            //Tuple<List<Models.Producto>, Usuario> tuplaVista = new Tuple<List<Models.Producto>, Usuario>(listaProductos, nUser);

            return View();
        }

        public ActionResult Admin(Usuario nUser)
        {

            WebmarketDataContext context = new WebmarketDataContext();
            List<Models.Producto> listaProductos = new List<Models.Producto>();

            var prods = from p in context.Productos
                        where p.UsuarioCreador == nUser.Id
                        select p;

            foreach (var prod in prods)
            {
                Models.Producto producto = new Models.Producto(prod.Id, prod.IdProveedor, prod.Nombre, prod.Descripcion, prod.Marca, prod.Precio, prod.Cantidad);
                listaProductos.Add(producto);

                producto = null;

            }
            ViewData["ListaProductos"] = listaProductos;
            ViewData["Usuario"] = nUser;
            //Tuple<List<Models.Producto>, Usuario> tuplaVista = new Tuple<List<Models.Producto>, Usuario>(listaProductos, nUser);

            return View();
        }

        public ActionResult Detalles(Models.Producto rProd)
        {
            ViewData["ProdDetalles"] = rProd;

            return View();
        }
        
        public ActionResult DetallesProd(Models.Producto pId)
        {
            WebmarketDataContext context = new WebmarketDataContext();

            var nProd = (from p in context.Productos
                        where p.Id == pId.Id
                        select p).First();
            Models.Producto rProd = new Models.Producto(nProd.Id, nProd.IdProveedor, nProd.Nombre, nProd.Descripcion, nProd.Marca, nProd.Precio, nProd.Cantidad);
            
            return RedirectToAction("Detalles", "Shop", rProd);
        }
    }
}