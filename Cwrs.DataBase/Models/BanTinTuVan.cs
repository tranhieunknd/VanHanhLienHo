using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cwrs.DataBase.VanHanhLienHo;

namespace Cwrs.DataBase.Models
{
    public class BanTinTuVan
    {
        public string DuKien { get; set; }
        public string NhanXetTinhHinh { get; set; }
        public string Link { get; set; }
        public DateTime ThoiGian { get; set; }
        public List<DienBien_Models> DienBienExcel { get; set; }
        public List<Hientrang_Model> HienTrangExcel { get; set; }
        //public List<PhuongAnExcel> PhuongAnExcel { get; set; }
        private List<PhuongAnExcel> _phuongAnExcel = new List<PhuongAnExcel>();

        public List<PhuongAnExcel> PhuongAnExcel
        {
            get { return _phuongAnExcel; }
            set { _phuongAnExcel = value; }
        }
    }
}