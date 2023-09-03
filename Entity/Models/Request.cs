using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity.Models
{
    class Request
    {
        public int id { get; set; }
        public User user { get; set; }
        public string status { get; set; }
        public string pcr_status { get; set; }
        public int id_event { get; set; }
        public string send_leader { get; set; }
    }
}
