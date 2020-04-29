using FluentAssertions;
using GestaoDeNaoConformidades.Application.Queries.ObterTodosDepartamentos;
using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Infrastructure.Data;
using GestaoDeNaoConformidadesTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidadesTests.Application.Queries
{
    public class ObterTodosDepartamentosQueryTests
    {
        private GestaoNaoConformidadesDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = DbContextHelper.CreateContext();
        }

        [Test]
        public async Task Handle_DeveRetornarTodosOsDepartamentos()
        {
            var departamento1 = new Departamento()
            {
                Nome = "TI"
            };
            var departamento2 = new Departamento()
            {
                Nome = "Financeiro"
            };

            _context.Departamentos.AddRange(departamento1, departamento2);
            await _context.SaveChangesAsync();

            var query = new ObterTodosDepartamentosQuery();
            var handler = new ObterTodosDepartamentosQueryHandler(_context);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(2);
        }
    }
}
