using Service.DataLayer.TraineeModels;
using Service.DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataLayer.Repository
{
    public interface ITraineeRepository
    {
        IEnumerable<ViewAllViewModel> getAllTraineeAndCourses();
        AddCourseViewModel AddCourse(AddCourseViewModel course);

        TraineeCourseViewModel AddEmployeeandCourses(TraineeCourseViewModel model);
        TraineeCourseTable AddCourseTable(TraineeTbl trainee, int courseId);

        List<CourseTbl> GetAllCourses();

        TraineeTbl DeleteTrainee(int TraineeId, int TraineeTrsId);

        ViewAllViewModel getTraineeById(int TraineeId);

        bool Update(ViewAllViewModel model);
    }
}
