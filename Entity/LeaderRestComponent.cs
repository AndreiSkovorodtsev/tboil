using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity
{
    class LeaderRestComponent
    {
        protected static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://leader-id.ru/api/") };
        protected static string access_token;

        public LeaderRestComponent()
        {
            access_token = GetAccessTokenAsync().Result;
        }
        protected static async Task<string> GetAccessTokenAsync()
        {
            string url = "https://tboil.spb.ru/api/getLeaderToken.php";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
