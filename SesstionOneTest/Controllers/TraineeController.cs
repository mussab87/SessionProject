using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DataLayer.Repository;
using Service.DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SesstionOneTest.Controllers
{
    public class TraineeController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITraineeRepository traineeRepository;

        public TraineeController(
           ITraineeRepository traineeRepository,
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager
           )

        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.traineeRepository = traineeRepository;
        }

        //View All trainee
        public ViewResult Index()
        {
            var model = traineeRepository.getAllTraineeAndCourses();

            if (model == null)
            {
                ViewBag.Empty = "yes";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel model)
        {
            if (ModelState.IsValid)
            {

                AddCourseViewModel result = traineeRepository.AddCourse(model);

                return RedirectToAction("Index");

            }

            return View(model);
        }

        public IActionResult AddTrainee()
        {
            //var UserId = userManager.GetUserId(HttpContext.User);

            ViewBag.ListofCourses = traineeRepository.GetAllCourses();


            return View();
        }


        [HttpPost]
        public IActionResult AddTrainee(TraineeCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Trainee.CreateStamp = DateTime.Now;
                model.Trainee.CreatedBy = HttpContext.User.Identity.Name;

                var AddNewTrainee = traineeRepository.AddEmployeeandCourses(model);


                return RedirectToAction("Index");

            }

            ModelState.AddModelError(string.Empty, "There is something wrong please contact Administrator.");

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTrainee(int TraineeId, int TraineeTrsId)
        {
            //var UserId = userManager.GetUserId(HttpContext.User);
            var model = traineeRepository.DeleteTrainee(TraineeId, TraineeTrsId);

            //delete user from users
            //var User_Id = id.ToString();
            //var User = userManager.Users.FirstOrDefault(u => u.Id == userID);

            //if (User != null)
            //{
            //    //delete role
            //    var roleId = _config.GetValue<string>("UserRole:RoleId");
            //    var role = await roleManager.FindByIdAsync(roleId);

            //    var resultRole = userManager.RemoveFromRoleAsync(User, role.Name);

            //    var result = userManager.DeleteAsync(User);
            //}

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTrainee(int TraineeId)
        {
            var UserId = userManager.GetUserId(HttpContext.User);

            ViewBag.ListofCourses = traineeRepository.GetAllCourses();

            var TraineeViewModel = traineeRepository.getTraineeById(TraineeId);

            return View(TraineeViewModel);
        }


        [HttpPost]
        public IActionResult UpdateTrainee(ViewAllViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateStamp = DateTime.Now;

                var UpdateTrainee = traineeRepository.Update(model);

                if (UpdateTrainee == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Please Contact System Admin");

                    ViewBag.ListofCourses = traineeRepository.GetAllCourses();
                    return View(model);
                }

            }

            ModelState.AddModelError("", "Please Complete Required data");

            ViewBag.ListofCourses = traineeRepository.GetAllCourses();
            return View(model);
        }
    }
}
