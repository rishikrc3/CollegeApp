using System;
using Microsoft.AspNetCore.Mvc;


namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All" ,Name ="GetAllStudents")]
        public IEnumerable<Student> StudentName()
        {
            return CollegeRepository.Students;
        }

        [HttpGet]
        [Route("{id:int}", Name ="GetStudentsById")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        [HttpGet]
        [Route("{name}", Name ="GetStudentByName")]
        public Student GetStudentByName(string name)
        {
            return CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
        }
        [HttpDelete]
        [Route("{id:int}" , Name ="DeleteStudentById")]
        public bool DeleteStudent(int id)
        {
            var studentToBeDeleted = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            CollegeRepository.Students.Remove(studentToBeDeleted);
            return true;
        }
    }
}

