using GestaoDeNaoConformidades.Application.Queries.ObterTodasNaoConformidades;
using GestaoDeNaoConformidades.Rest.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Rest.DTO
{
    [Serializable]
    public class NaoConformidadeFiltroPorDataOcorrenciaDescricaoDepartamentosDTO : IMapFrom<ObterTodasNaoConformidadesPorDataOcorrenciaOuDescricaoOuDepartamentosQuery>
    {
        public DateTime? DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<long> Departamentos { get; set; }
    }
}
