using FluentAssertions;
using FluentValidation;
using GestaoDeNaoConformidades.Application.Commands.AtualizarAcaoCorretiva;
using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Domain.Repositories;
using GestaoDeNaoConformidades.Infrastructure.Data;
using GestaoDeNaoConformidades.Infrastructure.Data.Repositories;
using GestaoDeNaoConformidadesTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidadesTests.Application.Commands
{
    public class AtualizarAcaoCorretivaCommandHandlerTests
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
        public async Task Handler_DeveAtualizarAcaoCorretiva()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";
            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = novoPorqueFazer;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            validator.ValidateAndThrow(command);

            var handler = new AtualizarAcaoCorretivaCommandHandler(_repository);
            await handler.Handle(command, CancellationToken.None);

            var acaoCorretivaAtualizada = _repository.ObterPorID(acaoCorretiva.AcaoCorretivaID);
            acaoCorretivaAtualizada.Should().NotBeNull();

            acaoCorretivaAtualizada.PorqueFazer.Should().Be(novoPorqueFazer);
            acaoCorretivaAtualizada.OqueFazer.Should().Be(novoOqueFazer);
        }

        [Test]
        public async Task Handler_DeveValidarSeAcaoCorretivaFoiInformada()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";
            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = novoPorqueFazer;
            acaoCorretiva.AcaoCorretivaID = 0L;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public async Task Handler_DeveValidarSeNaoConformidadeFoiInformado()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";
            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = novoPorqueFazer;
            acaoCorretiva.NaoConformidadeID = 0L;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public async Task Handler_DeveValidarSeAteQuandoFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";
            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = novoPorqueFazer;
            acaoCorretiva.AteQuando = DateTime.MinValue;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public async Task Handler_DeveValidarSeComoFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";
            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = novoPorqueFazer;
            acaoCorretiva.ComoFazer = string.Empty;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public async Task Handler_DeveValidarSeOndeFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";
            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = novoPorqueFazer;
            acaoCorretiva.OndeFazer = string.Empty;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public async Task Handler_DeveValidarSeOqueFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoPorqueFazer = "teste123";

            acaoCorretiva.PorqueFazer = novoPorqueFazer;
            acaoCorretiva.OqueFazer = string.Empty;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public async Task Handler_DeveValidarSePorqueFazerFoiPreenchido()
        {
            var ateQuando = DateTime.Now;
            var naoConformidadeID = 1L;
            var oqueFazer = "Formatar computador";
            var porqueFazer = "Solicitação do gerente da área";
            var comoFazer = "Ve no google como que faz";
            var ondeFazer = "Departamento financeiro";

            var acaoCorretiva = new AcaoCorretiva()
            {
                AteQuando = ateQuando,
                NaoConformidadeID = naoConformidadeID,
                OqueFazer = oqueFazer,
                PorqueFazer = porqueFazer,
                ComoFazer = comoFazer,
                OndeFazer = ondeFazer
            };

            var acaoCorretivaEntry = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = acaoCorretivaEntry.Entity;

            await _context.SaveChangesAsync();

            acaoCorretiva.OqueFazer.Should().Be(oqueFazer);
            acaoCorretiva.PorqueFazer.Should().Be(porqueFazer);

            var novoOqueFazer = "atualizando...";

            acaoCorretiva.OqueFazer = novoOqueFazer;
            acaoCorretiva.PorqueFazer = string.Empty;

            var command = new AtualizarAcaoCorretivaCommand(
                acaoCorretiva.AcaoCorretivaID,
                acaoCorretiva.NaoConformidadeID,
                acaoCorretiva.OqueFazer,
                acaoCorretiva.PorqueFazer,
                acaoCorretiva.ComoFazer,
                acaoCorretiva.OndeFazer,
                acaoCorretiva.AteQuando
            );
            var validator = new AtualizarAcaoCorretivaCommandValidator();

            Func<Task> result = () => validator.ValidateAndThrowAsync(command);

            result.Should().ThrowExactly<ValidationException>();
        }
    }
}
