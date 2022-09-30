using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaAPICodeFirst.Models
{
    public class Usuario
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        private string _email;

        [Required]
        [StringLength(255)]
        public string Email {
            get { return _email; }
            set
            {
                if (!value.Contains('@'))
                    throw new FormatException("Email inválido");

                _email = value; 
            }
        }

        private string _senha;

        [Required]
        public string Senha { 
            get { return _senha; }
            set
            {
                if (value.Length < 3)
                    throw new FormatException("Senha inválida - A senha deve ter 3 ou mais caracteres");

                _senha = value;

            }
        }

        [ForeignKey("TipoUsuario")]
        public int IdTipoUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]    
        public TipoUsuario TipoUsuario { get; set; }
    }
}
