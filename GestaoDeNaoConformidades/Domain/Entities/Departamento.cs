using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Domain.Entities
{
    [Serializable]
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DepartamentoID { get; set; }

        [Column]
        [MaxLength(255, ErrorMessage = "Nome do departamento não pode ser maior que 255 caracteres.")]
        public string Nome { get; set; }

        public List<NaoConformidadeDepartamento> NaoConformidadeDepartamentos { get; set; }
    }
}
