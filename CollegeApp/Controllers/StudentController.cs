using System;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController:ControllerBase
	{
		[HttpGet]
		[Route("/name")]
		public IEnumerable<Student> StudentName()
		{
            return new List<Student>
            {
                new Student
                {
                    Id = 1,
                    StudentName = "RishikC",
                    Email = "rishikr3@gmail.com",
                    Address = "Kalyanpur"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Disha",
                    Email = "bidisha2125@gmail.com",
                    Address = "Garia"
                }
            };
        }
	}
}

