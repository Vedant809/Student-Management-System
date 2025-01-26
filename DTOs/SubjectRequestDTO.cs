using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTOs
{
    public class SubjectRequestDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
