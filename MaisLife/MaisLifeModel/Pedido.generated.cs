#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using MaisLifeModel;

namespace MaisLifeModel	
{
	[System.Serializable()]
	public partial class Pedido : System.Runtime.Serialization.ISerializable
	{
		private int _id;
		public virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		
		private int _usuario;
		public virtual int Usuario
		{
			get
			{
				return this._usuario;
			}
			set
			{
				this._usuario = value;
			}
		}
		
		private int _endereco;
		public virtual int Endereco
		{
			get
			{
				return this._endereco;
			}
			set
			{
				this._endereco = value;
			}
		}
		
		private decimal _valor;
		public virtual decimal Valor
		{
			get
			{
				return this._valor;
			}
			set
			{
				this._valor = value;
			}
		}
		
		private DateTime _data;
		public virtual DateTime Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}
		
		private string _status;
		public virtual string Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}
		
		private int _carrinho;
		public virtual int Carrinho
		{
			get
			{
				return this._carrinho;
			}
			set
			{
				this._carrinho = value;
			}
		}
		
		private decimal _pago;
		public virtual decimal Pago
		{
			get
			{
				return this._pago;
			}
			set
			{
				this._pago = value;
			}
		}
		
		private string _metodo;
		public virtual string Metodo
		{
			get
			{
				return this._metodo;
			}
			set
			{
				this._metodo = value;
			}
		}
		
		private Endereco _endereco1;
		public virtual Endereco Endereco1
		{
			get
			{
				return this._endereco1;
			}
			set
			{
				this._endereco1 = value;
			}
		}
		
		private Usuario _usuario1;
		public virtual Usuario Usuario1
		{
			get
			{
				return this._usuario1;
			}
			set
			{
				this._usuario1 = value;
			}
		}
		
		private Carrinho _carrinho1;
		public virtual Carrinho Carrinho1
		{
			get
			{
				return this._carrinho1;
			}
			set
			{
				this._carrinho1 = value;
			}
		}
		
		private IList<Produto_pedido> _produto_pedidos = new List<Produto_pedido>();
		public virtual IList<Produto_pedido> Produto_pedidos
		{
			get
			{
				return this._produto_pedidos;
			}
		}
		
		#region ISerializable Implementation
		
		public Pedido()
		{
		}
		
		protected Pedido(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			this.Id = info.GetInt32("Id");
			this.Usuario = info.GetInt32("Usuario");
			this.Endereco = info.GetInt32("Endereco");
			this.Valor = info.GetDecimal("Valor");
			this.Data = (DateTime)info.GetValue("Data", typeof(DateTime));
			this.Status = info.GetString("Status");
			this.Carrinho = info.GetInt32("Carrinho");
			this.Pago = info.GetDecimal("Pago");
			this.Metodo = info.GetString("Metodo");
			CustomizeDeserializationProcess(info, context);
		}
		
		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("Id", this.Id, typeof(int));
			info.AddValue("Usuario", this.Usuario, typeof(int));
			info.AddValue("Endereco", this.Endereco, typeof(int));
			info.AddValue("Valor", this.Valor, typeof(decimal));
			info.AddValue("Data", this.Data, typeof(DateTime));
			info.AddValue("Status", this.Status, typeof(string));
			info.AddValue("Carrinho", this.Carrinho, typeof(int));
			info.AddValue("Pago", this.Pago, typeof(decimal));
			info.AddValue("Metodo", this.Metodo, typeof(string));
			CustomizeSerializationProcess(info, context);
		}
		
		partial void CustomizeSerializationProcess(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
		partial void CustomizeDeserializationProcess(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
		#endregion
	}
}
#pragma warning restore 1591
