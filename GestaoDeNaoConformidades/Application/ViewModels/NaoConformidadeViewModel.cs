using AutoMapper;
using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Rest.DTO;
using GestaoDeNaoConformidades.Rest.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.ViewModels
{
    public class NaoConformidadeViewModel : IHaveCustomMapping
    {
        public long NaoConformidadeID { get; set; }

        public string Descricao { get; set; }

        public DateTime DataOcorrencia { get; set; }

        public IEnumerable<DepartamentoViewModel> Departamentos { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<NaoConformidade, NaoConformidadeViewModel>()
                        .ForMember(d => d.Departamentos, opt => opt.MapFrom(src => src.NaoConformidadeDepartamentos.Select(nd => new DepartamentoViewModel { DepartamentoID = nd.DepartamentoID, Nome = nd.Departamento.Nome })));
            configuration.CreateMap<NaoConformidadeViewModel, NaoConformidadeDTO>();
        }
    }
}
