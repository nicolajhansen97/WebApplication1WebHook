using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1WebHook
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public GenericJsonWebHookHandler()
        {
            this.Receiver = "genericjson";
        }

        public async override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            string secret = "PASSWORD";


































            System.Diagnostics.Debug.WriteLine("Called1-----------------------");


            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            try
            {
                string customerWeebhook = "z";
                string newOrderWebhook = "c";

                string whichType = context.Id;

                if (whichType.Equals("z"))
                {

                    try
                    {

                        int customerID = int.Parse(data.GetValue("id").ToString());
                        string firstName = data.GetValue("first_name").ToString();
                        string lastName = data.GetValue("last_name").ToString();
                        string customerEmail = data.GetValue("email").ToString();
                        string customerUsername = data.GetValue("username").ToString();

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
                            var response = await httpClient.PostAsJsonAsync(adress, new Test { id = customerID, firstName = firstName, lastName = lastName, customerEmail = customerEmail, customerUsername = customerUsername }); ;

                            var result = await response.Content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                                System.Diagnostics.Debug.WriteLine("CreateCustomer call result: " + result);
                            else
                                System.Diagnostics.Debug.WriteLine("CreateCustomer call error: " + response.StatusCode.ToString());
                        }

          

                        string action = context.Actions.FirstOrDefault();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
                else if(whichType.Equals("c"))
                {
                    int customerID = int.Parse(data.GetValue("customer_id").ToString());
          //          string firstName = data.GetValue("first_name").ToString();
            //        string lastName = data.GetValue("last_name").ToString();
              //      string customerEmail = data.GetValue("email").ToString();
                //    string adress = data.GetValue("adress_1").ToString();
                  //  string city = data.GetValue("city").ToString();
                    //string postCode = data.GetValue("postcode").ToString();
                    //string country = data.GetValue("country").ToString();
                    //string phone = data.GetValue("phone").ToString();

                    Uri adress2 = new Uri(@"http://laptop-jos7ofn3:7048/BC170/ODataV4/CreateCustomer_CreateCustomer?company=CRONUS%20UK%20Ltd.");

                    var credentialsCache = new CredentialCache();
                    credentialsCache.Add(adress2, "NTLM", new NetworkCredential("nicol", secret, "workgroup"));//Negotiate
                    var handler = new HttpClientHandler() { Credentials = credentialsCache, PreAuthenticate = true };//, PreAuthenticate = true


                    using (var httpClient = new HttpClient(handler))
                    {
                        httpClient.Timeout = TimeSpan.FromMinutes(2);
                        //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //var response = await httpClient.PostAsJsonAsync(adress, "{'id':1}");
                        //var response = await httpClient.GetAsync(adress);
                        var response = await httpClient.PostAsJsonAsync(adress2, new Order { customerID = customerID }); ; //firstName = firstName, lastName = lastName, customerEmail = customerEmail, adress = adress, city = city , postCode = postCode, country = country, phone = phone}); ;

                        var result = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                            System.Diagnostics.Debug.WriteLine("CreateCustomer call result: " + result);
                        else
                            System.Diagnostics.Debug.WriteLine("CreateCustomer call error: " + response.StatusCode.ToString());
                    }



                    string action = context.Actions.FirstOrDefault();
                }
            }

            catch (Exception e)
            {
                Console.Write(e);
            }


        }
    }

    class Test
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string customerEmail { get; set; }
        public string customerUsername { get; set; }

    }

    class Order
    {
        public int customerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string customerEmail { get; set; }
        public string adress { get; set; }
        public string city { get; set; }
        public string postCode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }

    }
}