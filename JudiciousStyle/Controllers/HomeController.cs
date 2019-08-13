using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace JudiciousStyle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult About(decimal price, string desc)
        {
            User_Client_Card_Info a = new User_Client_Card_Info
            {
                TransactionAmount = price,
                PurchaseDescription = desc,
                CardNumber = "2407199449917042",
                IdentityNumber = "K2407944608215",
                CardType = "Credit",
                ExpirationDate = DateTime.Parse("2030-12-30")


            };
            return View(a);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> About(User_Client_Card_Info User_Client_Card_Info)
        {
            var s = await PostResult(User_Client_Card_Info);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<JsonResult> GetResult()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responce = await client.GetAsync("https://localhost:44311/api/PaymentGateway?appid=0c56f2c2-5457-4a32-ba2b-971afdb9f99e&CardNumber=2407199449917042"))
                {
                    using (HttpContent content = responce.Content)
                    {
                        
                        return Json(content.ReadAsStringAsync(), JsonRequestBehavior.AllowGet);
                    }

                }
            };
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> PostResult(User_Client_Card_Info User_Client_Card_Info)
        {
            using (HttpClient client = new HttpClient())
            {
              
                var content = new StringContent(JsonConvert.SerializeObject(User_Client_Card_Info), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:44311/api/PaymentGateway?appid=0c56f2c2-5457-4a32-ba2b-971afdb9f99e&CardNumber=2407199449917042", content);

                var responseString = await response.Content.ReadAsStringAsync();

                return Json(responseString, JsonRequestBehavior.AllowGet);
            };
            
        }

 
        public class User_Client_Card_Info
        {
            public string CardNumber { get; set; }
            public string IdentityNumber { get; set; }
            public string CardType { get; set; }
            public DateTime ExpirationDate { get; set; }
            public decimal? TransactionAmount { get; set; }
            public string PurchaseDescription { get; set; }
            public User_Client_Card_Info()
            {

            }
        }
    }
}