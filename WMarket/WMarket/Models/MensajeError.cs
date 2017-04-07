using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class MensajeError
    {

        #region Atributos

        String database;
        String mensaje;
        String controller;
        String view;

        #endregion

        #region Propiedades

        public String Database
        {
            get { return this.database; }
            set { this.database = value; }
        }

        public String Mensaje
        {
            get { return this.mensaje; }
            set { this.mensaje = value; }
        }

        public String Controller
        {
            get { return this.controller; }
            set { this.controller = value; }
        }

        public String View
        {
            get { return this.view; }
            set { this.view = value; }
        }

        #endregion

        #region Constructor & Destructor

        public MensajeError()
        {

        }

        public MensajeError(String nDatabase, String nMensaje, String nController, String nView)
        {
            Database = nDatabase;
            Mensaje = nMensaje;
            Controller = nController;
            View = nView;
        }

        #endregion

    }
}