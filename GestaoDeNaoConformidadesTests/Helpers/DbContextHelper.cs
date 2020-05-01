using GestaoDeNaoConformidades.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoDeNaoConformidadesTests.Helpers
{
    public class DbContextHelper
    {
        public static GestaoNaoConformidadesDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<GestaoNaoConformidadesDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new GestaoNaoConformidadesDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
