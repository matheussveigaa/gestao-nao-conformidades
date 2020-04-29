using GestaoDeNaoConformidades.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodasNaoConformidades
{
    public class ObterTodasNaoConformidadesQuery : IRequest<NaoConformidadeViewModel[]>
    {
    }
}
