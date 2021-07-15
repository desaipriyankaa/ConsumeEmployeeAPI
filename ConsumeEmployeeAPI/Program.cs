using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ConsumeEmployeeAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetEmployeeInfo();
            //PostEmployeeInfo();
            PutEmployeeInfo(5);
            DeleteEmployeeInfo(2);
            Console.ReadLine();
        }


        // GET method
        public static void GetEmployeeInfo()
        {
            var client = new RestClient("https://dummy.restapiexample.com/api/v1/");
            var request = new RestRequest("employees");
            var responce = client.Execute(request);

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                string resp = responce.Content;
                Rootobject result = JsonConvert.DeserializeObject<Rootobject>(resp);

                foreach (var item in result.data)
                {
                    Console.WriteLine($"ID : {item.id} \nEmployee_Name : {item.employee_name}\n" +
                        $"Employee_Salary : {item.employee_salary}\nEmployee_Age : {item.employee_age}");
                    Console.WriteLine();
                }
                Console.WriteLine("All records showed...");
                Console.WriteLine();
            }
        }


        // POST / Create method
        public static void PostEmployeeInfo()
        {
            var client = new RestClient("https://dummy.restapiexample.com/api/v1/");
            var request = new RestRequest("create", Method.POST);
            var obj = new Datum() { employee_name = "xyz", employee_age = 50, employee_salary = 10000, profile_image = "" };

            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(JsonConvert.SerializeObject(obj));
            var responce = client.Post(request);

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                string resp = responce.Content;
                RootEmployee result = JsonConvert.DeserializeObject<RootEmployee>(resp);                
                
                Console.WriteLine($"ID : {result.data.id} \nEmployee_Name : {result.data.employee_name}\n" +
                $"Employee_Salary : {result.data.employee_salary}\nEmployee_Age : {result.data.employee_age}" +
                $"\nMessage : {result.message}");

                Console.WriteLine();
            }            
        }

        // PUT/Update method
        public static void PutEmployeeInfo(int id)
        {
            var client = new RestClient("https://dummy.restapiexample.com/api/v1/");
            var request = new RestRequest($"update/{id}/", Method.PUT);
            var responce = client.Put(request);

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                string resp = responce.Content;
                Rootobject result = JsonConvert.DeserializeObject<Rootobject>(resp);
                
                Console.WriteLine($"Record at id : {id} is updated...");
                Console.WriteLine(result.message);
                Console.WriteLine();
            }
        }

        // Delete method
        public static void DeleteEmployeeInfo(int id)
        {
            var client = new RestClient("https://dummy.restapiexample.com/api/v1/");
            var request = new RestRequest($"delete/{id}/", Method.DELETE);
            var responce = client.Delete(request);
            
            if (responce.StatusCode == HttpStatusCode.OK)
            {
                //string resp = responce.Content;                  
                Console.WriteLine($"Record at id {id} is deleted");
                Console.WriteLine();
            }
        }
    }

    public class RootEmployee
    {
        public string status { get; set; }
        public Datum data { get; set; }
        public string message { get; set; }
    }

    public class Rootobject
    {
        public string status { get; set; }
        public Datum[] data { get; set; }
        public string message { get; set; }
    }

    public class Datum
    {
        
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }
        
    }

}
