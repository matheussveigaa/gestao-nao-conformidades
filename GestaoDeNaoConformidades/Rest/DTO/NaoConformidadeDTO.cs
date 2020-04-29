using AutoMapper;
using GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade;
using GestaoDeNaoConformidades.Rest.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Rest.DTO
{
    [Serializable]
    public class NaoConformidadeDTO : IHaveCustomMapping
    {
        public long NaoConformidadeID { get; set; }

        public string Descricao { get; set; }

        public DateTime DataOcorrencia { get; set; }

        public IEnumerable<DepartamentoDTO> Departamentos { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<NaoConformidadeDTO, InserirNaoConformidadeCommand>()
                .ForMember(cmd => cmd.Departamentos, opt => opt.MapFrom(dto => dto.Departamentos.Select(d => d.DepartamentoID)));
        }
    }
}
