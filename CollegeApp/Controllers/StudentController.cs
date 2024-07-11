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
        public ActionResult<IEnumerable<Student>>StudentName()
        {
            return Ok(CollegeRepository.Students);
        }

        [HttpGet]
        [Route("{id:int}", Name ="GetStudentsById")]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} is not found");

            return Ok(student);
        }

        [HttpGet]
        [Route("{name:alpha}", Name ="GetStudentByName")]
        public ActionResult< Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student =  CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with the name {name} is not found");

            return Ok(student);
        }
        [HttpDelete]
        [Route("{id:int}" , Name ="DeleteStudentById")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var studentToBeDeleted = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (studentToBeDeleted == null)
                return BadRequest($"The student with the id {id} does not exist");
            CollegeRepository.Students.Remove(studentToBeDeleted);
            return Ok(true);
        }
    }
}

