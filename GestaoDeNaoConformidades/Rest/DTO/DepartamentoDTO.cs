using GestaoDeNaoConformidades.Application.ViewModels;
using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Rest.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Rest.DTO
{
    [Serializable]
    public class DepartamentoDTO : IMapFrom<DepartamentoViewModel>
    {
        public long DepartamentoID { get; set; }
        public string Nome { get; set; }
    }
}
