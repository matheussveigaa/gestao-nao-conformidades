using GestaoDeNaoConformidades.Application.Exceptions;
using GestaoDeNaoConformidades.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.AtualizarAcaoCorretiva
{
    public class AtualizarAcaoCorretivaCommandHandler : IRequestHandler<AtualizarAcaoCorretivaCommand>
    {
        private readonly IAcaoCorretivaRepository _repository;

        public AtualizarAcaoCorretivaCommandHandler(IAcaoCorretivaRepository repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(AtualizarAcaoCorretivaCommand request, CancellationToken cancellationToken)
        {
            var acaoCorretiva = _repository.ObterPorID(request.AcaoCorretivaID);

            if (acaoCorretiva == null) throw new NotFoundException("Não foi encontrado a ação corretiva para atualização.");

            acaoCorretiva.NaoConformidadeID = request.NaoConformidadeID;
            acaoCorretiva.AteQuando = request.AteQuando;
            acaoCorretiva.ComoFazer = request.ComoFazer;
            acaoCorretiva.OndeFazer = request.OndeFazer;
            acaoCorretiva.OqueFazer = request.OqueFazer;
            acaoCorretiva.PorqueFazer = request.PorqueFazer;

            _repository.Atualizar(acaoCorretiva);

            return Unit.Task;
        }
    }
}
