using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity.Processor
{
    class User : RestComponent
    {
       
        public static async Task<Entity.Models.Users> GetUsersAsync(string search)
        {

            string url = string.Format("user?&client_id={0}&search={1}", client_id, search);

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Entity.Models.Users>(responseBody);
                    return data;
                }
                else
                {
                }
            }

            return null;
        }
        public static async Task<List<Entity.Models.User>> GetUserByIdAsync(int idUser)
        {

            string url = string.Format("user/{0}?client_id={1}", idUser, client_id);

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Entity.Models.User>>(responseBody);
                    return data;
                }
                else
                {
                }
            }

            return null;
        }
        public async static Task<string> Add(string name, string lastname, string middlename, string email, string birthDay,  string companyName, string position)
        {
            string url = "user";

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { new StringContent(name.ToString()), "name" },
                { new StringContent(lastname.ToString()), "lastname" },
                { new StringContent(middlename), "middlename" },
                { new StringContent(birthDay), "birthday" },
                { new StringContent(email), "email" },
                { new StringContent(client_id), "client_id" },
                { new StringContent("P@ssw0rd!2020"), "password" },
                { new StringContent("Y"), "active" },
                { new StringContent("точка кипения (десктоп)"), "url_id" },
                { new StringContent(companyName), "company" },
                { new StringContent(position), "profession" }
            };

            HttpResponseMessage response = await client.PostAsync(url, form);

            string responseMessage = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpListenerException(((int)response.StatusCode), responseMessage);
            }

            return responseMessage;
        }
        public async static Task<bool> Add(string name, string lastname, string middlename, string email, string birthDay, string companyName, string position, string phone)
        {
            string url = "user";

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { new StringContent(name.ToString()), "name" },
                { new StringContent(lastname.ToString()), "lastname" },
                { new StringContent(middlename), "middlename" },
                { new StringContent(birthDay), "birthday" },
                { new StringContent(email), "email" },
                { new StringContent(client_id), "client_id" },
                { new StringContent("P@ssw0rd!2020"), "password" },
                { new StringContent("Y"), "active" },
                { new StringContent("точка кипения (десктоп)"), "url_id" },
                { new StringContent(companyName), "company" },
                { new StringContent(position), "profession" },
                { new StringContent(phone), "tel" }
            };

            HttpResponseMessage response = await client.PostAsync(url, form);

            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }
    }
}
