using Assessment.Api.Models.Entities;
using Assessment.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Api.Services
{
    public interface IDatabaseService
    {
        Task<Student> AddStudentAsync(Student student);

        Task<Student> UpdateStudentAsync(Student student);

        Task<List<Student>> GetAllStudentsAsync();

        Task<Student> FindStudentByIdAsync(Guid studentId);
    }
}
