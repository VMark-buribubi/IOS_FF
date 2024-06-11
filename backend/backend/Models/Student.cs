using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Neptun { get; set; }
        private int? sumCredit;

        public int? SumCredit
        {
            get
            {
                int? sum = 0;
                if (Subjects is not null)
                {
                    foreach (var item in Subjects)
                    {
                        sum += item.Credit;
                    }
                }
                return sum;
            }
        }
        public string? Image { get; set; }

        [NotMapped]
        public virtual ICollection<Subject>? Subjects { get; set; }

        public Student()
        {
            Subjects = new HashSet<Subject>();
        }
    }
}
