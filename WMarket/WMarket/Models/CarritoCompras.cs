using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    [Serializable]
    public class CarritoCompras
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
    }
}