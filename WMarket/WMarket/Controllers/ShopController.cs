using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using Neo4jClient;
using MySql.Data.MySqlClient;
using Cassandra;
using System.Threading.Tasks;

namespace WMarket.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(Usuario nUser)
        {
            List<Models.Producto> listaProductos = new List<Models.Producto>();
            String myConnectionString = "server=127.0.0.1;port=3307;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                String nQuery = "SELECT * FROM webmarket.producto WHERE webmarket.producto.cantidad > 0 AND webmarket.producto.activo = 1;";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                MySqlDataReader nReader = nCommand.ExecuteReader();
                
                if(nReader.HasRows)
                {
                    while(nReader.Read())
                    {
                        Models.Producto producto = new Models.Producto(Convert.ToInt32(nReader["Id"].ToString()), Convert.ToInt32(nReader["Id_Proveedor"].ToString()), nReader["Nombre"].ToString(), nReader["Descripcion"].ToString(), nReader["Marca"].ToString(), nReader["Precio"].ToString(), Convert.ToInt32(nReader["Cantidad"].ToString()), Convert.ToInt32(nReader["Usuario_Creador"].ToString()), Convert.ToBoolean(Convert.ToInt32(nReader["Activo"].ToString())));
                        listaProductos.Add(producto);

                        producto = null;
                    }
                }

                
                ViewData["ListaProductos"] = listaProductos;
                ViewData["Usuario"] = nUser;
                Session["user"] = nUser;
                cnn.Close();

                return View();
            }
            catch (Exception ex)
            {
                MensajeError mError = new MensajeError("MySQL", "No se pudo conectar con la base de datos.", "Login", "Index");
                return RedirectToAction("Error", "Login", mError);
            }

            
        }

        public ActionResult Admin(Usuario nUser)
        {

            List<Models.Producto> listaProductos = new List<Models.Producto>();
            String myConnectionString = "server=127.0.0.1;port=3307;database=webmarket;uid=shop;pwd=shop123;";
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
                        Models.Producto producto = new Models.Producto(Convert.ToInt32(nReader["Id"].ToString()), Convert.ToInt32(nReader["Id_Proveedor"].ToString()), nReader["Nombre"].ToString(), nReader["Descripcion"].ToString(), nReader["Marca"].ToString(), nReader["Precio"].ToString(), Convert.ToInt32(nReader["Cantidad"].ToString()), Convert.ToInt32(nReader["Usuario_Creador"].ToString()), Convert.ToBoolean(Convert.ToInt32(nReader["Activo"].ToString())));
                        listaProductos.Add(producto);

                        producto = null;
                    }
                }

                ViewData["ListaProductos"] = listaProductos;
                ViewData["Usuario"] = nUser;
                cnn.Close();

                return View();
                
            }
            catch(Exception ex)
            {
                MensajeError mError = new MensajeError("MySQL", "No se pudo comunicar con la base de datos.", "Login", "Index");
                return RedirectToAction("Error", "Login", mError);
            }
            //Tuple<List<Models.Producto>, Usuario> tuplaVista = new Tuple<List<Models.Producto>, Usuario>(listaProductos, nUser);
            
        }

        public ActionResult Detalles(Producto datos)
        {
            ViewData["ProdDetalles"] = datos;
            ViewData["imagen"] = Session["imagen"];

            return View();
        }
        
        public ActionResult DetallesProd(Models.Producto pId)
        {
            
            Models.Producto producto = new Producto();
            String myConnectionString = "server=127.0.0.1;port=3307;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();

                //MySQL
                String nQuery = "SELECT * FROM webmarket.producto WHERE webmarket.producto.id = " + pId.Id + ";";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                MySqlDataReader nReader = nCommand.ExecuteReader();
                
                if (nReader.HasRows)
                {
                    while (nReader.Read())
                    {
                        producto.Id = Convert.ToInt32(nReader["Id"].ToString());
                        producto.Id_Proveedor = Convert.ToInt32(nReader["Id_Proveedor"].ToString());
                        producto.Nombre = nReader["Nombre"].ToString();
                        producto.Descripcion = nReader["Descripcion"].ToString();
                        producto.Marca = nReader["Marca"].ToString();
                        producto.Precio = nReader["Precio"].ToString();
                        producto.Cantidad = Convert.ToInt32(nReader["Cantidad"].ToString());
                        producto.Usuario_Creador = Convert.ToInt32(nReader["Usuario_Creador"].ToString());
                        producto.Activo = Convert.ToBoolean(Convert.ToInt32(nReader["Activo"].ToString()));

                    }
                }

                cnn.Close();

                ImagenController nImagen = new ImagenController();
                List<Imagenes> lista = nImagen.GetThePictures();
                Imagenes fImagen = new Imagenes();

                foreach(var img in lista)
                {
                    if(img.codPro == producto.Id)
                    {
                        fImagen.codPro = img.codPro;
                        fImagen.FileName = img.FileName;
                        fImagen.PictureDataAsString = img.PictureDataAsString;
                    }
                }

                Session["imagen"] = fImagen;
                return RedirectToAction("Detalles", "Shop", producto);
            }
            catch (Exception ex)
            {
                MensajeError mError = new MensajeError("MySQL", "No se pudo conectar con la base de datos.", "Shop", "Index");
                return RedirectToAction("Error", "Shop", mError);
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

            string myConnectionString = "server=127.0.0.1;port=3307;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            long lastId = 0;

            try
            {
                cnn.Open();
                String nQuery = "INSERT INTO webmarket.producto(id_proveedor, nombre, descripcion, marca, precio, cantidad, usuario_creador, activo)"  + 
                    "VALUES (" + nProd.Id_Proveedor + ", '" + nProd.Nombre + "', '" + nProd.Descripcion + "', '" + nProd.Marca + "', '" + nProd.Precio + "', " + nProd.Cantidad + ", " + nProd.Usuario_Creador +", "+ nProd.Activo + ");";
                MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                nCommand.ExecuteNonQuery();
                lastId = nCommand.LastInsertedId;
                nProd.Id = Convert.ToInt32(lastId);
                cnn.Close();
            }
            catch (Exception ex)
            {
                MensajeError mError = new MensajeError("MySQL", "Error al insertar el producto", "Shop", "Agregar");
                return RedirectToAction("Error", "Shop", mError);
            }
            Session["nProd"] = nProd;
            return RedirectToAction("AddPicture", "Imagen");
        }
        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult DesactivarProducto(Producto nProd)
        {

            var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "test");
            cliente.Connect();

            var query = cliente.Cypher
                .Match("(userF:User)")
                .Where((Usuario userF) => userF.Id == nProd.Usuario_Creador)
                .Return(userF => userF.As<Usuario>());

            var userResult = query.Results;

            string myConnectionString = "server=127.0.0.1;port=3307;database=webmarket;uid=shop;pwd=shop123;";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            
            try
            {
                cnn.Open();

                if(nProd.Activo == true)
                {
                    String nQuery = "UPDATE webmarket.producto SET webmarket.producto.activo = 0 WHERE webmarket.producto.id = " + nProd.Id + ";";
                    MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                    nCommand.ExecuteNonQuery();
                }
                else
                {
                    String nQuery = "UPDATE webmarket.producto SET webmarket.producto.activo = 1 WHERE webmarket.producto.id = " + nProd.Id + ";";
                    MySqlCommand nCommand = new MySqlCommand(nQuery, cnn);
                    nCommand.ExecuteNonQuery();
                }
                
                
                cnn.Close();

                

                return RedirectToAction("Admin", "Shop", userResult.First());

            }
            catch(Exception ex)
            {
                MensajeError mError = new MensajeError("Neo4j", "No se pudo comunicar con la base de datos.", "Shop", "Admin");
                Tuple<MensajeError, Usuario> tuplaDatosError = new Tuple<MensajeError, Usuario>(mError, userResult.First());
                return RedirectToAction("Error", "Shop", tuplaDatosError);
            }
            
        }

        public ActionResult Error(Tuple<MensajeError, Usuario> mError)
        {
            ViewData["mError"] = mError;
            return View();

        }

        public ActionResult VolverError(Tuple<MensajeError, Usuario> mError)
        {
            return RedirectToAction(mError.Item1.View, mError.Item1.Controller, mError.Item2);
        }

        public ActionResult Comentarios(Producto nProd)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

            try
            {
                ISession session = cluster.Connect("webmarket");
               
                //Cassandra
                Comentario nComment = new Comentario();
                List<Comentario> listaComments = new List<Comentario>();
                RowSet resultados = session.Execute("select * from comentarios where productid = " + nProd.Id + " ALLOW FILTERING;");

                foreach (var comment in resultados)
                {
                    nComment.Id = comment["id"].ToString();
                    nComment.User = comment["user"].ToString();
                    nComment.ProductId = Convert.ToInt32(comment["productid"]);
                    nComment.Title = comment["titulo"].ToString();
                    nComment.Detalle = comment["detalle"].ToString();

                        listaComments.Add(nComment);

                        nComment = new Comentario();
                }
                ViewData["listaComms"] = listaComments;
                Session["producto"] = nProd;
                return View();
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Detalles", "Shop", nProd);
            }
        }

        [HttpPost]
        public ActionResult AgregarComentario(Comentario comment)
        {
            var producto = Session["producto"] as Producto;
            var user = Session["user"] as Usuario;
            comment.User = user.User;
            comment.ProductId = producto.Id;
           
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            
            

            try
            {
                
                ISession session = cluster.Connect("webmarket");

                session.Execute("insert into comentarios(id, user, productid, titulo, detalle) values(now(), '" +
                    comment.User + "'," + comment.ProductId + ",'" + comment.Title + "','" + comment.Detalle + "');");
                return RedirectToAction("Comentarios", "Shop", producto);
            }
            catch(Exception ex)
            {
                producto.Id = comment.ProductId;
                return RedirectToAction("Comentarios", "Shop", producto);
            }

            
        }

        public ActionResult LogOut(Usuario nUser)
        {
            if (Request.Cookies["sesionAbierta"] != null)
            {
                HttpCookie myCookie = new HttpCookie("sesionAbierta");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Index", "Login");
        }
        
            
    }
}