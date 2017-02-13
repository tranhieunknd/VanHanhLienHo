using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.Lib.Model
{
    class SecurityUser
    {
        public int? UserId { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public int? DeletedStatus { set; get; }
        public string RoleRef { set; get; }
        public bool? IsAdminBitType { set; get; }
        public string GroupRef { set; get; }
        public string UserGuid { set; get; }
    }
}
