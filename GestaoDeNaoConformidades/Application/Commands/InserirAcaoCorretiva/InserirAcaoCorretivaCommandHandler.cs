using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.InserirAcaoCorretiva
{
    public class InserirAcaoCorretivaCommandHandler : IRequestHandler<InserirAcaoCorretivaCommand>
    {
        private readonly IAcaoCorretivaRepository _repository;

        public InserirAcaoCorretivaCommandHandler(IAcaoCorretivaRepository repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(InserirAcaoCorretivaCommand request, CancellationToken cancellationToken)
        {
            var acaoCorretiva = new AcaoCorretiva();
            acaoCorretiva.NaoConformidadeID = request.NaoConformidadeID;
            acaoCorretiva.AteQuando = request.AteQuando;
            acaoCorretiva.ComoFazer = request.ComoFazer;
            acaoCorretiva.OndeFazer = request.OndeFazer;
            acaoCorretiva.OqueFazer = request.OqueFazer;
            acaoCorretiva.PorqueFazer = request.PorqueFazer;

            _repository.Inserir(acaoCorretiva);

            return Unit.Task;
        }
    }
}
