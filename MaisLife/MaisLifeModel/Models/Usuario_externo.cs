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
    
    public partial class usuario_externo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario_externo()
        {
            this.pedido = new HashSet<pedido>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public string telefone { get; set; }
        public int endereco { get; set; }
    
        public virtual endereco endereco1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido> pedido { get; set; }
    }
}