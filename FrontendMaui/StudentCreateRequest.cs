using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public class StudentCreateRequest
    {
        public string? Id { get; set; }
        [Required]
        //[StringLength(30, MinimumLength = 3)]
        public string Name { get; set; } = "";

        [Required]
        //[StringLength(6,MinimumLength =6)]
        public string Neptun { get; set; } = "";

    }
}
