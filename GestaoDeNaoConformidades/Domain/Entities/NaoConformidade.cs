using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Domain.Entities
{
    [Serializable]
    [Table("NaoConformidade")]
    public class NaoConformidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NaoConformidadeID { get; set; }

        [Column]
        public string Descricao { get; set; }

        [Column]
        public DateTime DataOcorrencia { get; set; }

        public List<NaoConformidadeDepartamento> NaoConformidadeDepartamentos { get; set; }
        public List<AcaoCorretiva> AcaoCorretivas { get; set; }
    }
}
