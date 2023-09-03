using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21.Entity.Models
{
    class User
    {
        public int id { get; set; }
        public string lastname { get; set; }
        public string name { get; set; }
        public string middlename { get; set; }
        public string email { get; set; }
        public string tel{ get; set; }
        public string profession { get; set; }
        public string work_company { get; set; }
        public string photo { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public List<Group> groups { get; set; }


    }
}
