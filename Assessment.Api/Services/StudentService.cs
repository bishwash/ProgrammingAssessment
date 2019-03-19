using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Api.Exceptions;
using Assessment.Api.Models;
using Assessment.Api.Models.Entities;
using Assessment.Api.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IDatabaseService _databaseContext;
        private readonly IMapper _mapper;

        public StudentService()
        {
        }

        public StudentService(IDatabaseService databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }


        public async Task<Student> AddStudentAsync(StudentViewModel student)
        {
            var studentToAdd = _mapper.Map<Student>(student);

            //Assuming that duplicate entries are allowed we skipped on checking if the student already exists in the system

            if (studentToAdd.Id == null || studentToAdd.Id == Guid.Empty)
                studentToAdd.Id = Guid.NewGuid();

            var user = await _databaseContext.AddStudentAsync(studentToAdd);
            return studentToAdd;
        }

        public async Task<List<Student>> AllStudentsAsync()
        {
            var students = await _databaseContext.GetAllStudentsAsync();
            return students;
        }

        public async Task<Student> StudentByIdAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
                throw new Exception("Provided Student Id is invalid.");

            var student = await _databaseContext.FindStudentByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student with provided id cannot be found in the system");

            return student;
        }

        public async Task<Student> UpdateStudentAsync(StudentViewModel vmStudent)
        {
            var studentToUpdate = _mapper.Map<Student>(vmStudent);

            var student = await _databaseContext.UpdateStudentAsync(studentToUpdate);

            return student;
        }
    }
}
