using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADManageSystem.Models
{
    public class Course
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Course Code")]
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Course Full Name")]
        public string FullName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Short Name")]
        public string ShortName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Description")]
        public string Description { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Course Duration")]
        public int Duration { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Credit")]
        public int Credit { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Categories { get; set; }
    }

    public class Category
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Category Description")]
        public string Description { get; set; }
    }

    public class Class
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Class Code")]
        public string Code { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int? CourseID { get; set; }
        public virtual Course Courses { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<TraineeClass> TraineeClasses { get; set; }
    }

    public class Topic
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Topic Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Topic Description")]
        public string Description { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }

        public int? ClassID { get; set; }
        public virtual Class Classes { get; set; }
    }

    public class TraineeClass
    {
        public int? Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Grade")]
        public string Grade { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }

        public int ClassID { get; set; }
        public virtual Class Classes { get; set; }
    }
}