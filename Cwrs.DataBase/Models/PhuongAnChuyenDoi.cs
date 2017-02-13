using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class PhuongAnChuyenDoi
    {
        public string Don_Vi { set; get; }
        public string Phuong_An { set; get; }

       [DisplayFormat(DataFormatString = ("{0:MM/dd/yyyy HH:mm}"))]
        public DateTime? Thoi_Diem { set; get; }
        public double? SonLa_XaDay { set; get; }
        public double? SonLa_XaMat { set; get; }
        public double? SonLa_LuuLuong { set; get; }
        public double? SonLa_MucNuoc { set; get; }

        public double? HoaBinh_XaDay { set; get; }
        public double? HoaBinh_XaMat { set; get; }
        public double? HoaBinh_LuuLuong { set; get; }
        public double? HoaBinh_MucNuoc { set; get; }

        public double? TuyenQuang_XaDay { set; get; }
        public double? TuyenQuang_XaMat { set; get; }
        public double? TuyenQuang_LuuLuong { set; get; }
        public double? TuyenQuang_MucNuoc { set; get; }

        public double? HaLuu_HaNoi { set; get; }
        public double? HaLuu_PhaLai { set; get; }
        public double? HaLuu_TuyenQuang { set; get; }
        public PhuongAnChuyenDoi() { }
        public int RowDonVi { get; set; }
        public int RowPhuongAn { get; set; }
        public int CodePhuongAn { get; set; }
        public int CodeDonVi { get; set; }
    }
}
