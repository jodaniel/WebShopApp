using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    [Serializable]
    public class Usuario
    {
        #region Atributos
        public static int nextId = 1;
        int id = nextId++;
        String nombreCompleto;
        String usuario;
        String contrasena;
        String telefono;
        String correo;
        String direccion;
        Boolean admin;


        #endregion

        #region Propiedades

        public int Id
        {
            get  { return this.id; }
            set { this.id = value; }

        }

        public String NombreCompleto
        {
            get { return this.nombreCompleto;}
            set { this.nombreCompleto = value; }
        }

        public String User
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public String Contrasena
        {
            get { return this.contrasena; }
            set { this.contrasena = value; }
        }

        public String Telefono
        {
            get { return this.telefono; }
            set { this.telefono = value; }
        }

        public String Correo
        {
            get { return this.correo; }
            set { this.correo = value; }
        }

        public String Direccion
        {
            get { return this.direccion; }
            set { this.direccion = value; }
        }

        public Boolean Admin
        {
            get { return this.admin; }
            set { this.admin = value; }
        }

        #endregion

        #region Constructor & Destructor
        public Usuario()
        { }

        public Usuario(String nNombreCompleto, String nUser, String nContrasena, String nTelefono, String nCorreo, String nDireccion, Boolean nAdmin)
        {
          
            NombreCompleto = nNombreCompleto;
            User = nUser;
            Contrasena = nContrasena;
            Telefono = nTelefono;
            Correo = nCorreo;
            Direccion = nDireccion;
            Admin = nAdmin;
            nextId++;
        }

        public Usuario(String nUser, String nPassword)
        {
            User = nUser;
            Contrasena = nPassword;

        }

        #endregion

        #region Metodos
        #endregion
    }

    
}