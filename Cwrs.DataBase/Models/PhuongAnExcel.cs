using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cwrs.DataBase.VanHanhLienHo;
namespace Cwrs.DataBase.Models
{
    public class PhuongAnExcel
    {
        public string PhanTichNhanXet { get; set; }
        public string KienNghiPA { get; set; }
        public List<PhuongAnDetail_Model> PhuongAn_Detail { get; set; }
    }
}