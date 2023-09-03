using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tboil_v20._10._21.Entity.Models;
using Tboil_v20._10._21.Entity;
namespace Tboil_v20._10._21.Entity.Processor
{
    class Event : RestComponent
    {

        public static async Task<Events> GetEventAsync(string dateStart, string dateEnd, string name, string id_tboil, string status)
        {
                
            string url = string.Format("events?client_id={0}&name={1}&date_start={2}&date_end={3}&id_tboil={4}&status={5}",client_id,name,dateStart, dateEnd, id_tboil,status);

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Events>(responseBody);
                    return data;
                }
                else
                {
                }
            }

            return null;
        }

        public static async Task<Events> GetEventAsync(string dateStart, string dateEnd, int status)
        {

            string url = string.Format("events?client_id={0}&date_start={1}&date_end={2}&status={3}", client_id, dateStart, dateEnd, status.ToString());

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Events>(responseBody);
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
