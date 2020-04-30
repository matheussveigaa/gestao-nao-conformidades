using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodasAcoesCorretivasPorNaoConformidade
{
    public class ObterTodasAcoesCorretivasPorNaoConformidadeQueryValidator : AbstractValidator<ObterTodasAcoesCorretivasPorNaoConformidadeQuery>
    {
        public ObterTodasAcoesCorretivasPorNaoConformidadeQueryValidator()
        {
            RuleFor(d => d.NaoConformidadeID)
                .GreaterThan(0)
                .WithMessage("A não conformidade deve ser informada!");
        }
    }
}
