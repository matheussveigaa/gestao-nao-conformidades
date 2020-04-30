using GestaoDeNaoConformidades.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodasAcoesCorretivasPorNaoConformidade
{
    public class ObterTodasAcoesCorretivasPorNaoConformidadeQuery : IRequest<AcaoCorretivaViewModel[]>
    {
        public ObterTodasAcoesCorretivasPorNaoConformidadeQuery(long naoConformidadeID)
        {
            NaoConformidadeID = naoConformidadeID;
        }

        public long NaoConformidadeID { get; set; }
    }
}
