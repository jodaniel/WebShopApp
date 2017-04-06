using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WebmarketContext;
using Neo4jClient;
using MySql.Data.MySqlClient;

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
                Models.Producto producto = new Models.Producto(prod.Id, prod.IdProveedor, prod.Nombre, prod.Descripcion, prod.Marca, prod.Precio, prod.Cantidad, prod.UsuarioCreador);
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
                Models.Producto producto = new Models.Producto(prod.Id, prod.IdProveedor, prod.Nombre, prod.Descripcion, prod.Marca, prod.Precio, prod.Cantidad, prod.UsuarioCreador);
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
            Models.Producto rProd = new Models.Producto(nProd.Id, nProd.IdProveedor, nProd.Nombre, nProd.Descripcion, nProd.Marca, nProd.Precio, nProd.Cantidad, nProd.UsuarioCreador);
            
            return RedirectToAction("Detalles", "Shop", rProd);
        }

        [HttpPost]
        public ActionResult AgregaProducto(Models.Producto nProd)
        {

            HttpCookie nCookie = Request.Cookies["sesionAbierta"];
            String nUsuario = nCookie.Value.ToString();

            var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "test");
            cliente.Connect();

            var query = cliente.Cypher
                .Match("(userF:User)")
                .Where((Usuario userF) => userF.User == nUsuario)
                .Return(userF => userF.As<Usuario>());

            var userResult = query.Results;

            nProd.Usuario_Creador = userResult.First().Id;

            string myConnectionString = "server=127.0.0.1;port=3307;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                String nQuery = "INSERT INTO webmarket.producto(id_proveedor, nombre, descripcion, marca, precio, cantidad, usuario_creador)"  + 
                    "VALUES (" + nProd.Id_Proveedor + ", '" + nProd.Nombre + "', '" + nProd.Descripcion + "', '" + nProd.Marca + "', '" + nProd.Precio + "', " + nProd.Cantidad + ", " + nProd.Usuario_Creador + ");";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                nCommand.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                
            }

            return RedirectToAction("Admin", "Shop", userResult.First());
        }
        public ActionResult Agregar()
        {
            return View();
        }
    }
}