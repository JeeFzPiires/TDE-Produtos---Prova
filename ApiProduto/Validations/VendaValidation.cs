using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiProduto.models;

namespace TDEProdutos.Validations
{
    public class VendaVallidation : AbstractValidator<Vendas>
    {
        public VendaVallidation()
        {
            RuleFor(Vendas => Vendas.DataVenda)
                   .NotEmpty().WithMessage("Campo data venda vazia,tente novamente ")
                   .NotNull().WithMessage("Campo data venda não informado, tente novamente !");

            RuleFor(Vendas => Vendas.CpfCliente)
                 .NotEmpty().WithMessage("Campo cpf cliente vazia,tente novamente ")
                 .NotNull().WithMessage("Campo cpf cliente não informado, tente novamente !");

            RuleFor(Vendas => Vendas.CpfVendedor)
                .NotEmpty().WithMessage("Campo cpf vendedor vazia,tente novamente ")
                .NotNull().WithMessage("Campo cpf vendedor não informado, tente novamente !");

            RuleFor(Vendas => Vendas.ValorTotal)
                .GreaterThan(0).WithMessage("preço deve ser maior que zero");

            RuleFor(Vendas => Vendas.NomeCliente)
                .MinimumLength(3).WithMessage("minimo 3 caracteres")
                 .Must(SomenteLetras).WithMessage("Somente Letras")
                .MaximumLength(500).WithMessage("maximo 500 caracteres");

        }
        public static bool SomenteLetras(string palavra)
        {
            return Regex.IsMatch(palavra, @"^[a-zA-Z]+$");
        }

    }
}