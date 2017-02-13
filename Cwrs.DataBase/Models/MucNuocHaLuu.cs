using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class MucNuocHaLuu
    {
        public DateTime? thoi_gian { get; set; }
        public double MucNuocQuyTrinh { get; set; }
        public double? mucnuoc_HaNoi { get; set; }
        public double? mucnuoc_PhaLai { get; set; }
        public double? mucnuoc_TuyenQuang { get; set; }
        public int? don_vi_code { get; set; }
        public int? cap_bao_dong { get; set; }
        public MucNuocHaLuu() { }
    }
}
