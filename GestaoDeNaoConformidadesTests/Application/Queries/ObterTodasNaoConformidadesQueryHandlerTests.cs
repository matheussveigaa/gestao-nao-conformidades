using AutoMapper;
using FluentAssertions;
using GestaoDeNaoConformidades.Application.Queries.ObterTodasNaoConformidades;
using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Infrastructure.Data;
using GestaoDeNaoConformidades.Rest.Map;
using GestaoDeNaoConformidadesTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidadesTests.Application.Queries
{
    public class ObterTodasNaoConformidadesQueryHandlerTests
    {
        private IMapper _mapper;
        private GestaoNaoConformidadesDbContext _context;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddMaps(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
            });

            _mapper = config.CreateMapper();
            _context = DbContextHelper.CreateContext();
        }

        [Test]
        public async Task Handler_DeveRetornarTodasAsNaoConformidades()
        {
            var departamento = new Departamento()
            {
                DepartamentoID = 1,
                Nome = "TI"
            };
            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 1 } }
            };
            var naoConformidade2 = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 1 } }
            };

            _context.Departamentos.Add(departamento);
            _context.NaoConformidades.AddRange(naoConformidade, naoConformidade2);

            await _context.SaveChangesAsync();

            var query = new ObterTodasNaoConformidadesQuery();
            var handler = new ObterTodasNaoConformidadesQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(2);
        }
    }
}
