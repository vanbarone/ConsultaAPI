using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaAPICodeFirst.Models
{
    public class Medico
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }


        [StringLength(20)]
        public string CRM { get; set; }

        
        [ForeignKey("Especialidade")]
        public int IdEspecialidade { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]  
        public Especialidade Especialidade { get; set; }

        
        [ForeignKey("Usuario")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Consulta> Consultas { get; set; }
    }
}
