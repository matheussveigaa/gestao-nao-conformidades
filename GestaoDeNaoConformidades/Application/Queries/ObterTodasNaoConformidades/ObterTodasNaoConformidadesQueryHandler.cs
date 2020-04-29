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

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodasNaoConformidades
{
    public class ObterTodasNaoConformidadesQueryHandler : IRequestHandler<ObterTodasNaoConformidadesQuery, NaoConformidadeViewModel[]>
    {
        private readonly GestaoNaoConformidadesDbContext _context;
        private readonly IMapper _mapper;

        public ObterTodasNaoConformidadesQueryHandler(GestaoNaoConformidadesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NaoConformidadeViewModel[]> Handle(ObterTodasNaoConformidadesQuery request, CancellationToken cancellationToken)
        {
            var naoConformidades = await _context.NaoConformidades
                                                 .Include(nc => nc.NaoConformidadeDepartamentos)
                                                 .ThenInclude(ncd => ncd.Departamento)
                                                 .ToArrayAsync();
            var vms = _mapper.Map<NaoConformidadeViewModel[]>(naoConformidades);

            return vms.OrderByDescending(v => v.DataOcorrencia)
                      .ToArray();
        }
    }
}
