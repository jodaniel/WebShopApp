using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Producto
    {
        #region Atributos

        int id;
        int id_proveedor;
        String nombre;
        String descripcion;
        String marca;
        String precio;
        int cantidad;

        #endregion

        #region Propiedades

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int Id_Proveedor
        {
            get { return this.id_proveedor; }
            set { this.id_proveedor = value; }
        }

        public String Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public String Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        public String Marca
        {
            get { return this.marca; }
            set { this.marca = value; }
        }

        public String Precio
        {
            get { return this.precio; }
            set { this.precio = value; }
        }

        public int Cantidad
        {
            get { return this.cantidad; }
            set { this.cantidad = value; }
        }
        #endregion

        #region Constructor & Destructor

        public Producto() { }

        public Producto(int nId, int nId_Proveedor, String nNombre, String nDescripcion, String nMarca, int nCantidad)
        {
            this.Id = nId;
            this.Id_Proveedor = nId_Proveedor;
            this.Nombre = nNombre;
            this.Descripcion = nDescripcion;
            this.Marca = nMarca;
            this.Cantidad = nCantidad;
        }


        #endregion
    }
}