using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
   public class DienBien_Models
    {
        public int vi_tri_ref { get; set; }
        public int loai_du_lieu_ref { get; set; }
        public string gia_tri { get; set; }
        public Nullable<System.DateTime> thoi_gian { get; set; }
        public int is_thuc_do { get; set; }
    }
}
