using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Neptun { get; set; }
        public int? Credit { get; set; }
        public bool? Exam { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public virtual ICollection<Teacher>? Teachers { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Student? Student { get; set; }
        public string? StudentId { get; set; }

        public Subject()
        {
            Teachers = new List<Teacher>();
        }
    }
}
