using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using studentmanagementsystemmvc.Models;
using System.Text;
//using System.Web.Mvc;

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
            ClsStudent obj = new ClsStudent();
            obj.Name = objstudent.Name;
            obj.Gender = objstudent.Gender;
            obj.Age = objstudent.Age;
          

            url = url + "SetAllStudent";
            string data = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Insert_message"] = "Student Added";
                return RedirectToAction("Index");
            }
            ModelState.Clear();

            return Json(new { successMessage = "Data successfully added." }, System.Web.Mvc.JsonRequestBehavior.AllowGet);
          
        }
    }
}
