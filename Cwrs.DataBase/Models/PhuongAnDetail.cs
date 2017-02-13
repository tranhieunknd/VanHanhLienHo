using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    class PhuongAnDetail
    {
        public int phuong_an_detail_code { get; set; }
        public int vi_tri_ref { get; set; }
        public int vi_tri_name { get; set; }
        public Nullable<int> phuong_an_ref { get; set; }
        public int loai_du_lieu_ref { get; set; }
        public double gia_tri { get; set; }
        public Nullable<System.DateTime> thoi_gian { get; set; }
        public Nullable<int> deleted_status { get; set; }
    }
}
