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
    [Serializable]
    public class AcaoCorretivaViewModel : IHaveCustomMapping
    {
        public long AcaoCorretivaID { get; set; }

        public long NaoConformidadeID { get; set; }

        public string OqueFazer { get; set; }

        public string PorqueFazer { get; set; }

        public string ComoFazer { get; set; }

        public string OndeFazer { get; set; }

        public DateTime AteQuando { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AcaoCorretiva, AcaoCorretivaViewModel>();

            configuration.CreateMap<AcaoCorretivaViewModel, AcaoCorretivaDTO>();
        }
    }
}
