using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersAndClassroomDll.Entities
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Varchar(10)")]
        public string? FirstName { get; set; }
        [Column(TypeName = "Varchar(10)")]
        public string? LastName { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string? Address { get; set; }
        public ICollection<Classroom>? Classroom { get; set; }
    }
}
