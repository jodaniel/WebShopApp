﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using LinqConnect template.
// Code is generated on: 5/4/2017 1:53:18 p. m.
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using Devart.Data.Linq;
using Devart.Data.Linq.Mapping;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace WebmarketContext
{

    [DatabaseAttribute(Name = "webmarket")]
    [ProviderAttribute(typeof(Devart.Data.MySql.Linq.Provider.MySqlDataProvider))]
    public partial class WebmarketDataContext : Devart.Data.Linq.DataContext
    {
        public static CompiledQueryCache compiledQueryCache = CompiledQueryCache.RegisterDataContext(typeof(WebmarketDataContext));
        private static MappingSource mappingSource = new Devart.Data.Linq.Mapping.AttributeMappingSource();

        #region Extensibility Method Definitions
    
        partial void OnCreated();
        partial void OnSubmitError(Devart.Data.Linq.SubmitErrorEventArgs args);
        partial void InsertProducto(Producto instance);
        partial void UpdateProducto(Producto instance);
        partial void DeleteProducto(Producto instance);

        #endregion

        public WebmarketDataContext() :
        base(GetConnectionString("WebmarketDataContextConnectionString"), mappingSource)
        {
            OnCreated();
        }

        public WebmarketDataContext(MappingSource mappingSource) :
        base(GetConnectionString("WebmarketDataContextConnectionString"), mappingSource)
        {
            OnCreated();
        }

        private static string GetConnectionString(string connectionStringName)
        {
            System.Configuration.ConnectionStringSettings connectionStringSettings = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null)
                throw new InvalidOperationException("Connection string \"" + connectionStringName +"\" could not be found in the configuration file.");
            return connectionStringSettings.ConnectionString;
        }

        public WebmarketDataContext(string connection) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public WebmarketDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public WebmarketDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public WebmarketDataContext(System.Data.IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public Devart.Data.Linq.Table<Producto> Productos
        {
            get
            {
                return this.GetTable<Producto>();
            }
        }
    }
}

namespace WebmarketContext
{

    /// <summary>
    /// There are no comments for WebmarketContext.Producto in the schema.
    /// </summary>
    [Table(Name = @"webmarket.producto")]
    public partial class Producto : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);
        #pragma warning disable 0649

        private int _Id;

        private int _IdProveedor;

        private string _Nombre;

        private string _Descripcion;

        private string _Marca;

        private string _Precio;

        private int _Cantidad;

        private int _UsuarioCreador;
        #pragma warning restore 0649
    
        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnIdProveedorChanging(int value);
        partial void OnIdProveedorChanged();
        partial void OnNombreChanging(string value);
        partial void OnNombreChanged();
        partial void OnDescripcionChanging(string value);
        partial void OnDescripcionChanged();
        partial void OnMarcaChanging(string value);
        partial void OnMarcaChanged();
        partial void OnPrecioChanging(string value);
        partial void OnPrecioChanged();
        partial void OnCantidadChanging(int value);
        partial void OnCantidadChanged();
        partial void OnUsuarioCreadorChanging(int value);
        partial void OnUsuarioCreadorChanged();
        #endregion

        public Producto()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        [Column(Name = @"id", Storage = "_Id", AutoSync = AutoSync.OnInsert, CanBeNull = false, DbType = "INT(11) NOT NULL AUTO_INCREMENT", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if (this._Id != value)
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IdProveedor in the schema.
        /// </summary>
        [Column(Name = @"id_proveedor", Storage = "_IdProveedor", CanBeNull = false, DbType = "INT(11) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public int IdProveedor
        {
            get
            {
                return this._IdProveedor;
            }
            set
            {
                if (this._IdProveedor != value)
                {
                    this.OnIdProveedorChanging(value);
                    this.SendPropertyChanging();
                    this._IdProveedor = value;
                    this.SendPropertyChanged("IdProveedor");
                    this.OnIdProveedorChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Nombre in the schema.
        /// </summary>
        [Column(Name = @"nombre", Storage = "_Nombre", CanBeNull = false, DbType = "VARCHAR(45) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public string Nombre
        {
            get
            {
                return this._Nombre;
            }
            set
            {
                if (this._Nombre != value)
                {
                    this.OnNombreChanging(value);
                    this.SendPropertyChanging();
                    this._Nombre = value;
                    this.SendPropertyChanged("Nombre");
                    this.OnNombreChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Descripcion in the schema.
        /// </summary>
        [Column(Name = @"descripcion", Storage = "_Descripcion", CanBeNull = false, DbType = "VARCHAR(100) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public string Descripcion
        {
            get
            {
                return this._Descripcion;
            }
            set
            {
                if (this._Descripcion != value)
                {
                    this.OnDescripcionChanging(value);
                    this.SendPropertyChanging();
                    this._Descripcion = value;
                    this.SendPropertyChanged("Descripcion");
                    this.OnDescripcionChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Marca in the schema.
        /// </summary>
        [Column(Name = @"marca", Storage = "_Marca", CanBeNull = false, DbType = "VARCHAR(45) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public string Marca
        {
            get
            {
                return this._Marca;
            }
            set
            {
                if (this._Marca != value)
                {
                    this.OnMarcaChanging(value);
                    this.SendPropertyChanging();
                    this._Marca = value;
                    this.SendPropertyChanged("Marca");
                    this.OnMarcaChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Precio in the schema.
        /// </summary>
        [Column(Name = @"precio", Storage = "_Precio", CanBeNull = false, DbType = "VARCHAR(6) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public string Precio
        {
            get
            {
                return this._Precio;
            }
            set
            {
                if (this._Precio != value)
                {
                    this.OnPrecioChanging(value);
                    this.SendPropertyChanging();
                    this._Precio = value;
                    this.SendPropertyChanged("Precio");
                    this.OnPrecioChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Cantidad in the schema.
        /// </summary>
        [Column(Name = @"cantidad", Storage = "_Cantidad", CanBeNull = false, DbType = "INT(11) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public int Cantidad
        {
            get
            {
                return this._Cantidad;
            }
            set
            {
                if (this._Cantidad != value)
                {
                    this.OnCantidadChanging(value);
                    this.SendPropertyChanging();
                    this._Cantidad = value;
                    this.SendPropertyChanged("Cantidad");
                    this.OnCantidadChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for UsuarioCreador in the schema.
        /// </summary>
        [Column(Name = @"usuario_creador", Storage = "_UsuarioCreador", CanBeNull = false, DbType = "INT(11) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public int UsuarioCreador
        {
            get
            {
                return this._UsuarioCreador;
            }
            set
            {
                if (this._UsuarioCreador != value)
                {
                    this.OnUsuarioCreadorChanging(value);
                    this.SendPropertyChanging();
                    this._UsuarioCreador = value;
                    this.SendPropertyChanged("UsuarioCreador");
                    this.OnUsuarioCreadorChanged();
                }
            }
        }
   
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) 
        {    
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName)
        {    
		        var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
