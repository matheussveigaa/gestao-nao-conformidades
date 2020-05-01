using GestaoDeNaoConformidades.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodasNaoConformidades
{
    public class ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery : IRequest<NaoConformidadeViewModel[]>
    {
        public DateTime? DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<long> Departamentos { get; set; }
    }
}
