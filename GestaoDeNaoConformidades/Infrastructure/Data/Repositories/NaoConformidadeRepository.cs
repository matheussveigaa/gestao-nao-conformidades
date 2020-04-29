using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Infrastructure.Data.Repositories
{
    public class NaoConformidadeRepository : INaoConformidadeRepository
    {
        private readonly GestaoNaoConformidadesDbContext _context;

        public NaoConformidadeRepository(GestaoNaoConformidadesDbContext context)
        {
            _context = context;
        }

        public NaoConformidade Atualizar(NaoConformidade naoConformidade)
        {
            var result = _context.NaoConformidades.Update(naoConformidade);
            naoConformidade = result.Entity;

            _context.SaveChanges();

            return naoConformidade;
        }

        public void Deletar(NaoConformidade naoConformidade)
        {
            _context.NaoConformidades.Remove(naoConformidade);

            _context.SaveChanges();
        }

        public NaoConformidade Inserir(NaoConformidade naoConformidade)
        {
            var result = _context.NaoConformidades.Add(naoConformidade);
            naoConformidade = result.Entity;

            _context.SaveChanges();

            return naoConformidade;
        }

        public NaoConformidade ObterPorID(long naoConformidadeID)
        {
            var naoConformidade = _context.NaoConformidades.FirstOrDefault(nc => nc.NaoConformidadeID == naoConformidadeID);

            return naoConformidade;
        }
    }
}
