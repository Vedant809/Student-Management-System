using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Entity
{
    [Table("Student")]
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "PhoneNumber must be a 10-digit numeric value.")]

        public string? PhoneNumber { get; set; }
        [Required]
        public string? EmailId { get; set; }
        public virtual ICollection<StudentSubject>? StudentSubject { get; set; }

    }
}
