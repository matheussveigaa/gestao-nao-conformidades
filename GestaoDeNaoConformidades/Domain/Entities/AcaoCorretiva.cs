using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Domain.Entities
{
    [Serializable]
    [Table("AcaoCorretiva")]
    public class AcaoCorretiva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AcaoCorretivaID { get; set; }

        [Column]
        public long NaoConformidadeID { get; set; }
        public virtual NaoConformidade NaoConformidade { get; set; }

        [Column]
        [MaxLength(50, ErrorMessage = "Oque fazer não pode ser maior que 50 caracteres.")]
        public string OqueFazer { get; set; }

        [Column]
        public string PorqueFazer { get; set; }

        [Column]
        public string ComoFazer { get; set; }

        [Column]
        [MaxLength(30, ErrorMessage = "Onde fazer não pode ser maior que 30 caracteres.")]
        public string OndeFazer { get; set; }

        [Column]
        public DateTime AteQuando { get; set; }
    }
}
