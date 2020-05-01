using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.AtualizarAcaoCorretiva
{
    public class AtualizarAcaoCorretivaCommandValidator : AbstractValidator<AtualizarAcaoCorretivaCommand>
    {
        public AtualizarAcaoCorretivaCommandValidator()
        {
            RuleFor(c => c.AcaoCorretivaID)
                .GreaterThan(0)
                .WithMessage("Ação corretiva não informada.");

            RuleFor(c => c.NaoConformidadeID)
                .GreaterThan(0)
                .WithMessage("A ação corretiva deve estar vinculada a uma não conformidade.");

            RuleFor(c => c.AteQuando)
                .NotNull()
                .WithMessage("A data para a ação deve ser preenchida.")
                .GreaterThan(DateTime.MinValue)
                .WithMessage("A data para a ação deve ser preenchida.");

            RuleFor(c => c.ComoFazer)
                .NotEmpty()
                .WithMessage("Como fazer deve ser preenchido.");

            RuleFor(c => c.OndeFazer)
                .NotEmpty()
                .WithMessage("Onde fazer deve ser preenchido.")
                .MaximumLength(30)
                .WithMessage("Onde fazer não pode ser maior que 30 caracteres.");

            RuleFor(c => c.OqueFazer)
                .NotEmpty()
                .WithMessage("O que fazer deve ser preenchido.")
                .MaximumLength(50)
                .WithMessage("O que fazer não pode ser maior que 50 caracteres.");

            RuleFor(c => c.PorqueFazer)
                .NotEmpty()
                .WithMessage("Por que fazer deve ser preenchido.");
        }
    }
}
