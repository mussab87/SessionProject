using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SesstionOneTest.Controllers
{
    [Authorize]
    public class MainHomeController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        //private readonly IWorkerRepository WorkerRep;

        public MainHomeController(
           //IWorkerRepository WorkerRep,
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager
           )

        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            //this.WorkerRep = WorkerRep;
        }
        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole("Admin"))
            {
                ViewData["User"] = "Admin";

                var UserId = userManager.GetUserId(HttpContext.User);

                //var model = WorkerRep.GetAllWorker();
                //var modelEval = WorkerRep.GetAllEmpEvaluated();

                //if (model != null)
                //    ViewBag.countAll = model.Count();

                //if (modelEval != null)
                //    ViewBag.countAllEvaluated = modelEval.Count();


                return View();
            }
            else if (HttpContext.User.IsInRole("User"))
            {
                ViewData["User"] = "User";
                return View();
            }

            TempData["NoAccess"] = "Sorry This User Does Not have an access to This page, return back to the Administartor..";
            return RedirectToAction("Login", "Account");
        }

    }
}
