using demo.Models;
using Hospital_Management_System.Models;
using Hospital_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital_Management_System.Controllers
{
   

    public class HomeController : Controller
	{
        HMSEntites context = new HMSEntites();
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
        }
        public IActionResult Index()
		{
            StatisticsHospitalVM statisticsHospital = new StatisticsHospitalVM();
            statisticsHospital.StaffCount = context.Staffs.Count(s => s.Role == "Doctor" || s.Role == "Nurce");
            statisticsHospital.DepartmentCount = context.Departments.Select(d => d.Name).Distinct().Count(); ;
            statisticsHospital.AppointmentCount = context.Appointments.Count(a => a.Status == "Confirmed"); ;
            return View(statisticsHospital);
        }

        // Action to redirect to the login page
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        // Action to redirect to the register page
        public ActionResult Register()
        {
            return RedirectToAction("Register", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}