using GestaoDeNaoConformidades.Application.Commands.InserirAcaoCorretiva;
using GestaoDeNaoConformidades.Rest.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Rest.DTO
{
    [Serializable]
    public class AcaoCorretivaDTO : IMapFrom<InserirAcaoCorretivaCommand>
    {
        public long AcaoCorretivaID { get; set; }

        public long NaoConformidadeID { get; set; }

        public string OqueFazer { get; set; }

        public string PorqueFazer { get; set; }

        public string ComoFazer { get; set; }

        public string OndeFazer { get; set; }

        public DateTime AteQuando { get; set; }
    }
}
