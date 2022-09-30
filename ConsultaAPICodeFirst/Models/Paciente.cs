using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConsultaAPICodeFirst.Models
{
    public class Paciente
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }

        [StringLength(30)]
        public string Carteirinha { get; set; }

        [Required]
        public DateTime DtNascimento { get; set; }

        [Required]
        public bool Ativo { get; set; }


        [ForeignKey("Usuario")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Consulta> Consultas { get; set; }
    }
}
