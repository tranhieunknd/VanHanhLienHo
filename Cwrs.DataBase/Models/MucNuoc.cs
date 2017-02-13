using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class MucNuoc_giatri
    {
        public DateTime thoi_gian { get; set; }
        public int phuong_an_code { get; set; }
        public int tu_van_code { get; set; }
        public int don_vi_code { get; set; }
        public int vi_tri_ref { get; set; }
        public double gia_tri { get; set; }
        public MucNuoc_giatri() { }
    }
    public class MucNuoc
    {
        public DateTime thoi_gian { get; set; }
        public double MucNuocThucDo { get; set; }
        public double MucNuocQuyTrinh { get; set; }
        public List<double> mucNuoc_KHTL { get; set; }
        public List<double> mucNuoc_DHTL { get; set; }
        public List<double> mucNuoc_VCH { get; set; }
        public List<double> mucNuoc_KTTV { get; set; }
        public List<double> mucNuoc_KTTVTW { get; set; }
        public List<double> mucNuoc_VQHTL { get; set; }
        public int? vi_tri_code { get; set; }
        public int rowspan { get; set; }
        public MucNuoc()
        {
            mucNuoc_DHTL = new List<double>();
            mucNuoc_KHTL = new List<double>();
            mucNuoc_KTTVTW = new List<double>();
            mucNuoc_VCH = new List<double>();
            mucNuoc_VQHTL = new List<double>();
            mucNuoc_KTTV = new List<double>();
            rowspan = 1;
        }
    }
    public class MucNuoc_giatri_SoSanh
    {
        public DateTime ThoiGian { get; set; }
        //public int PhuongAnDetail { get; set; }
        public int PhuongAn { get; set; }
        public int TuVan { get; set; }
        public int DonVi { get; set; }
        public int ViTri { get; set; }
        public double GiaTri { get; set; }
        public MucNuoc_giatri_SoSanh() { }
    }
    public class MucNuoc_SoSanh
    {
        public DateTime thoi_gian { get; set; }
        public double MucNuocThucDo { get; set; }
        public double MucNuocQuyTrinh { get; set; }
        public double mucnuoc_KHTL { get; set; }
        public double mucnuoc_DHTL { get; set; }
        public double mucnuoc_VCH { get; set; }
        public double mucnuoc_KTTV { get; set; }
        public double mucnuoc_KTTVTW { get; set; }
        public double mucnuoc_VQHTL { get; set; }
        public int vi_tri_code { get; set; }
        public MucNuoc_SoSanh() { }
    }
}
