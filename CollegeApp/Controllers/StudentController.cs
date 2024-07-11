using System;
using Microsoft.AspNetCore.Mvc;


namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("/name")]
        public IEnumerable<Student> StudentName()
        {
            return CollegeRepository.Students;
        }
       
    }
}

