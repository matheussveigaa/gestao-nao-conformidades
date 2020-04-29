using FluentValidation;
using GestaoDeNaoConformidades.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade
{
    public class InserirNaoConformidadeCommand : IRequest
    {
        public InserirNaoConformidadeCommand(string descricao, DateTime dataOcorrencia, IEnumerable<DepartamentoViewModel> departamentos) 
        {
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Departamentos = departamentos;
        }

        public string Descricao { get; set; }

        public DateTime DataOcorrencia { get; set; }

        public IEnumerable<DepartamentoViewModel> Departamentos { get; set; }
    }
}
