using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neo4jClient;
using WMarket.Models;

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

        /*[HttpPost]
        public ActionResult ]*/


        public void Neo4jConn()
        {
            var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "neo4j");
            cliente.Connect();
        }

        public Usuario GeneraUsuario()
        {
            Usuario nUsuario = new Usuario();



            return nUsuario;
        }
    }
}