using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Interface;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> Insert(List<StudentRequestDTO> request)
        {
            try
            {
                var result = await _service.InsertStudentDetails(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _service.GetAll();
            return Ok(result);
        }
        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(StudentRequestDTO request)
        {
            var result = await _service.UpdateStudent(request);
            return Ok(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var result = _service.GetById(Id);
            return Ok(result);
        }
    }
}
