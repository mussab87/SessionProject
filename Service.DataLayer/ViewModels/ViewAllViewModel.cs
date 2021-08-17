using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataLayer.ViewModels
{
    public class ViewAllViewModel
    {
        public int? TraineeId { get; set; }
        public string TraineeName { get; set; }
        public string TraineeNationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }

        public int? TraineeTrsId { get; set; }
        public int? TraineeIdCourseLink { get; set; }
        public int? CourseId { get; set; }

        public int? CourseCourseId { get; set; }
        public string CourseNameArabic { get; set; }
        public string CourseNameEnglish { get; set; }
    }
}
