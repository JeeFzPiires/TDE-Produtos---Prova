using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.models
{
    public class VendasProdutos
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CodigoProduto { get; set; }

        public string NomeProduto { get; set; }

        public decimal valorUnitario { get; set; }

        public int qtde { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
