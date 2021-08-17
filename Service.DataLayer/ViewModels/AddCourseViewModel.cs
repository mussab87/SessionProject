using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataLayer.ViewModels
{
   public class AddCourseViewModel
    {
        [Required]
        [Display(Name = "Course Name Arabic")]
        public string CourseNameArabic { get; set; }
        [Required]
        [Display(Name = "Course Name English")]
        public string CourseNameEnglish { get; set; }
    }
}
