using System;
using System.Collections.Generic;

#nullable disable

namespace Service.DataLayer.TraineeModels
{
    public partial class TeacherCourseTbl
    {
        public int TeacherTrsId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        public virtual CourseTbl Course { get; set; }
        public virtual TeacherTbl Teacher { get; set; }
    }
}
