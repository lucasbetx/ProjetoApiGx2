using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoAPI.Models.Entities
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome não pode ultrapassar 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public DateTime DataAlteracao { get; set; }
    }
}