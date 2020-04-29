using GestaoDeNaoConformidades.Rest.DTO;
using GestaoDeNaoConformidades.Rest.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.ViewModels
{
    [Serializable]
    public class DepartamentoViewModel : IMapFrom<DepartamentoDTO>
    {
        public long DepartamentoID { get; set; }
        public string Nome { get; set; }
    }
}
