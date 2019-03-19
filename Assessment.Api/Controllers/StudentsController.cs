using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Api.Models;
using Assessment.Api.Services;
using Assessment.Api.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assessment.Api.Controllers
{
    [ApiVersion("1")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class StudentsController : Controller
    {
        private readonly AssessmentDbContext _appDbContext;
        private readonly ILogger<StudentsController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentsController(AssessmentDbContext appDbContext
            ,IStudentService studentService 
            ,IMapper mapper
            , ILogger<StudentsController> logger)
        {
            _appDbContext = appDbContext;
            _studentService = studentService;
            _mapper = mapper;
            _logger = logger;
        }


        // GET api/v1/students
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            _logger.LogInformation("Student List api called!!");
            var students = await _studentService.AllStudentsAsync();
            var studentsViewModel = students.Select(r => _mapper.Map<StudentViewModel>(r));
            return new OkObjectResult(studentsViewModel);
        }

        // GET api/v1/students/<studentId>
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudent(Guid studentId)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var student = await _studentService.StudentByIdAsync(studentId);
            var studentViewModel = _mapper.Map<StudentViewModel>(student);
            return new OkObjectResult(studentViewModel);
        }

        // POST api/v1/students
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody]StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            var student = await _studentService.AddStudentAsync(studentViewModel);

            return new OkObjectResult(student);
        }

        // PUT api/v1/students/<studentId>
        [HttpPut("{studentId}")]
        public async Task<IActionResult> PutStudent(Guid studentId, [FromBody]StudentViewModel vmStudent)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            throw new NotImplementedException();
        }

        // DELETE api/v1/students/<studentId>
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            throw new NotImplementedException();
        }
    }
}
