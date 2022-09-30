using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaAPICodeFirst.Models
{
    public class Consulta
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }


        [ForeignKey("Medico")]
        public int IdMedico { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Medico Medico { get; set; }


        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Paciente Paciente { get; set; }
    }
}
