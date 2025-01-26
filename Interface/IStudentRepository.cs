using StudentManagementSystem.DTOs;
using StudentManagementSystem.Entity;

namespace StudentManagementSystem.Interface
{
    public interface IStudentRepository
    {
        Task<int> AddStudentInfo(List<Student> entity);
        IQueryable<Student> GetAll();
        string? getName(int Id);
        string? getDescription(int Id);
        Task<int> Update(Student entity);
    }
}
