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
    
    public partial class pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pedido()
        {
            this.mapa_pedido = new HashSet<mapa_pedido>();
        }
    
        public int id { get; set; }
        public string tipo { get; set; }
        public string origem { get; set; }
        public System.DateTime data { get; set; }
        public string metodo { get; set; }
        public int usuario { get; set; }
        public int carrinho { get; set; }
        public Nullable<int> usuario_externo { get; set; }
        public int endereco { get; set; }
        public decimal valor { get; set; }
        public string status { get; set; }
        public Nullable<decimal> pago { get; set; }
        public Nullable<int> desconto { get; set; }
        public Nullable<int> parcelas { get; set; }
        public Nullable<System.DateTime> vencimento { get; set; }
        public string motivo_troca { get; set; }
        public Nullable<System.DateTime> previsao_entrega { get; set; }
    
        public virtual carrinho carrinho1 { get; set; }
        public virtual endereco endereco1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mapa_pedido> mapa_pedido { get; set; }
        public virtual usuario usuario1 { get; set; }
        public virtual usuario_externo usuario_externo1 { get; set; }
    }
}
