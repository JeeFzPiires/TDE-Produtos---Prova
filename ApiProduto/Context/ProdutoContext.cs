using ApiProduto.models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.Context
{
    public class ProdutoContext
    {

        public MongoDatabase Database;
        public String DataBaseName = "test";
        string conexaoMongoDB = "mongodb+srv://TDE:tde@cluster0.nxwxm.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";


        public IMongoCollection<Produto> _produtos;
        public IMongoCollection<Usuario> _usuarios;
        public IMongoCollection<Categoria> _categoria;
        public IMongoCollection<Vendas> _vendas;
        public ProdutoContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _produtos = server.GetCollection<Produto>("Produtos");
            _categoria = server.GetCollection<Categoria>("Categorias");
            _usuarios = server.GetCollection<Usuario>("Usuarios");
            _vendas = server.GetCollection<Vendas>("Vendas");

        }
    }
    

   
}

