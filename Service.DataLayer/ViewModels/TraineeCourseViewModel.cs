using Service.DataLayer.TraineeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataLayer.ViewModels
{
    public class TraineeCourseViewModel
    {
        public TraineeTbl Trainee { get; set; }
        public TraineeCourseTable TraineeCourse { get; set; }
        public CourseTbl Course { get; set; }
    }
}
