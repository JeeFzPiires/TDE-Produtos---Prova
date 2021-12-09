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
    public class ProdutoController : ControllerBase
    {

        private readonly ProdutoContext Context;
        public ProdutoController()
        {
            Context = new ProdutoContext();
        }

        [HttpGet]
        public ActionResult Ola()
        {
            return Ok("ola");
        }

        /// <summary>
        ///  Consulta dados de uma pessoa a partir do CPF
        ///  Requer uso de token.
        /// </summary>
        /// <param name="cpf">CPF</param>
        /// <returns>Objeto contendo dados de uma pessoa.</returns>
        [Authorize]
        [HttpGet("ObterPorId/{Id}")]
        public ActionResult ObterPorId(string id )
        {

           return Ok(Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault());
        }

        
        [HttpPost("AdicionarP")]
        public ActionResult AdicionarP(Produto produto)
        {
            ProdutoValidation produtoValidation = new ProdutoValidation();

            var validacao = produtoValidation.Validate(produto);


            if (!validacao.IsValid)
            {
                List<string> erros = new List<string>();
                foreach (var failure in validacao.Errors)
                {
                    erros.Add("Property " + failure.PropertyName +
                        " failed validation. Error Was: "
                        + failure.ErrorMessage);
                }

                return BadRequest(erros);
            }


            Context._produtos.InsertOne(produto);

            return CreatedAtAction(nameof(AdicionarP), "");
        }

       


        [HttpPut("Atualizar/{id}")]
        public ActionResult Atualizar(string id, [FromBody] Produto produto)
        {
            var pResultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
            NotFound("Id não encontrado, atualizacao não realizada!");

            produto.Id = id;
            Context._produtos.ReplaceOne<Produto>(p => p.Id == id, produto);

            return NoContent();


        }



        [HttpPut("Desativar/{id}")]
        public ActionResult Desativar(string id)
        {
            var ProdutoDesativado = Context._produtos.Find<Produto>(P => P.Id == id).FirstOrDefault();
            if (ProdutoDesativado == null)
                return NotFound("Produto não pode ser desativado, pois id não existe");


            if (ProdutoDesativado != null &&
                ProdutoDesativado.Ativo == false)
                return BadRequest("Produto já está desativado, operação não realizada");



            ProdutoDesativado.Ativo = false;
   

            return Ok("Produto desativado");
        }



        [HttpDelete("Remover/{Id}")]
        public ActionResult Remover(string id)
        {


            var Resultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();
            if (Resultado == null) return
                    NotFound("Id não encontrada, atualizacao não realizada!");

            Context._produtos.DeleteOne<Produto>(filter => filter.Id == id);
            return NoContent();
        }
        
    }

}
