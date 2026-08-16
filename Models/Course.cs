using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentAdminPortal.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required]
        public int Duration { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}