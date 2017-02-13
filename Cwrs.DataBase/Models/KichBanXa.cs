using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;

namespace Cwrs.DataBase.Models
{
    public class KichBanXa
    {
        public thiet_lap_kich_ban_tb KichBan { get; set; }
        public List<thiet_lap_kich_ban_detail_tb> KichBanChiTiet { get; set; }
    }
}
