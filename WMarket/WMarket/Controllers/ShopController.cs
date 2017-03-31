using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using WMarket.Models;

namespace WMarket.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;port=3307;uid=shop;" +
                "pwd=shop123;database=webmarket;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                List<Producto> productos = new List<Producto>();
                product


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                
            }
            return View();
        }
    }
}