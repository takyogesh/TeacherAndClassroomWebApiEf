using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersAndClassroomDll.Entities
{
    public class Classroom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string? ClassName { get; set; }
        public Teacher? Teacher { get; set; }

    }
}
