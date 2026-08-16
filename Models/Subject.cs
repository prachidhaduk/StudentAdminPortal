using System.ComponentModel.DataAnnotations;

namespace StudentAdminPortal.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}