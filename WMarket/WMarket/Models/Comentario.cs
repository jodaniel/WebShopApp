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
        int userid;
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

        public int UserId
        {
            get { return this.userid; }
            set { this.userid = value; }
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

        public Comentario(int nUserid, int nProductid, String nTitle, String nDetalle)
        {
            
            UserId = nUserid;
            ProductId = nProductid;
            Title = nTitle;
            Detalle = nDetalle;
        }

        public Comentario(String nId, int nUserid, int nProductid, String nTitle, String nDetalle)
        {
            Id = nId;
            UserId = nUserid;
            ProductId = nProductid;
            Title = nTitle;
            Detalle = nDetalle;
        }

        #endregion

    }
}