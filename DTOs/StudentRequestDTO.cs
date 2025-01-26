using StudentManagementSystem.Entity;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTOs
{
    public class StudentRequestDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailId { get; set; }
        public List<int>? ClassIds { get; set; }
    }
    public class StudentResponseDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailId { get; set; }
        public List<SubjectRequestDTO>? StudentSubject { get; set; }
    }
}
