using Microsoft.AspNetCore.Mvc;
using studentmanagementsystemmvc.Models;

namespace studentmanagementsystemmvc.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7020/api/StudentAPI/";
        // 

        private HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ClsStudent objstudent)
        {
            ClsStudent obj=new ClsStudent();
            //obj

            return Json("");
        }
    }
}
