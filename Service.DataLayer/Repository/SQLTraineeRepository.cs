using Service.DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DataLayer.TraineeModels;

namespace Service.DataLayer.Repository
{
    public class SQLTraineeRepository : ITraineeRepository
    {
        public readonly TraineeDbContext context;
        public SQLTraineeRepository(TraineeDbContext context)
        {
            this.context = context;
        }
        public AddCourseViewModel AddCourse(AddCourseViewModel course)
        {
            try
            {
                CourseTbl courseTable = new CourseTbl
                {
                    CourseNameArabic = course.CourseNameArabic,
                    CourseNameEnglish = course.CourseNameEnglish
                };

                //Add new course record to Db
                context.CourseTbls.Add(courseTable);
                context.SaveChanges();

                return course;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public TraineeCourseTable AddCourseTable(TraineeTbl trainee, int courseId)
        {
            TraineeCourseTable courseTable = new TraineeCourseTable
            {
                TraineeId = trainee.TraineeId,
                CourseId = courseId
            };

            //Add new course record to Db
            context.TraineeCourseTables.Add(courseTable);
            context.SaveChanges();

            return courseTable;
        }

        public TraineeCourseViewModel AddEmployeeandCourses(TraineeCourseViewModel model)
        {
            try
            {
                TraineeTbl traineeTbl = null;

                if (model.Trainee != null)
                {
                    traineeTbl = new TraineeTbl();

                    traineeTbl.TraineeName = model.Trainee.TraineeName;
                    traineeTbl.TraineeNationalId = model.Trainee.TraineeNationalId;
                    traineeTbl.PhoneNumber = model.Trainee.PhoneNumber;
                    traineeTbl.CreatedBy = model.Trainee.CreatedBy;
                    traineeTbl.CreateStamp = model.Trainee.CreateStamp;
                    traineeTbl.UpdateStamp = model.Trainee.UpdateStamp;

                    //Add new trainee record to Db
                    context.TraineeTbls.Add(traineeTbl);
                    context.SaveChanges();

                    if (model.Course != null)
                    {
                        var AddCourse = AddCourseTable(traineeTbl, model.Course.CourseId);
                    }


                }

                return model;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public TraineeTbl DeleteTrainee(int TraineeId, int TraineeTrsId)
        {
            //delete Trainee course table if trainee has course 
            if (TraineeTrsId.ToString() != null)
            {
                TraineeCourseTable traineeCourse = context.TraineeCourseTables.Find(TraineeTrsId);
                if (traineeCourse != null)
                {
                    context.TraineeCourseTables.Remove(traineeCourse);
                    context.SaveChanges();
                }
            }
            TraineeTbl trainee = context.TraineeTbls.Find(TraineeId);
            if (trainee != null)
            {
                context.TraineeTbls.Remove(trainee);
                context.SaveChanges();
            }
            return trainee;
        }

        public List<CourseTbl> GetAllCourses()
        {
            return context.CourseTbls.ToList();
        }

        public IEnumerable<ViewAllViewModel> getAllTraineeAndCourses()
        {
            try
            {
                var model = (
                    from t in context.TraineeTbls
                    join tc in context.TraineeCourseTables on t.TraineeId equals tc.TraineeId
                    join c in context.CourseTbls on tc.CourseId equals c.CourseId
                    select new { t, tc, c }
                    ).ToList();

                if (model.Count > 0)
                {
                    List<ViewAllViewModel> obj = new List<ViewAllViewModel>();

                    foreach (var r in model)
                    {
                        ViewAllViewModel InsertObj = new ViewAllViewModel();

                        // var traineeTbl = new TraineeTbl();

                        var SelectedtraineeId = Convert.ToInt32(r.t.TraineeId);
                        //fill trainee object
                        InsertObj.TraineeId = SelectedtraineeId;
                        InsertObj.TraineeName = r.t.TraineeName;
                        InsertObj.TraineeNationalId = r.t.TraineeNationalId;
                        InsertObj.PhoneNumber = r.t.PhoneNumber;
                        InsertObj.CreatedBy = r.t.CreatedBy;
                        InsertObj.CreateStamp = r.t.CreateStamp;
                        InsertObj.UpdateStamp = r.t.UpdateStamp;

                        //fill trainee course object
                        var SelectedTraineeTrsId = r.tc.TraineeTrsId;
                        InsertObj.TraineeTrsId = SelectedTraineeTrsId;
                        InsertObj.TraineeIdCourseLink = r.tc.TraineeId;
                        InsertObj.CourseId = r.tc.CourseId;

                        //fill course object
                        var courseid = r.c.CourseId;
                        InsertObj.CourseCourseId = courseid;
                        InsertObj.CourseNameArabic = r.c.CourseNameArabic;
                        InsertObj.CourseNameEnglish = r.c.CourseNameEnglish;

                        obj.Add(InsertObj);
                    }
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public ViewAllViewModel getTraineeById(int TraineeId)
        {
            try
            {
                var model = (
                    from t in context.TraineeTbls
                    join tc in context.TraineeCourseTables on t.TraineeId equals tc.TraineeId
                    join c in context.CourseTbls on tc.CourseId equals c.CourseId
                    where t.TraineeId == TraineeId
                    select new { t, tc, c }
                    ).ToList();

                if (model.Count > 0)
                {

                    ViewAllViewModel InsertObj = new ViewAllViewModel();

                    // var traineeTbl = new TraineeTbl();

                    var SelectedtraineeId = Convert.ToInt32(model[0].t.TraineeId);
                    //fill trainee object
                    InsertObj.TraineeId = SelectedtraineeId;
                    InsertObj.TraineeName = model[0].t.TraineeName;
                    InsertObj.TraineeNationalId = model[0].t.TraineeNationalId;
                    InsertObj.PhoneNumber = model[0].t.PhoneNumber;
                    InsertObj.CreatedBy = model[0].t.CreatedBy;
                    InsertObj.CreateStamp = model[0].t.CreateStamp;
                    InsertObj.UpdateStamp = model[0].t.UpdateStamp;

                    //fill trainee course object
                    var SelectedTraineeTrsId = model[0].tc.TraineeTrsId;
                    InsertObj.TraineeTrsId = SelectedTraineeTrsId;
                    InsertObj.TraineeIdCourseLink = model[0].tc.TraineeId;
                    InsertObj.CourseId = model[0].tc.CourseId;

                    //fill course object
                    var courseid = model[0].c.CourseId;
                    InsertObj.CourseCourseId = courseid;
                    InsertObj.CourseNameArabic = model[0].c.CourseNameArabic;
                    InsertObj.CourseNameEnglish = model[0].c.CourseNameEnglish;


                    return InsertObj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public bool Update(ViewAllViewModel model)
        {
            try
            {
                TraineeTbl traineeTbl = null;
                TraineeCourseTable traineecourseTbl = null;

                if (model != null)
                {

                    traineeTbl = new TraineeTbl()
                    {
                        TraineeId = (int)model.TraineeId,
                        TraineeName = model.TraineeName,
                        TraineeNationalId = model.TraineeNationalId,
                        PhoneNumber = model.PhoneNumber,
                        CreatedBy = model.CreatedBy,
                        CreateStamp = model.CreateStamp,
                        UpdateStamp = model.UpdateStamp
                    };

                    var UpdateTraiee = context.TraineeTbls.Attach(traineeTbl);

                    UpdateTraiee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                    if (model.TraineeTrsId.ToString() != null)
                    {
                        traineecourseTbl = new TraineeCourseTable()
                        {
                            TraineeTrsId = (int)model.TraineeTrsId,
                            TraineeId = (int)model.TraineeIdCourseLink,
                            CourseId = (int)model.CourseId
                        };

                        var UpdateCourseTrainee = context.TraineeCourseTables.Attach(traineecourseTbl);

                        UpdateCourseTrainee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }

                    return true;
                }

                else return false;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
