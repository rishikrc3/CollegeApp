using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {


        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> StudentName()
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
        [Route("{id:int}", Name = "GetStudentsById")]
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
        [Route("{name}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
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

        [HttpPost]
        [Route("create", Name = "CreateNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
                return BadRequest();


            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student studentobj = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            CollegeRepository.Students.Add(studentobj);

            model.Id = studentobj.Id;
            return CreatedAtRoute("GetStudentsById", new { id = model.Id }, model);



        }
        [HttpPut]
        [Route("update",Name ="Update Student")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();
            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}", Name = "Update Student Partial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var newStudentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };

            patchDocument.ApplyTo(newStudentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            existingStudent.StudentName = newStudentDTO.StudentName;
            existingStudent.Email = newStudentDTO.Email;
            existingStudent.Address = newStudentDTO.Address;

            return NoContent();
        }





        [HttpDelete]
            [Route("{id:int}", Name = "DeleteStudentById")]
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


