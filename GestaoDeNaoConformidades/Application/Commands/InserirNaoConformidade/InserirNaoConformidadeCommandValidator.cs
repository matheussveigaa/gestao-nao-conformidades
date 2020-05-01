using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade
{
    public class InserirNaoConformidadeCommandValidator : AbstractValidator<InserirNaoConformidadeCommand>
    {
        public InserirNaoConformidadeCommandValidator()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("Você deve informar uma descrição para a ocorrência.");

            RuleFor(c => c.DataOcorrencia)
                .NotNull()
                .WithMessage("Você deve informar a data da ocorrência.")
                .GreaterThan(DateTime.MinValue)
                .WithMessage("Você deve informar uma data válida.");

            RuleFor(c => c.Departamentos)
                .Must(departamentos => departamentos != null && departamentos.Count() > 0)
                .WithMessage("Você deve informar pelo menos um departamento para a ocorrência.");
        }
    }
}
