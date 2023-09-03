using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity
{
    class RestComponent
    {
        public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://tboil.spb.ru/api/v7_ti/") };
        public static readonly string client_id = "l145vUnNHE2mzYYn";

    }
}
