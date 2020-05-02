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
    public class ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler : IRequestHandler<ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery, NaoConformidadeViewModel[]>
    {
        private readonly GestaoNaoConformidadesDbContext _context;
        private readonly IMapper _mapper;

        public ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler(GestaoNaoConformidadesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NaoConformidadeViewModel[]> Handle(ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery request, CancellationToken cancellationToken)
        {
            var naoConformidades = await _context.NaoConformidades
                                                 .Include(nc => nc.NaoConformidadeDepartamentos)
                                                 .ThenInclude(ncd => ncd.Departamento)
                                                 .ToArrayAsync();

            if(!string.IsNullOrEmpty(request.Descricao))
            {
                naoConformidades = naoConformidades.Where(nc => nc.Descricao.Contains(request.Descricao))
                                                   .ToArray();
            }

            if(request.DataOcorrencia.HasValue)
            {
                naoConformidades = naoConformidades.Where(nc => nc.DataOcorrencia.Date == request.DataOcorrencia?.Date)
                                                   .ToArray();
            }

            if(request.Departamentos != null && request.Departamentos.Count() > 0)
            {
                naoConformidades = naoConformidades.Where(nc => request.Departamentos.Any(id => nc.NaoConformidadeDepartamentos.Select(ncd => ncd.DepartamentoID).Contains(id)))
                                                   .ToArray();
            }

            var vms = _mapper.Map<NaoConformidadeViewModel[]>(naoConformidades);

            return vms.OrderByDescending(vm => vm.DataOcorrencia)
                      .ToArray();
        }
    }
}
