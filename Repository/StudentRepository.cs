using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Entity;
using StudentManagementSystem.Interface;

namespace StudentManagementSystem.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly APIDbContext _context;

        public StudentRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddStudentInfo(List<Student> entity)
        {
            await _context.Student.AddRangeAsync(entity);
            return await _context.SaveChangesAsync();
        }
        public IQueryable<Student> GetAll()
        {
            var list = _context.Student.Include(x => x.StudentSubject).AsQueryable();
            return list;
        }
        public string? getName(int Id)
        {
            var name = _context.subject?.Where(x => x.SubjectId == Id)?.FirstOrDefault()?.Name;
            return name;
        }
        public string? getDescription(int Id)
        {
            var name = _context.subject?.Where(x => x.SubjectId == Id)?.FirstOrDefault()?.Description;
            return name;
        }
        public async Task<int> Update(Student entity)
        {
            _context.Student.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
