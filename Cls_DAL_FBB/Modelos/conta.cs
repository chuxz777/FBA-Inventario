//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cls_DAL_FBB.Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class conta
    {
        public int id_factura { get; set; }
        public System.DateTime fecha_factura { get; set; }
        public decimal monto { get; set; }
        public string detalle { get; set; }
        public string proveedor { get; set; }
        public Nullable<int> cod_categoria { get; set; }
        public int cod_estado { get; set; }
    
        public virtual categoria categoria { get; set; }
        public virtual estado estado { get; set; }
    }
}
