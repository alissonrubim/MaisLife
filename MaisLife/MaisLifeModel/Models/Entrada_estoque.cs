//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaisLifeModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class entrada_estoque
    {
        public int codigo { get; set; }
        public int entrada { get; set; }
        public int estoque { get; set; }
        public Nullable<System.DateTime> data_entrada { get; set; }
    
        public virtual entrada entrada1 { get; set; }
        public virtual estoque estoque1 { get; set; }
    }
}
