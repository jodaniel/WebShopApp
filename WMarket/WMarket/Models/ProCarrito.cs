using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    [Serializable]
    public class ProCarrito
    {
        public int Id_prod { get; set; }
        public String Id_cli { get; set; }
        public String Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int Modo { get; set; }
    }
}