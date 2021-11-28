using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_DUONGTHICHINH.Entities
{
   public class Credential
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string account_id { get; set; }
        public string expire_time { get; set; }
        public string create_At { get; set; }
        public string update_At { get; set; }
        public int status { get; set; }
    }
}
