//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FarmaciaB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUCURSAL_PRODUCTO
    {
        public int ID_SUCURSAL_PRODUCTO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public int ID_PRODUCTO { get; set; }
        public System.DateTime FECHA_VENCIMIENTO { get; set; }
        public int EXISTENCIA { get; set; }
        public Nullable<decimal> PRECIO { get; set; }
    
        public virtual SUCURSAL SUCURSAL { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}
