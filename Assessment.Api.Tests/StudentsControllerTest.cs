using Assessment.Api.Models;
using Assessment.Api.Models.Entities;
using Assessment.Api.Services;
using Assessment.Api.ViewModels;
using Assessment.Api.ViewModels.Mappings;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.Api.Tests
{
    //A simple test class to test Student Service

    [TestClass]
    public class StudentServiceTest
    {
        
        //A simple test method to test some Student Service class
        [TestMethod]
        public async Task StudentByIdAsync_WithEmtyGuid_ShouldReturnException()
        {
            //Assign
            var emptyGuid  = Guid.Empty;
            var mockDb = new Mock<IDatabaseService>();
            var mockMap = new Mock<IMapper>();
            var expectedMessage = "Provided Student Id is invalid.";
            IStudentService service = new StudentService(mockDb.Object, mockMap.Object);
            
            try
            {
                //Act
                var ex = await service.StudentByIdAsync(emptyGuid);
                Assert.Fail();
            }catch(Exception ex)
            {
                // Assert - Expects exception
                Assert.AreEqual(expectedMessage, ex.Message, "Wrong expected message returned.");
            }
            
        }

    }
}
