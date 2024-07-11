using System;
using System.Collections.Generic;
namespace CollegeApp.Controllers
{
	public class CollegeRepository
	{
        public static List<Student> Students { get; set; } = new List<Student>() {
             new Student
                {
                    Id = 1,
                    StudentName = "Rishik",
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

