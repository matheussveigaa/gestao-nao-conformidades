using GestaoDeNaoConformidades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Domain.Repositories
{
    public interface IAcaoCorretivaRepository
    {
        AcaoCorretiva Inserir(AcaoCorretiva acaoCorretiva);
        AcaoCorretiva Atualizar(AcaoCorretiva acaoCorretiva);
        void Deletar(AcaoCorretiva acaoCorretiva);
        AcaoCorretiva ObterPorID(long acaoCorretivaID);
    }
}
