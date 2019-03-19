using Assessment.Api.Models.Entities;
using Assessment.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Api.Services
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(StudentViewModel student);

        Task<Student> UpdateStudentAsync(StudentViewModel student);

        Task<List<Student>> AllStudentsAsync();

        Task<Student> StudentByIdAsync(Guid studentId);

    }
}
