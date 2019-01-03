using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using ProjectParameters;

namespace CustomerServiceProject
{
    public class CustomerService
    {
        private readonly ITestOutputHelper output;

        public CustomerService(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetLegacyCustomer()
        {

            //Server URL
            ServerURL s =new ServerURL();
            string baseURL = s.CSUrl();
            //Method Name (Resource)
            string method = "/Account.svc/legacyCustomer/";
            //Parameter
            string CustomerId = "153010";
            //Complete Request URL
            string requestURL = baseURL + method + CustomerId;

            var client = new RestClient(requestURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("accept", "application/json");

            //Run Request
            IRestResponse response = client.Execute(request);

            string ResponseContent = response.Content;
            
            //Validate for HTTP - OK in the response
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("\"WasFound\":true", ResponseContent);
            output.WriteLine("GetLegacyCustomer Request: {0}", requestURL);
            output.WriteLine("GetLegacyCustomer Response: {0}", ResponseContent);
        }
        [Fact]
        public void GetCustomerProfileAccountStatus()
        {
            //Server URL
            ServerURL s = new ServerURL();
            string baseURL = s.CSUrl();
            //Method Name (Resource)

            string method = "/Account.svc/status/";
            //Parameter

            string CustomerProfileAccount = "41928298188700";
            //Complete Request URL
            string requestURL = baseURL + method + CustomerProfileAccount;

            var client = new RestClient(requestURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("accept", "application/json");

            //Run Request
            IRestResponse response = client.Execute(request);

            string ResponseContent = response.Content;

            //Validate for HTTP - OK in the response and one random element
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("\"WasFound\":true", ResponseContent);
            output.WriteLine("GetCustomerProfileAccountStatus Request: {0}", requestURL);
            output.WriteLine("GetCustomerProfileAccountStatus Response : {0}", ResponseContent);
        }
        [Fact]
        public void RefundCalculate()
        {
            //Server URL
            ServerURL s = new ServerURL();
            string baseURL = s.CSUrl();
            //Method Name (Resource)
            string transactionID = "1978928";
            string method = "/api/v1.0/refund/" + transactionID + "/calculate";
            //Complete Request URL
            string requestURL = baseURL + method;
            //Request Body
            string requestObject = "[{ \"DiscId\": 15943220,  \"RefundAmount\": 1, \"Username\": \"RBHQSAKULA-D\"}]";

            //Adding Headers and Creating Request
            var client = new RestClient(requestURL);
            var request = new RestRequest(Method.POST);           
            request.Parameters.Clear();
            request.AddHeader("Origin", baseURL);
            request.AddHeader("Referer", baseURL + "/help/index");
            request.AddParameter("application/json", requestObject, ParameterType.RequestBody);
            
            //Run Request
            IRestResponse response = client.Execute(request);
            string ResponseContent = response.Content;

            //Validate for HTTP - OK in the response and one random element
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("\"Success\":true", ResponseContent);
            Assert.Contains("\"Error\":null", ResponseContent);
            output.WriteLine("RefundCalculate Request: {0}", request);
            output.WriteLine("RefundCalculate Response : {0}", ResponseContent);
        }
    }
}
