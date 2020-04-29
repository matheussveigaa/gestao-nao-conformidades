using GestaoDeNaoConformidades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Domain.Repositories
{
    public interface INaoConformidadeRepository
    {
        NaoConformidade Inserir(NaoConformidade naoConformidade);
        NaoConformidade Atualizar(NaoConformidade naoConformidade);
        void Deletar(NaoConformidade naoConformidade);
        NaoConformidade ObterPorID(long naoConformidadeID);
    }
}
