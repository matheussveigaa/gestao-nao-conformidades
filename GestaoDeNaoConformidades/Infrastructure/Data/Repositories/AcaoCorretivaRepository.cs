using GestaoDeNaoConformidades.Domain.Entities;
using GestaoDeNaoConformidades.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Infrastructure.Data.Repositories
{
    public class AcaoCorretivaRepository : IAcaoCorretivaRepository
    {
        private readonly GestaoNaoConformidadesDbContext _context;

        public AcaoCorretivaRepository(GestaoNaoConformidadesDbContext context)
        {
            _context = context;
        }

        public AcaoCorretiva Atualizar(AcaoCorretiva acaoCorretiva)
        {
            var result = _context.AcaoCorretivas.Update(acaoCorretiva);
            acaoCorretiva = result.Entity;

            _context.SaveChanges();

            return acaoCorretiva;
        }

        public void Deletar(AcaoCorretiva acaoCorretiva)
        {
            _context.AcaoCorretivas.Remove(acaoCorretiva);

            _context.SaveChanges();
        }

        public AcaoCorretiva Inserir(AcaoCorretiva acaoCorretiva)
        {
            var result = _context.AcaoCorretivas.Add(acaoCorretiva);
            acaoCorretiva = result.Entity;

            _context.SaveChanges();

            return acaoCorretiva;
        }

        public AcaoCorretiva ObterPorID(long acaoCorretivaID)
        {
            var acaoCorretiva = _context.AcaoCorretivas.FirstOrDefault(nc => nc.AcaoCorretivaID == acaoCorretivaID);

            return acaoCorretiva;
        }
    }
}
