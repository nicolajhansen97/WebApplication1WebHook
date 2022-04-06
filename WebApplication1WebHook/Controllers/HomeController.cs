using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1WebHook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        
        public async Task<ActionResult> CreateCustomer(int customerNo = 10, string customerName = "")
        {
            // Call create customer ODataV4
            System.Diagnostics.Debug.WriteLine("CreateCustomer call with: " + customerNo);

            //var options = new NTLMOptions { };

            //var handler = new HttpClientHandler
            //{
            //    Credentials = new NetworkCredential(options.Username, options.Password, options.Domain)
            //};


            //Uri adress = new Uri(@"http://easv-fha-q119:7048/BC170/api/beta/companies");
            //Uri adress = new Uri(@"http://easv-fha-q119:7048/BC170/ODataV4/Company('CRONUS%20UK%20Ltd.')/SalesOrder");
            Uri adress = new Uri(@"http://laptop-jos7ofn3:7048/BC170/ODataV4/CreateCustomer_CreateCustomer?company=CRONUS%20UK%20Ltd.");

            



            var credentialsCache = new CredentialCache();
            credentialsCache.Add(adress, "NTLM", new NetworkCredential("nicol", secret, "workgroup"));//Negotiate
            var handler = new HttpClientHandler() { Credentials = credentialsCache, PreAuthenticate = true };//, PreAuthenticate = true


            using (var httpClient = new HttpClient(handler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(2);
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = await httpClient.PostAsJsonAsync(adress, "{'id':1}");
                //var response = await httpClient.GetAsync(adress);
                var response = await httpClient.PostAsJsonAsync(adress, new Test { id = customerNo, name= customerName }); ;

                var result = await response.Content.ReadAsStringAsync();
                
                if ( response.IsSuccessStatusCode )
                    System.Diagnostics.Debug.WriteLine("CreateCustomer call result: " + result);
                else
                    System.Diagnostics.Debug.WriteLine("CreateCustomer call error: " + response.StatusCode.ToString());
            }



         


            return RedirectToAction("Index");
        }










        private String secret = "PASSWORD";
    }

    class Test
    {
        public int id { get; set; }
        public string name { get; set; }
        
    }

    
}
