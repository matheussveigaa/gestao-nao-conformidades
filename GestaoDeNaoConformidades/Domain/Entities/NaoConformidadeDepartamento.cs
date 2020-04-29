using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Domain.Entities
{
    [Serializable]
    [Table("NaoConformidadeDepartamento")]
    public class NaoConformidadeDepartamento
    {
        public long NaoConformidadeID { get; set; }
        public NaoConformidade NaoConformidade { get; set; }

        public long DepartamentoID { get; set; }
        public Departamento Departamento { get; set; }
    }
}
