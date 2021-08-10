using System;
using System.Collections.Generic;

#nullable disable

namespace Service.DataLayer.TraineeModels
{
    public partial class TraineeCourseTable
    {
        public int TraineeTrsId { get; set; }
        public int TraineeId { get; set; }
        public int CourseId { get; set; }

        public virtual CourseTbl Course { get; set; }
        public virtual TraineeTbl Trainee { get; set; }
    }
}
