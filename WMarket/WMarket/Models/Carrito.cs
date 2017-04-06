using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    [Serializable]
    public class Carrito
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}