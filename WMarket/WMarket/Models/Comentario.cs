using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Comentario
    {

        #region Atributos
        String id;
        String user;
        int productid;
        String title;
        String detalle;

        #endregion

        #region Propiedades
        public String Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String User
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public int ProductId
        {
            get { return this.productid; }
            set { this.productid = value; }
        }

        public String Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public String Detalle
        {
            get { return this.detalle; }
            set { this.detalle = value; }
        }
        #endregion

        #region Constructor & Destructor

        public Comentario()
        {

        }

        public Comentario(String nUserid, int nProductid, String nTitle, String nDetalle)
        {
            
            User = nUserid;
            ProductId = nProductid;
            Title = nTitle;
            Detalle = nDetalle;
        }

        public Comentario(String nId, String nUserid, int nProductid, String nTitle, String nDetalle)
        {
            Id = nId;
            User = nUserid;
            ProductId = nProductid;
            Title = nTitle;
            Detalle = nDetalle;
        }

        #endregion

    }
}