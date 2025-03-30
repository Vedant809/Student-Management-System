using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Entity;
using StudentManagementSystem.Interface;
using System.Collections.Generic;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private const long MaxFileSize = 5 * 1024 * 1024;
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
        [HttpPost("GetAll")]
        public IActionResult Get(PaginationRequestDTO request)
        {
            var result = _service.GetAll(request);
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



        [HttpPost("bulk-import")]
        public async Task<IActionResult> BulkImport(IFormFile file)
        {
            // Validate file presence
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            // Validate file size
            if (file.Length > MaxFileSize)
            {
                return BadRequest("File size exceeds the 5 MB limit.");
            }

            var student = new List<StudentRequestDTO>();
            var errors = new List<string>();
            const int expectedColumnCount = 6; // Update this based on the expected number of columns

            try
            {
                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                {
                    int lineNumber = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;

                        // Skip header
                        if (lineNumber == 1)
                            continue;

                        var columns = line.Split(',');

                        // Validate column count
                        if (columns.Length != expectedColumnCount)
                        {
                            errors.Add($"Line {lineNumber}: Invalid column count.");
                            continue;
                        }

                        try
                        {
                            // Parse and validate student data
                            var student1 = new StudentRequestDTO
                            {
                                Id = int.Parse(columns[0]),
                                FirstName = columns[1].Trim(),
                                LastName = columns[2].Trim(),
                                EmailId = columns[3].Trim(),
                                ClassIds = columns[5]
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => int.TryParse(x, out var classId) ? classId : (int?)null)
    .Where(x => x.HasValue) // Filter out null values
    .Select(x => x.Value)    // Get the non-null values
    .ToList()

                            };

                            // Validate required fields
                            if (string.IsNullOrWhiteSpace(student1.FirstName) || string.IsNullOrWhiteSpace(student1.LastName) || string.IsNullOrWhiteSpace(student1.EmailId))
                            {
                                errors.Add($"Line {lineNumber}: Missing required fields.");
                                continue;
                            }

                            // Add to list if valid
                            student.Add(student1);
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Line {lineNumber}: {ex.Message}");
                        }
                    }
                }

                // Return errors if any
                if (errors.Any())
                {
                    return BadRequest(new
                    {
                        Message = "Some records have validation errors.",
                        Errors = errors
                    });
                }

                await _service.InsertStudentDetails(student);

                return Ok(new
                {
                    Message = $"{student.Count} students imported successfully.",
                    Students = student
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
