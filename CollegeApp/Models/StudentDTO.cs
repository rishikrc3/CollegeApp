using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Controllers
{
	public class StudentDTO
	{

        public int Id { get; set; }

        [Required(ErrorMessage ="Student name is required")]
        public string StudentName { get; set;}

        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Student address is required")]
        public string Address { get; set; }

    }
}
