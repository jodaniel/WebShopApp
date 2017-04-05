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

        public ActionResult ErrorRegistro()
        {
            return View();
        }

        public ActionResult ErrorLogIn()
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

                        return RedirectToAction("Index", "Shop");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                      
                }
                else 
                {
                    return RedirectToAction("Registro", "Login");
                }

                
            }
            catch (Exception e)
            {
                return RedirectToAction("ErrorLogIn", "Login");
            }
        }

        [HttpPost]
        public ActionResult Registrar( Usuario nUser)
        {
            try
            {
                var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "test");
                cliente.Connect();

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
            catch (Exception e)
            {
                
                return RedirectToAction("ErrorRegistro", "Login");
            }


        }

    }
}