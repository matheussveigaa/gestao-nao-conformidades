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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidadesTests.Application.Queries
{
    public class ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandlerTests
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
        public async Task Handler_DeveRetornarTodasNaoConformidadesSeNaoInformadoNenhumFiltro()
        {
            var departamento = new Departamento()
            {
                DepartamentoID = 100,
                Nome = "TI"
            };
            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 100 } }
            };
            var naoConformidade2 = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 100 } }
            };

            _context.Departamentos.Add(departamento);
            _context.NaoConformidades.AddRange(naoConformidade, naoConformidade2);

            await _context.SaveChangesAsync();

            var query = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery();
            var handler = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(2);
        }

        [Test]
        public async Task Handler_DeveRetornarSomenteNaoConformidadesComDescricaoIgualAoFiltro()
        {
            var departamento = new Departamento()
            {
                DepartamentoID = 12,
                Nome = "TI"
            };
            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 12 } }
            };
            var naoConformidade2 = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 12 } }
            };
            var naoConformidade3 = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 12 } }
            };

            _context.Departamentos.Add(departamento);
            _context.NaoConformidades.AddRange(naoConformidade, naoConformidade2, naoConformidade3);

            await _context.SaveChangesAsync();

            var query = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery();
            query.Descricao = "Teste2";

            var handler = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(2);

            result.Count(nc => nc.Descricao.Equals(query.Descricao)).Should().Be(2);
        }

        [Test]
        public async Task Handler_DeveRetornarSomenteNaoConformidadesComDataOcorrenciaIgualAoFiltro()
        {
            var dataAtual = DateTime.Now;

            var departamento = new Departamento()
            {
                DepartamentoID = 14,
                Nome = "TI"
            };
            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now.AddDays(1),
                Descricao = "Teste",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 14 } }
            };
            var naoConformidade2 = new NaoConformidade()
            {
                DataOcorrencia = dataAtual,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 14 } }
            };
            var naoConformidade3 = new NaoConformidade()
            {
                DataOcorrencia = dataAtual,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 14 } }
            };

            _context.Departamentos.Add(departamento);
            _context.NaoConformidades.AddRange(naoConformidade, naoConformidade2, naoConformidade3);

            await _context.SaveChangesAsync();

            var query = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery();
            query.DataOcorrencia = dataAtual;

            var handler = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(2);

            result.Count(nc => nc.DataOcorrencia == query.DataOcorrencia).Should().Be(2);
        }

        [Test]
        public async Task Handler_DeveRetornarSomenteNaoConformidadesComDepartamentosIgualAoFiltro()
        {
            var departamento = new Departamento()
            {
                DepartamentoID = 15,
                Nome = "TI"
            };
            var departamento1 = new Departamento()
            {
                DepartamentoID = 16,
                Nome = "Financeiro"
            };

            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now.AddDays(1),
                Descricao = "Teste",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 15 } }
            };
            var naoConformidade2 = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 16 } }
            };
            var naoConformidade3 = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 15 } }
            };

            _context.Departamentos.AddRange(departamento, departamento1);
            _context.NaoConformidades.AddRange(naoConformidade, naoConformidade2, naoConformidade3);

            await _context.SaveChangesAsync();

            var query = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery();
            query.Departamentos = new[] { 15L, 16L };

            var handler = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(3);

            result.Count(nc => nc.Departamentos.Any(d => nc.Departamentos.Select(ncd => ncd.DepartamentoID).Contains(d.DepartamentoID))).Should().Be(3);
        }

        [Test]
        public async Task Handler_DeveRetornarSomenteNaoConformidadesIgualAoFiltroInformado()
        {
            var dataAtual = DateTime.Now;

            var departamento = new Departamento()
            {
                DepartamentoID = 14,
                Nome = "TI"
            };
            var departamento1 = new Departamento()
            {
                DepartamentoID = 16,
                Nome = "Financeiro"
            };

            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now.AddDays(1),
                Descricao = "Teste",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 14 } }
            };
            var naoConformidade2 = new NaoConformidade()
            {
                DataOcorrencia = dataAtual,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 16 } }
            };
            var naoConformidade3 = new NaoConformidade()
            {
                DataOcorrencia = dataAtual,
                Descricao = "Teste2",
                NaoConformidadeDepartamentos = new List<NaoConformidadeDepartamento> { new NaoConformidadeDepartamento { DepartamentoID = 14 } }
            };

            _context.Departamentos.AddRange(departamento, departamento1);
            _context.NaoConformidades.AddRange(naoConformidade, naoConformidade2, naoConformidade3);

            await _context.SaveChangesAsync();

            var query = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery();
            query.DataOcorrencia = dataAtual;
            query.Departamentos = new[] { 16, 14L };
            query.Descricao = "Teste";

            var handler = new ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCountGreaterOrEqualTo(2);
        }
    }
}
