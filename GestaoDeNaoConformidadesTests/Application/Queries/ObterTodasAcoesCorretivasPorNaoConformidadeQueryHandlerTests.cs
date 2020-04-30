using AutoMapper;
using FluentAssertions;
using FluentValidation;
using GestaoDeNaoConformidades.Application.Queries.ObterTodasAcoesCorretivasPorNaoConformidade;
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
    public class ObterTodasAcoesCorretivasPorNaoConformidadeQueryHandlerTests
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
        public async Task Handler_DeveRetornarTodasAcoesCorretivasPorNaoConformidade()
        {
            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste",
            };

            var naoConformidadeEntry = _context.NaoConformidades.Add(naoConformidade);
            naoConformidade = naoConformidadeEntry.Entity;

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = DateTime.Now,
                ComoFazer = "teste",
                OndeFazer = "teste",
                OqueFazer = "teste",
                PorqueFazer = "teste",
                NaoConformidadeID = naoConformidade.NaoConformidadeID
            };
            var acaoCorretiva2 = new AcaoCorretiva()
            {
                AteQuando = DateTime.Now,
                ComoFazer = "teste",
                OndeFazer = "teste",
                OqueFazer = "teste",
                PorqueFazer = "teste",
                NaoConformidadeID = naoConformidade.NaoConformidadeID
            };

            _context.AcaoCorretivas.AddRange(acaoCorretiva, acaoCorretiva2);

            await _context.SaveChangesAsync();

            var query = new ObterTodasAcoesCorretivasPorNaoConformidadeQuery(naoConformidade.NaoConformidadeID);
            var validator = new ObterTodasAcoesCorretivasPorNaoConformidadeQueryValidator();

            validator.ValidateAndThrow(query);

            var handler = new ObterTodasAcoesCorretivasPorNaoConformidadeQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(2);
        }

        [Test]
        public async Task Handler_DeveValidarSeNaoConformidadeFoiInformada()
        {
            var naoConformidade = new NaoConformidade()
            {
                DataOcorrencia = DateTime.Now,
                Descricao = "Teste",
            };

            var naoConformidadeEntry = _context.NaoConformidades.Add(naoConformidade);
            naoConformidade = naoConformidadeEntry.Entity;

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = DateTime.Now,
                ComoFazer = "teste",
                OndeFazer = "teste",
                OqueFazer = "teste",
                PorqueFazer = "teste",
                NaoConformidadeID = naoConformidade.NaoConformidadeID
            };
            var acaoCorretiva2 = new AcaoCorretiva()
            {
                AteQuando = DateTime.Now,
                ComoFazer = "teste",
                OndeFazer = "teste",
                OqueFazer = "teste",
                PorqueFazer = "teste",
                NaoConformidadeID = naoConformidade.NaoConformidadeID
            };

            _context.AcaoCorretivas.AddRange(acaoCorretiva, acaoCorretiva2);

            await _context.SaveChangesAsync();

            var query = new ObterTodasAcoesCorretivasPorNaoConformidadeQuery(0L);
            var validator = new ObterTodasAcoesCorretivasPorNaoConformidadeQueryValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(query);

            result.Should().ThrowExactly<ValidationException>();
        }
    }
}
