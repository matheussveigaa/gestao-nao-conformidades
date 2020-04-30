using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Commands.AtualizarAcaoCorretiva
{
    public class AtualizarAcaoCorretivaCommand : IRequest
    {
        public AtualizarAcaoCorretivaCommand(long acaoCorretivaID, long naoConformidadeID, string oqueFazer, string porqueFazer, string comoFazer, string ondeFazer, DateTime ateQuando)
        {
            AcaoCorretivaID = acaoCorretivaID;
            NaoConformidadeID = naoConformidadeID;
            OqueFazer = oqueFazer;
            PorqueFazer = porqueFazer;
            ComoFazer = comoFazer;
            OndeFazer = ondeFazer;
            AteQuando = ateQuando;
        }

        public long AcaoCorretivaID { get; set; }

        public long NaoConformidadeID { get; set; }

        public string OqueFazer { get; set; }

        public string PorqueFazer { get; set; }

        public string ComoFazer { get; set; }

        public string OndeFazer { get; set; }

        public DateTime AteQuando { get; set; }
    }
}
