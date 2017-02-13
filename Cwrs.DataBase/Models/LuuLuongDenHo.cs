using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class LuuLuongDenHo_giatri
    {
        public DateTime thoi_gian { get; set; }
        public int phuong_an_code { get; set; }
        public int tu_van_code { get; set; }
        public int don_vi_code { get; set; }
        public int vi_tri_ref { get; set; }
        public double gia_tri { get; set; }
    }
    public class LuuLuongDenHo1
    {
        public DateTime thoi_gian { get; set; }
        public double? luuLuong_KHTL { get; set; }
        public double? luuLuong_DHTL { get; set; }
        public double? luuLuong_VCH { get; set; }
        public double? luuLuong_KTTV { get; set; }
        public double? luuLuong_KTTVTW { get; set; }
        public double? luuLuong_VQHTL { get; set; }
        public int? vi_tri_code { get; set; }
        public int? phuong_an_code { get; set; }
        public LuuLuongDenHo1() { }
    }
    public class LuuLuongDenHo
    {
        public DateTime thoi_gian { get; set; }
        public List<double> luuLuong_KHTL { get; set; }
        public List<double> luuLuong_DHTL { get; set; }
        public List<double> luuLuong_VCH { get; set; }
        public List<double> luuLuong_KTTV { get; set; }
        public List<double> luuLuong_KTTVTW { get; set; }
        public List<double> luuLuong_VQHTL { get; set; }
        public int? vi_tri_code { get; set; }
        public int rowspan { get; set; }
        public LuuLuongDenHo() {
            luuLuong_DHTL = new List<double>();
            luuLuong_KHTL = new List<double>();
            luuLuong_KTTVTW = new List<double>();
            luuLuong_VCH = new List<double>();
            luuLuong_VQHTL = new List<double>();
            luuLuong_KTTV = new List<double>();
            rowspan = 1; }
    }
}
