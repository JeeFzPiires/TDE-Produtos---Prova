using ApiProduto.Context;
using ApiProduto.models;
using ApiProduto.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ApiProduto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly ProdutoContext context;
        public VendaController()
        {
            context = new ProdutoContext();

        }


        //  [Authorize]
        [HttpPost("RegistrarVenda")]
        public ActionResult RegistrarVenda(Vendas vendas)
        {

            foreach (var item in vendas.Itens)
            {
                var resultado = context._produtos.Find<Produto>(p => p.Id == item.CodigoProduto).FirstOrDefault();
                if (resultado == null)
                {
                    return NotFound($"O produto {item.CodigoProduto} não existe na base de dados, venda nao pde ser feita");
                }

                if (resultado.EstoqueAtual < item.qtde)
                {
                    return BadRequest($"O produto {item.qtde} nao pode ter venda relizada, Venda maior que o estoque atual!");
                }

                resultado.EstoqueAtual = resultado.EstoqueAtual - item.qtde;

                context._produtos.ReplaceOne<Produto>(p => p.Id == resultado.Id, resultado);

            }
            context._vendas.InsertOne(vendas);
            return Ok(vendas);

        }

    }
}
