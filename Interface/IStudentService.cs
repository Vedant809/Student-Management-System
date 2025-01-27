using StudentManagementSystem.DTOs;
using StudentManagementSystem.Entity;

namespace StudentManagementSystem.Interface
{
    public interface IStudentService
    {
        Task<int> InsertStudentDetails(List<StudentRequestDTO> request);
        PaginationResponse GetAll(PaginationRequestDTO request);
        Task<int> UpdateStudent(StudentRequestDTO request);
        StudentResponseDTO GetById(int Id);
    }
}
