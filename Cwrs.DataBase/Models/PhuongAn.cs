using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class PhuongAn
    {
        public string ki_hieu { set; get; }
        public int don_vi_code { set; get; }
        public int phuong_an_code { set; get; }
        public int kiem_tra { set; get; }
        public string kien_nghi { set; get; }
        public int loai_du_lieu_ref { set; get; }
        public double gia_tri { set; get; }
        public int vi_tri_ref { set; get; }
        public List<Item> item { set; get; }
        public DateTime thoi_gian { set; get; }
        public int? TongHop_Code { set; get; }
        public string TongHop_phanTich { set; get; }
        public string TongHop_TongHop { set; get; }
        public string TongHop_KienNghi { set; get; }
        public DateTime? TongHop_ThoiGian { set; get; }
        public DateTime? TongHop_CreateAt { set; get; }

    }

}
