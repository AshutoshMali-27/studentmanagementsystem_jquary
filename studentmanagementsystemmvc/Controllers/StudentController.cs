using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using studentmanagementsystemmvc.Models;
using System.Net.Http;
using System.Text;
//using System.Web.Mvc;
//using System.Web.Mvc;
//using System.Web.Mvc;

namespace studentmanagementsystemmvc.Controllers
{
    public class StudentController : Controller
    {
        //private string url = "https://localhost:7020/api/StudentAPI/";
        // 
        //private readonly IHttpClientFactory _httpClientFactory;
        //public StudentController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromBody] ClsStudent objstudent)
        {
            string url = "https://localhost:7020/api/StudentAPI/";
            url = url + "SetAllStudent";
            ClsStudent obj = new ClsStudent();
            obj.Name = objstudent.Name;
            obj.Gender = objstudent.Gender;
            obj.Age = objstudent.Age;


            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(objstudent);

            }
            string data = JsonConvert.SerializeObject(objstudent);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Insert_message"] = "Student Added";
                return RedirectToAction("Index");
            }
            ModelState.Clear();

            return View();
        }



        [HttpGet]
        public async  Task<IActionResult> GETData()
        {
            
            List<ClsStudent> data = await GetDataFromYourSource();
        


            return Ok(data);
        }


        [HttpGet]
        public  IActionResult Details()
        {



            return View();
        }


        private async Task<List<ClsStudent>> GetDataFromYourSource()
        {
            string url = "https://localhost:7020/api/StudentAPI/";
            url = url + "GetAllStudent";
            List <ClsStudent> responcedata = new List<ClsStudent>();

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
                {

                string result = response.Content.ReadAsStringAsync().Result;
                 responcedata = JsonConvert.DeserializeObject<List<ClsStudent>>(result);
            }
            

            return responcedata;
        }




        public IActionResult Edit(int id)
        {

            return View(id);
        }

        [HttpGet]
        public async Task<ActionResult<ClsStudent>> GetDatabyid(int id)
        {
            string url = "https://localhost:7020/api/StudentAPI/";
            url = url + "getAllStudentsByID/";

            ClsStudent responcedata = new ClsStudent();
            HttpResponseMessage response = client.GetAsync(url+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                 responcedata = JsonConvert.DeserializeObject<ClsStudent>(result);
               
            }

            return responcedata;
        }

        [HttpPost]
       public  IActionResult UpdateID(int id, [FromBody] ClsStudent updatedStudent)
        {
            string url = "https://localhost:7020/api/StudentAPI/";
            url = url + "updateStudentsByID/"+id;
            ClsStudent obj = new ClsStudent();
            obj.id = id;
            obj.Name = updatedStudent.Name;
            obj.Gender = updatedStudent.Gender;
            obj.Age = updatedStudent.Age;

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(updatedStudent);

            }
            string data = JsonConvert.SerializeObject(updatedStudent);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
               // TempData["Insert_message"] = "Student Updated Successfully";


            }
            
                //return RedirectToAction("Details");
            //ModelState.Clear();

           return Ok(new { successMessage = "Student updated successfully" });

        }



        public IActionResult Delete(int id)
        {

            return View(id);
        }

        [HttpPost]
        public IActionResult DeleteID(int id)
        {
            string url = "https://localhost:7020/api/StudentAPI/";
            url = url + "DeleteStudentsByID/";


            HttpResponseMessage response = client.DeleteAsync(url + +id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Delete_message"] = "Student deleted...";
                return RedirectToAction("Index");
            }

            return View(id);
        }



    }
}
