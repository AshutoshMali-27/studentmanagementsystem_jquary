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
        private string url = "https://localhost:7020/api/StudentAPI/";
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

       
        public  IActionResult Details()
        {



            return View();
        }


        private async Task<List<ClsStudent>> GetDataFromYourSource()
        {

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



    }
}
