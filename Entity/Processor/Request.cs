using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity.Processor
{
    class Request : RestComponent
    {
        public async static Task<string> Add(int idEvent, int idUser, int status, int pcrStatus)
        {
            string url = string.Format("events/{0}/request", idEvent.ToString());

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { new StringContent(idUser.ToString()), "id_user" },
                { new StringContent(status.ToString()), "status" },
                { new StringContent(pcrStatus.ToString()), "pcr_status" },
                { new StringContent(client_id), "client_id" },
                { new StringContent("1"), "send_leader" }
            };

            HttpResponseMessage response = await client.PostAsync(url, form);
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpListenerException(((int)response.StatusCode), responseMessage);
            }

            return responseMessage;
        }

        public async static Task<string> UpdateStatus(int idRequest, int status)
        {
            string url = string.Format("https://tboil.spb.ru/api/v7_ti/events/request/{0}?client_id={1}", idRequest.ToString(), client_id);

            Entity.Models.Request requests = new Entity.Models.Request { status=status.ToString(), send_leader = "1"};

            string jsonRequests = JsonConvert.SerializeObject(requests);
            var body = Encoding.UTF8.GetBytes(jsonRequests);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();

            if (response.StatusCode  == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                return readStream.ReadToEnd();
            }
            else
            {
                throw new HttpListenerException(((int)response.StatusCode), response.StatusDescription);
            }

            
        }

        public static async Task<Entity.Models.Requests> GetRequestsAsync(int idEvent ,int status, int idUser   )
        {

            string url = string.Format("events/{0}/request?client_id={1}&status={2}", idEvent.ToString(), client_id, status.ToString());

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Entity.Models.Requests>(responseBody);
                    return data;
                }
                else
                {
                }
            }

            return null;
        }

        public static async Task<Entity.Models.Requests> GetRequestsAsync(int idEvent, int idUser)
        {

            string url = string.Format("events/{0}/request?client_id={1}&id_user={2}", idEvent.ToString(), client_id, idUser.ToString());

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Entity.Models.Requests>(responseBody);
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
