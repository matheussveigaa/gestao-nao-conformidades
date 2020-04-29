using FluentAssertions;
using FluentValidation;
using GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade;
using GestaoDeNaoConformidades.Application.ViewModels;
using GestaoDeNaoConformidades.Domain.Repositories;
using GestaoDeNaoConformidades.Infrastructure.Data;
using GestaoDeNaoConformidades.Infrastructure.Data.Repositories;
using GestaoDeNaoConformidadesTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidadesTests.Application.Commands
{
    
    public class InserirNaoConformidadeCommandHandlerTests
    {
        private GestaoNaoConformidadesDbContext _context;
        private INaoConformidadeRepository _repository;

        [SetUp]
        public void Setup()
        {
            _context = DbContextHelper.CreateContext();
            _repository = new NaoConformidadeRepository(_context);
        }

        [Test]
        public async Task Handler_DeveInserirNaoConformidade()
        {
            var descricao = "Uma descrição teste para nao conformidade";
            var dataOcorrencia = DateTime.Now;
            var departamentos = new DepartamentoViewModel[] { new DepartamentoViewModel { DepartamentoID = 1}, new DepartamentoViewModel { DepartamentoID = 2 } };

            var command = new InserirNaoConformidadeCommand(descricao, dataOcorrencia, departamentos);
            var validator = new InserirNaoConformidadeCommandValidator();

            Func<Task> validationTask = () => validator.ValidateAndThrowAsync(command);
            validationTask.Should().NotThrow<ValidationException>();

            var handle = new InserirNaoConformidadeCommandHandler(_repository);

            await handle.Handle(command, CancellationToken.None);

            _context.NaoConformidades.Should().HaveCount(1);

            var naoConformidade = _context.NaoConformidades.FirstOrDefault(nc => nc.Descricao.Equals(descricao));
            naoConformidade.Should().NotBeNull();
        }

        [Test]
        public void Handler_DeveValidarSeDescricaoFoiPreenchido()
        {
            var descricao = string.Empty;
            var dataOcorrencia = DateTime.Now;
            var departamentos = new DepartamentoViewModel[] { new DepartamentoViewModel { DepartamentoID = 1 }, new DepartamentoViewModel { DepartamentoID = 2 } };

            var command = new InserirNaoConformidadeCommand(descricao, dataOcorrencia, departamentos);
            var validator = new InserirNaoConformidadeCommandValidator();

            Func<Task> validationTask = () => validator.ValidateAndThrowAsync(command);
            validationTask.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeDataOcorrenciaFoiPreenchidaCorretamente()
        {
            var descricao = "Uma descrição teste para nao conformidade";
            var dataOcorrencia = DateTime.MinValue;
            var departamentos = new DepartamentoViewModel[] { new DepartamentoViewModel { DepartamentoID = 1 }, new DepartamentoViewModel { DepartamentoID = 2 } };

            var command = new InserirNaoConformidadeCommand(descricao, dataOcorrencia, departamentos);
            var validator = new InserirNaoConformidadeCommandValidator();

            Func<Task> validationTask = () => validator.ValidateAndThrowAsync(command);
            validationTask.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSePeloMenosUmDepartamentoFoiSelecionado()
        {
            var descricao = "Uma descrição teste para nao conformidade";
            var dataOcorrencia = DateTime.Now;
            DepartamentoViewModel[] departamentos = { };

            var command = new InserirNaoConformidadeCommand(descricao, dataOcorrencia, departamentos);
            var validator = new InserirNaoConformidadeCommandValidator();

            Func<Task> validationTask = () => validator.ValidateAndThrowAsync(command);
            validationTask.Should().ThrowExactly<ValidationException>();
        }
    }
}
