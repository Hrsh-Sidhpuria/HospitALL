using Microsoft.AspNetCore.Mvc;

namespace HospitALL.Areas.Patient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
