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
	public partial class Carrinho_produto : System.Runtime.Serialization.ISerializable
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
		
		private int? _produto;
		public virtual int? Produto
		{
			get
			{
				return this._produto;
			}
			set
			{
				this._produto = value;
			}
		}
		
		private int? _carrinho;
		public virtual int? Carrinho
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
		
		private int? _quantidade;
		public virtual int? Quantidade
		{
			get
			{
				return this._quantidade;
			}
			set
			{
				this._quantidade = value;
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
		
		private Produto _produto1;
		public virtual Produto Produto1
		{
			get
			{
				return this._produto1;
			}
			set
			{
				this._produto1 = value;
			}
		}
		
		#region ISerializable Implementation
		
		public Carrinho_produto()
		{
		}
		
		protected Carrinho_produto(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			this.Id = info.GetInt32("Id");
			this.Produto = (int?)info.GetValue("Produto", typeof(int?));
			this.Carrinho = (int?)info.GetValue("Carrinho", typeof(int?));
			this.Quantidade = (int?)info.GetValue("Quantidade", typeof(int?));
			CustomizeDeserializationProcess(info, context);
		}
		
		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("Id", this.Id, typeof(int));
			info.AddValue("Produto", this.Produto, typeof(int?));
			info.AddValue("Carrinho", this.Carrinho, typeof(int?));
			info.AddValue("Quantidade", this.Quantidade, typeof(int?));
			CustomizeSerializationProcess(info, context);
		}
		
		partial void CustomizeSerializationProcess(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
		partial void CustomizeDeserializationProcess(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
		#endregion
	}
}
#pragma warning restore 1591