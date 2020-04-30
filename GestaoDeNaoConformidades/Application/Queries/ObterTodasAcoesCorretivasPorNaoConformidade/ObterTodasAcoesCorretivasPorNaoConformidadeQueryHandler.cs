using AutoMapper;
using GestaoDeNaoConformidades.Application.ViewModels;
using GestaoDeNaoConformidades.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodasAcoesCorretivasPorNaoConformidade
{
    public class ObterTodasAcoesCorretivasPorNaoConformidadeQueryHandler : IRequestHandler<ObterTodasAcoesCorretivasPorNaoConformidadeQuery, AcaoCorretivaViewModel[]>
    {
        private readonly GestaoNaoConformidadesDbContext _context;
        private readonly IMapper _mapper;

        public ObterTodasAcoesCorretivasPorNaoConformidadeQueryHandler(GestaoNaoConformidadesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AcaoCorretivaViewModel[]> Handle(ObterTodasAcoesCorretivasPorNaoConformidadeQuery request, CancellationToken cancellationToken)
        {
            var acoes = await _context.AcaoCorretivas
                                .Where(a => a.NaoConformidadeID == request.NaoConformidadeID)
                                .ToArrayAsync();

            var vms = _mapper.Map<AcaoCorretivaViewModel[]>(acoes);

            return vms.OrderByDescending(vm => vm.AteQuando)
                      .ToArray();
        }
    }
}
