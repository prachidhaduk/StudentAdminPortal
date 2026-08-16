using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAdminPortal.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Enrollment Number")]
        public string EnrollmentNo { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        public int CourseId { get; set; }

        public int Semester { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Admission Date")]
        public DateTime? AdmissionDate { get; set; }

        public bool Status { get; set; }

        public virtual Course Course { get; set; }
    }
}