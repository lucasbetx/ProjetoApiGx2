using ProjetoAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoAPI.Models
{
    [Table("Usuarios")]
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome não pode ultrapassar 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Necessario um e-mail valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public bool UsuarioAdm { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public bool Ativo { get; set; }
    }
}