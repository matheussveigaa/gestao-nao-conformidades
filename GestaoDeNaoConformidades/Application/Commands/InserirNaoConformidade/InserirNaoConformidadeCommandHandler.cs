using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade
{
    public class InserirNaoConformidadeCommandHandler : IRequestHandler<InserirNaoConformidadeCommand>
    {
        private readonly INaoConformidadeRepository _repository;

        public InserirNaoConformidadeCommandHandler(INaoConformidadeRepository repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(InserirNaoConformidadeCommand request, CancellationToken cancellationToken)
        {
            var naoConformidade = new NaoConformidade();
            naoConformidade.DataOcorrencia = request.DataOcorrencia;
            naoConformidade.Descricao = request.Descricao;
            naoConformidade.NaoConformidadeDepartamentos = request.Departamentos.Select(d => new NaoConformidadeDepartamento { DepartamentoID = d.DepartamentoID }).ToList();

            _repository.Inserir(naoConformidade);

            return Unit.Task;
        }
    }
}
