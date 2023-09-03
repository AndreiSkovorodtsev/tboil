using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity.Processor
{
    class TboilRequest : RestComponent
    {
        public static async Task<Entity.Models.TboilRequests> GetRequestsAsync(string dateStart, string dateEnd)
        {

            string url = string.Format("request?client_id={0}&date_start={1}&date_end={2}", client_id, dateStart, dateEnd);

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Entity.Models.TboilRequests>(responseBody);
                    return data;
                }
                else
                {
                }
            }

            return null;
        }

        public async static Task<bool> Add(int idUser, string placeAddress)
        {
            string url = "request";

            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(idUser.ToString()), "id");
            form.Add(new StringContent(client_id), "client_id");

            HttpResponseMessage response = await client.PostAsync(url, form);

            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }
    }
}
