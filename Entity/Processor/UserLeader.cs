using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity.Processor
{
    class UserLeader : LeaderRestComponent
    {
        public UserLeader() : base()
        {
           
        }

        public static async Task<Entity.Models.UsersLeader> GetUsersAsync(string search)
        {

            string url = string.Format("control/users?Search={0}&access_token={1}", search, await GetAccessTokenAsync());

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Entity.Models.UsersLeader>(responseBody);
                    return data;
                }
                else
                {
                }
            }

            return null;
        }
    }
}
