using FluentAssertions;
using FluentValidation;
using GestaoDeNaoConformidades.Application.Commands.InserirAcaoCorretiva;
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
    public class InserirAcaoCorretivaCommandHandlerTests
    {
        private GestaoNaoConformidadesDbContext _context;
        private IAcaoCorretivaRepository _repository;

        [SetUp]
        public void Setup()
        {
            _context = DbContextHelper.CreateContext();
            _repository = new AcaoCorretivaRepository(_context);
        }

        [Test]
        public async Task Handler_DeveInserirAcaoCorretiva()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            await validator.ValidateAndThrowAsync(command);

            var handler = new InserirAcaoCorretivaCommandHandler(_repository);
            await handler.Handle(command, CancellationToken.None);

            _context.AcaoCorretivas.Should().HaveCount(1);
            var acaoCorretiva = _context.AcaoCorretivas.FirstOrDefault(a => a.NaoConformidadeID == naoConformidadeID && a.OqueFazer.Equals(oqueFazer) && a.OndeFazer.Equals(ondeFazer));
            
            acaoCorretiva.Should().NotBeNull();
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);
            acaoCorretiva.ComoFazer.Should().Be(comoFazer);
            acaoCorretiva.AteQuando.Should().Be(ateQuando);
        }

        [Test]
        public void Handler_DeveValidarSeAteQuandoFoiPreenchido()
        {
            var ateQuando = DateTime.MinValue;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeNaoConformidadeFoiPreenchida()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 0L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeOqueFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = string.Empty;
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSePorqueFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = string.Empty;
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeComoFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = string.Empty;
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeOndeFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = string.Empty;

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeOqueFazerTemNoMaximo50Caracteres()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "FormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatarFormatar computadorFormatar computadorFormatar computadorFormatar computadorFormatar computadorFormatar computadorFormatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Handler_DeveValidarSeOndeFazerTemNoMaximo30Caracteres()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeirofinanceirofinanceirofinanceirofinanceirofinanceirofinanceirofinanceiro";

            var command = new InserirAcaoCorretivaCommand(naoConformidadeID, oqueFazer, porqueFazer, comoFazer, ondeFazer, ateQuando);
            var validator = new InserirAcaoCorretivaCommandValidator();

            Func<Task> validationResult = () => validator.ValidateAndThrowAsync(command);

            validationResult.Should().ThrowExactly<ValidationException>();
        }
    }
}
