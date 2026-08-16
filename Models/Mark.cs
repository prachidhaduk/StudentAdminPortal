using System.ComponentModel.DataAnnotations;

namespace StudentAdminPortal.Models
{
    public class Mark
    {
        public int MarkId { get; set; }


        // Student
        [Required]
        [Display(Name = "Student")]
        public int StudentId { get; set; }


        // Subject
        [Required]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }


        // SQL: decimal
        [Display(Name = "Marks Obtained")]
        public decimal MarksObtained { get; set; }


        // SQL: decimal
        [Display(Name = "Maximum Marks")]
        public decimal MaxMarks { get; set; }


        // SQL: int
        [Required]
        [Range(0, 40)]
        [Display(Name = "Internal Marks")]
        public int InternalMarks { get; set; }


        // SQL: int
        [Required]
        [Range(0, 60)]
        [Display(Name = "External Marks")]
        public int ExternalMarks { get; set; }


        // SQL: int
        [Display(Name = "Total Marks")]
        public int TotalMarks { get; set; }


        // SQL: decimal
        [Display(Name = "Percentage")]
        public decimal Percentage { get; set; }


        // SQL: nvarchar
        [Display(Name = "Grade")]
        public string Grade { get; set; }


        // Relationships
        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}