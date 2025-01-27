using StudentManagementSystem.DTOs;
using StudentManagementSystem.Entity;
using StudentManagementSystem.Interface;
using StudentManagementSystem.Service;

namespace StudentManagementSystem.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertStudentDetails(List<StudentRequestDTO> request)
        {
            var result=0;
            List<Student> InsertList = new List<Student>();
            foreach(var item in request)
            {
                Student student = new Student();
                student.Id = item.Id;
                student.FirstName = item.FirstName;
                student.LastName = item.LastName;
                student.PhoneNumber = item.PhoneNumber;
                student.EmailId = item.EmailId;
                student.StudentSubject = item.ClassIds?.Select(x => new StudentSubject
                {
                    SubjectId = x
                }).ToList();
                InsertList.Add(student);
            }

            if (InsertList.Any())
            {
                result = await _repository.AddStudentInfo(InsertList);
                return result;
            }
            return result;
        }

        public PaginationResponse GetAll(PaginationRequestDTO request)
        {
            PaginationResponse final = new PaginationResponse();
            List<StudentResponseDTO> response = new List<StudentResponseDTO>();
            var list = _repository.GetAll().ToList();
            var searchingCondition = list
                .WhereIf(!string.IsNullOrEmpty(request.Search), x => x.FirstName == request.Search);
            var paginationContent = searchingCondition.Skip(request.Index * request.PageSize - request.PageSize).Take(request.PageSize);

            foreach(var item in paginationContent)
            {
                StudentResponseDTO res = new StudentResponseDTO();
                res.Id = item.Id;
                res.FirstName = item.FirstName;
                res.LastName = item.LastName;
                res.PhoneNumber = item.PhoneNumber;
                res.EmailId = item.EmailId;
                res.StudentSubject = item.StudentSubject?.Select(x => new SubjectRequestDTO
                {
                    Id = x.SubjectId,
                    Name = _repository.getName(x.SubjectId),
                    Description = _repository.getDescription(x.SubjectId)
                }).ToList();
                response.Add(res);
            }

            final.StudentDetails = response;
            final.Index = request.Index;
            final.PageSize = request.PageSize;
            final.PageNumber = request.Index;
            final.Count = paginationContent.Count();
            return final;
        }

        public async Task<int> UpdateStudent(StudentRequestDTO request)
        {
            var entity = _repository.GetAll();
            var existingentity = entity
                .Where(x => x.Id == request.Id).FirstOrDefault();
            
            if(existingentity == null)
            {
                return 0;
            }
            existingentity.FirstName = request.FirstName;
            existingentity.LastName = request.LastName;
            existingentity.PhoneNumber = request.PhoneNumber;
            existingentity.EmailId = request.EmailId;

            if (request.ClassIds != null)
            {
                //remove the classIds if not in request
                var existingClassAssignments = existingentity?.StudentSubject?.ToList();
                foreach (var existingClass in existingClassAssignments)
                {
                    if (!request.ClassIds.Contains(existingClass.SubjectId))
                    {
                        existingentity?.StudentSubject?.Remove(existingClass);
                    }
                }
                //add new classIds if not present
                foreach(var classId in request.ClassIds)
                {
                    if(!existingentity.StudentSubject.Select(x=>x.SubjectId == classId).Any())
                    {
                        existingentity.StudentSubject.Add(new StudentSubject
                        {
                            StudentId = existingentity.Id,
                            SubjectId = classId
                        });
                    }
                }
            }

            existingentity.StudentSubject = request.ClassIds?
                .Select(x => new StudentSubject
                {
                    SubjectId =x,
                    StudentId = existingentity.Id
                }).ToList();
            var result = await _repository.Update(existingentity);
            return result;
        }

        public StudentResponseDTO GetById(int Id)
        {
            var studentList = _repository.GetAll();
            var item = studentList.Where(x => x.Id == Id).FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            StudentResponseDTO res = new StudentResponseDTO();
            res.Id = item.Id;
            res.FirstName = item.FirstName;
            res.LastName = item.LastName;
            res.PhoneNumber = item.PhoneNumber;
            res.EmailId = item.EmailId;
            res.StudentSubject = item.StudentSubject?.Select(x => new SubjectRequestDTO
            {
                Id = x.SubjectId,
                Name = _repository.getName(x.SubjectId),
                Description = _repository.getDescription(x.SubjectId)
            }).ToList();
            return res;
        }
    }
}
