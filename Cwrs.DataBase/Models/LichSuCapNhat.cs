using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class LichSuCapNhat
    {
        [DisplayName("Thời gian")]
        [DisplayFormat(DataFormatString = ("{0:dd/MM/yyyy}"))]
        public DateTime thoi_gian { set; get; }

        [DisplayName("Mã tư vấn")]
        public int tu_van_ref { set; get; }

        [DisplayName("Kiến nghị")]
        public string kien_nghi { set; get; }

        [DisplayName("ngày tạo")]
        [DisplayFormat(DataFormatString = ("{0:dd/MM/yyyy}"))]
        public DateTime created_at { set; get; }

        [DisplayName("Mã đơn vị")]
        public int don_vi_code { set; get; }

        [DisplayName("Kí hiệu")]
        public string ki_hieu { set; get; }
        public string guid { set; get; }
        public string link { set; get; }
        public int? rowThoiGian { set; get; }
        public int? rowDonVi { set; get; }
        public int? rowTuVan { set; get; }
    }
}
