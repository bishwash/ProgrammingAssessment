using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Api.Exceptions;
using Assessment.Api.Models;
using Assessment.Api.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Api.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly AssessmentDbContext _appDbContext;
        private readonly IMapper _mapper;

        public DatabaseService(AssessmentDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public async Task<Student> AddStudentAsync(Student studentToAdd)
        {
            //Assuming that duplicate entries are allowed we skipped on checking if the student already exists in the system
            if (studentToAdd.Id == null || studentToAdd.Id == Guid.Empty)
                studentToAdd.Id = Guid.NewGuid();

            var user = await _appDbContext.Student.AddAsync(studentToAdd);
            _appDbContext.SaveChanges();
            return studentToAdd;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _appDbContext.Student.ToListAsync();
            return students;
        }

        public async Task<Student> FindStudentByIdAsync(Guid studentId)
        {
            var student = await _appDbContext.Student.FindAsync(studentId);
            if (student == null)
                throw new Exception("Student with provided id cannot be found in the system");

            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student studentToUpdate)
        {
            if (studentToUpdate.Id == null)
                throw new Exception("Student Id cannot be null while updating");

            var student = await _appDbContext.Student.FindAsync(studentToUpdate.Id);
            if (student == null)
                throw new StudentNotFoundException(studentToUpdate.FirstName + " " + studentToUpdate.LastName);

            student.FirstName = studentToUpdate.FirstName;
            student.LastName = studentToUpdate.LastName;
            student.DOB = studentToUpdate.DOB;
            student.GPA = studentToUpdate.GPA;
            _appDbContext.SaveChanges();

            return student;
        }
    }
}
