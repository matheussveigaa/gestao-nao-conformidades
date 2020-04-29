using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.InserirAcaoCorretiva
{
    public class InserirAcaoCorretivaCommand : IRequest
    {
        public InserirAcaoCorretivaCommand(long naoConformidadeID, string oqueFazer, string porqueFazer, string comoFazer, string ondeFazer, DateTime ateQuando)
        {
            NaoConformidadeID = naoConformidadeID;
            OqueFazer = oqueFazer;
            PorqueFazer = porqueFazer;
            ComoFazer = comoFazer;
            OndeFazer = ondeFazer;
            AteQuando = ateQuando;
        }

        public long NaoConformidadeID { get; set; }

        public string OqueFazer { get; set; }

        public string PorqueFazer { get; set; }

        public string ComoFazer { get; set; }

        public string OndeFazer { get; set; }

        public DateTime AteQuando { get; set; }
    }
}
