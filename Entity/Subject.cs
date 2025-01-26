using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Entity
{
    [Table("Class")]
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }
        public virtual ICollection<StudentSubject>? StudentSubject { get; set; }
    }
}
