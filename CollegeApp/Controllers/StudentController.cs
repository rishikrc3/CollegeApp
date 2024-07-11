using System;
using Microsoft.AspNetCore.Mvc;


namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("name")]
        public IEnumerable<Student> StudentName()
        {
            return CollegeRepository.Students;
        }

        [HttpGet]
        [Route("{id:int}")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        [HttpGet]
        [Route("{name}")]
        public Student GetStudentByName(string name)
        {
            return CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
        }
       
    }
}

