using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using Neo4jClient;
using MySql.Data.MySqlClient;

namespace WMarket.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(Usuario nUser)
        {
            List<Models.Producto> listaProductos = new List<Models.Producto>();
            String myConnectionString = "server=127.0.0.1;port=3306;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                String nQuery = "SELECT * FROM webmarket.producto WHERE webmarket.producto.cantidad > 0;";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                MySqlDataReader nReader = nCommand.ExecuteReader();
                
                if(nReader.HasRows)
                {
                    while(nReader.Read())
                    {
                        Models.Producto producto = new Models.Producto(Convert.ToInt32(nReader["Id"]), Convert.ToInt32(nReader["IdProveedor"]), nReader["Nombre"].ToString(), nReader["Descripcion"].ToString(), nReader["Marca"].ToString(), nReader["Precio"].ToString(), Convert.ToInt32(nReader["Cantidad"]), Convert.ToInt32(nReader["UsuarioCreador"]));
                        listaProductos.Add(producto);

                        producto = null;
                    }
                }

                

                cnn.Close();
            }
            catch (Exception ex)
            {

            }

            ViewData["ListaProductos"] = listaProductos;
            ViewData["Usuario"] = nUser;

            return View();
        }

        public ActionResult Admin(Usuario nUser)
        {

            List<Models.Producto> listaProductos = new List<Models.Producto>();
            String myConnectionString = "server=127.0.0.1;port=3306;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                String nQuery = "SELECT * FROM webmarket.producto WHERE webmarket.producto.usuario_creador = " + nUser.Id + ";";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                MySqlDataReader nReader = nCommand.ExecuteReader();

                if (nReader.HasRows)
                {
                    while (nReader.Read())
                    {
                        Models.Producto producto = new Models.Producto(Convert.ToInt32(nReader["Id"]), Convert.ToInt32(nReader["IdProveedor"]), nReader["Nombre"].ToString(), nReader["Descripcion"].ToString(), nReader["Marca"].ToString(), nReader["Precio"].ToString(), Convert.ToInt32(nReader["Cantidad"]), Convert.ToInt32(nReader["UsuarioCreador"]));
                        listaProductos.Add(producto);

                        producto = null;
                    }
                }

                ViewData["ListaProductos"] = listaProductos;
                ViewData["Usuario"] = nUser;
                cnn.Close();
            }
            catch(Exception ex)
            { }
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
            Models.Producto producto = new Producto();
            String myConnectionString = "server=127.0.0.1;port=3306;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                String nQuery = "SELECT * FROM webmarket.producto WHERE webmarket.producto.id = " + pId.Id + ";";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                MySqlDataReader nReader = nCommand.ExecuteReader();
                
                if (nReader.HasRows)
                {
                    while (nReader.Read())
                    {
                        producto = new Models.Producto(Convert.ToInt32(nReader["Id"]), Convert.ToInt32(nReader["IdProveedor"]), nReader["Nombre"].ToString(), nReader["Descripcion"].ToString(), nReader["Marca"].ToString(), nReader["Precio"].ToString(), Convert.ToInt32(nReader["Cantidad"]), Convert.ToInt32(nReader["UsuarioCreador"]));
                    }
                }

                cnn.Close();
                return RedirectToAction("Detalles", "Shop", producto);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Detalles", "Shop");
            }
            
            
            
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

            string myConnectionString = "server=127.0.0.1;port=3306;database=webmarket;uid=shop;pwd=shop123;";
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