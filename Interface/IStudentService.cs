using StudentManagementSystem.DTOs;
using StudentManagementSystem.Entity;

namespace StudentManagementSystem.Interface
{
    public interface IStudentService
    {
        Task<int> InsertStudentDetails(List<StudentRequestDTO> request);
        List<StudentResponseDTO> GetAll();
        Task<int> UpdateStudent(StudentRequestDTO request);
        StudentResponseDTO GetById(int Id);
    }
}
