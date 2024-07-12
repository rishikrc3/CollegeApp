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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>>StudentName()
        {
            var students = new List<StudentDTO>();
            foreach (var item in CollegeRepository.Students)
            {
                StudentDTO obj = new StudentDTO()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    Email = item.Email,
                    Address = item.Address
                };
                students.Add(obj);
            }
            return Ok(students);
        }



        [HttpGet]
        [Route("{id:int}", Name ="GetStudentsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} is not found");

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address

            };

            return Ok(studentDTO);
        }


        [HttpGet]
        [Route("{name}", Name ="GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult< StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student =  CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with the name {name} is not found");
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            return Ok(studentDTO);
        }
        [HttpDelete]
        [Route("{id:int}" , Name ="DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var studentToBeDeleted = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (studentToBeDeleted == null)
                return BadRequest($"The student with the id {id} does not exist with us");
            CollegeRepository.Students.Remove(studentToBeDeleted);
            return Ok(true);
        }
    }
}

