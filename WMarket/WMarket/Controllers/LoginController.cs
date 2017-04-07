using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neo4jClient;
using WMarket.Models;
using System.Web.UI;
namespace WMarket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn (Usuario nUser)
        {
            try
            {
                var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "test");
                cliente.Connect();

                var query = cliente.Cypher
                    .Match("(userF:User)")
                    .Where((Usuario userF) => userF.User == nUser.User)
                    .Return(userF => userF.As<Usuario>());

                var userResult = query.Results;

                if(userResult.Count() == 1)
                {
                    if (userResult.First().Contrasena == nUser.Contrasena)
                    {
                        HttpCookie aCookie = new HttpCookie("sesionAbierta");
                        aCookie.Value = nUser.User.ToString();
                        aCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(aCookie);

                        if(userResult.First().Admin == true)
                        {
                            return RedirectToAction("Admin", "Shop", userResult.First());
                        }
                        else
                        {
                            return RedirectToAction("Index", "Shop", userResult.First());
                        }
                        
                    }
                    else
                    {
                        MensajeError mError = new MensajeError("Neo4j", "Contraseña incorrecta.", "Login", "Index");
                        return RedirectToAction("Error", "Login", mError);
                    }
                      
                }
                else 
                {
                    MensajeError mError = new MensajeError("Neo4j", "No existe el usuario en el sistema.", "Login", "Registro");
                    return RedirectToAction("Error", "Login", mError);
                }

                
            }
            catch (Exception e)
            {
                MensajeError mError = new MensajeError("Neo4j", "No se pudo comunicar con la base de datos.", "Login", "Index");
                return RedirectToAction("Error", "Login", mError);
            }
        }

        [HttpPost]
        public ActionResult Registrar( Usuario nUser)
        {
            try
            {
                var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "test");
                cliente.Connect();

                var query = cliente.Cypher
                    .Match("(userF:User)")
                    .Where((Usuario userF) => userF.User == nUser.User)
                    .Return(userF => userF.As<Usuario>());

                var nResults = query.Results;

                if (nResults.Count() > 0)
                {
                    MensajeError mError = new MensajeError("Neo4j", "El usuario ya está en uso, elija otro.", "Login", "Registro");
                    return RedirectToAction("Error", "Login", mError);
                }
                else
                {
                    cliente.Cypher
                        .Create("(user:User {nUser})")
                        .WithParam("nUser", nUser)
                        .ExecuteWithoutResults();

                    HttpCookie aCookie = new HttpCookie("sesionAbierta");
                    aCookie.Value = nUser.User.ToString();
                    aCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(aCookie);

                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception e)
            {
                MensajeError mError = new MensajeError("Neo4j", "No se pudo comunicar con el la base de datos.", "Login", "Index");
                return RedirectToAction("Error", "Login", mError);
            }


        }

        public ActionResult Error(MensajeError mError)
        {
            ViewData["mError"] = mError;
            return View();
            
        }

        public ActionResult VolverError(MensajeError mError)
        {
            return RedirectToAction(mError.View, mError.Controller);
        }

    }
}