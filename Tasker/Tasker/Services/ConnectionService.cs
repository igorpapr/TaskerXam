using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tasker.Models;

namespace Tasker.Services
{
    public class ConnectionService
    {
        //public const string BASE_ADDRESS = "http://192.168.1.4:8080/api/";
        public const string BASE_ADDRESS = "https://taskerappbc.herokuapp.com/api/";
        public const string LOGIN_ADDRESS = "auth/login";
        public const string TASKS_ADDRESS = "tasks";

        public async Task<string> Login(UserLoginData data)
        {
            string json = JsonConvert.SerializeObject(data);
            json.ToLower();
            HttpContent content = new StringContent(json.ToLower());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            var responseMessage = await client.PostAsync(BASE_ADDRESS + LOGIN_ADDRESS, content);
            
            string token = "";
            try
            {
                responseMessage.EnsureSuccessStatusCode();
                var jsonResonse = await responseMessage.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<UserLoginSuccessResponse>(jsonResonse).Token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }

        public async Task<bool> AddTask(MyTask task, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(task);
            HttpContent content = new StringContent(json.ToLower());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseMessage = await client.PostAsync(BASE_ADDRESS + TASKS_ADDRESS, content);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<MyTask>> GetTasks(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);
            
            var json = await client.GetStringAsync(BASE_ADDRESS + TASKS_ADDRESS);
            var tasks = JsonConvert.DeserializeObject<List<MyTask>>(json);
            
            return tasks;
        }

    }
}
