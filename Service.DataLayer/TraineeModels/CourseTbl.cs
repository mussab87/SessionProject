using System;
using System.Collections.Generic;

#nullable disable

namespace Service.DataLayer.TraineeModels
{
    public partial class CourseTbl
    {
        public CourseTbl()
        {
            TeacherCourseTbls = new HashSet<TeacherCourseTbl>();
            TraineeCourseTables = new HashSet<TraineeCourseTable>();
        }

        public int CourseId { get; set; }
        public string CourseNameArabic { get; set; }
        public string CourseNameEnglish { get; set; }

        public virtual ICollection<TeacherCourseTbl> TeacherCourseTbls { get; set; }
        public virtual ICollection<TraineeCourseTable> TraineeCourseTables { get; set; }
    }
}
