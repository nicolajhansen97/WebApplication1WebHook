using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1WebHook
{
    public class OrderCreateWebHookHandler : WebHookHandler
    {

        public OrderCreateWebHookHandler()
        {
            this.Receiver = "OrderCreate";
        }
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {

            System.Diagnostics.Debug.WriteLine("Called1-----------------------");


            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();


            return null;
        }
    }
}