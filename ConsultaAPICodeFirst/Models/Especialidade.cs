using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConsultaAPICodeFirst.Models
{
    public class Especialidade
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Categoria { get; set; }
    }
}
