using GestaoDeNaoConformidades.Application.ViewModels;
using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodosDepartamentos
{
    public class ObterTodosDepartamentosQueryHandler : IRequestHandler<ObterTodosDepartamentosQuery, DepartamentoViewModel[]>
    {
        private readonly GestaoNaoConformidadesDbContext _context;

        public ObterTodosDepartamentosQueryHandler(GestaoNaoConformidadesDbContext context)
        {
            _context = context;
        }

        public Task<DepartamentoViewModel[]> Handle(ObterTodosDepartamentosQuery request, CancellationToken cancellationToken)
        {
            return _context.Departamentos
                .Select(d => new DepartamentoViewModel { DepartamentoID = d.DepartamentoID, Nome = d.Nome })
                .ToArrayAsync();
        }
    }
}
