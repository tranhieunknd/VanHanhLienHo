using Cwrs.DataBase;
using Cwrs.DataBase.Code;
using Cwrs.DataBase.Models;
using Cwrs.DataBase.VanHanhLienHo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cwrs.Httl.Web.Controllers
{
    public class BieuDoController : BaseController
    {
        //// GET: BieuDo
        //public ActionResult Index()
        //{
        //    return View();
        //}
        #region Biểu đồ mực nước
        public ActionResult BieuDoMucNuoc()
        {
            List<MucNuoc_giatri> mngt = new List<MucNuoc_giatri>();
            mngt = _phuongan.GetAllMucNuocByThoiGian(1, DateTime.Now, 24);
            List<MucNuoc> mn = _phuongan.To_MucNuoc(mngt).ToList();
            return PartialView("_BieuDoMucNuoc", mn);
        }
        public ActionResult BieuDoMucNuocTable(int tenHo, DateTime thoiGianBatDau, int khoangTG)
        {
            List<MucNuoc_giatri> mngt = new List<MucNuoc_giatri>();
            mngt = _phuongan.GetAllMucNuocByThoiGian(tenHo, thoiGianBatDau, khoangTG);
            List<MucNuoc> mn = _phuongan.To_MucNuoc(mngt).ToList();
            if (mn.Count > 0)
            {
                int dem_VCH = (mn[0].mucNuoc_VCH != null) ? mn[0].mucNuoc_VCH.Count : 0;
                int dem_CPCTT = (mn[0].mucNuoc_KTTVTW != null) ? mn[0].mucNuoc_KTTVTW.Count : 0;
                int dem_DHTL = (mn[0].mucNuoc_DHTL != null) ? mn[0].mucNuoc_DHTL.Count : 0;
                int dem_KTTV = (mn[0].mucNuoc_KTTV != null) ? mn[0].mucNuoc_KTTV.Count : 0;
                int dem_QHTL = (mn[0].mucNuoc_VQHTL != null) ? mn[0].mucNuoc_VQHTL.Count : 0;
                int dem_KHTL = (mn[0].mucNuoc_KHTL != null) ? mn[0].mucNuoc_KHTL.Count : 0;
                ViewBag.soPA_VCH = dem_VCH;
                ViewBag.soPA_CPCTT = dem_CPCTT;
                ViewBag.soPA_DHTL = dem_DHTL;
                ViewBag.soPA_KTTV = dem_KTTV;
                ViewBag.soPA_QHTL = dem_QHTL;
                ViewBag.soPA_KHTL = dem_KHTL;
            }
            else
            {
                ViewBag.soPA_VCH = 0;
                ViewBag.soPA_CPCTT = 0;
                ViewBag.soPA_DHTL = 0;
                ViewBag.soPA_KTTV = 0;
                ViewBag.soPA_QHTL = 0;
                ViewBag.soPA_KHTL = 0;
            }
            return PartialView("_BieuDoMucNuoc_Table", mn);
        }
        public ActionResult BieuDoMucNuocJsonData(int tenHo, DateTime thoiGianBatDau, int khoangTG)
        {
            List<MucNuoc_giatri> mngt = new List<MucNuoc_giatri>();
            mngt = _phuongan.GetAllMucNuocByThoiGian(tenHo, thoiGianBatDau, khoangTG);
            List<MucNuoc> mn = _phuongan.To_MucNuoc(mngt).ToList();
            return Json(mn, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BieuDoMucNuoc_Data(int tenHo, DateTime thoiGianBatDau, int khoangTG)
        {
            return Json(_phuongandetail.GetValueDataTypeByLake(2, tenHo, thoiGianBatDau, khoangTG), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Biểu đồ lưu lượng
        public ActionResult BieuDoLuuLuong()
        {
            //IEnumerable<PhuongAn> pa = _phuongan.SelectAll();
            //IEnumerable<LuuLuongDenHo> ll = _phuongan.To_LuuLuongDenHo(pa.ToList(),1,DateTime.Now,24);
            //ll = _phuongan.Get_ViTri();
            List<LuuLuongDenHo_giatri> ll_val = _phuongan.GetAllLuuLuongByThoiGian(1, DateTime.Now, 24);
            List<LuuLuongDenHo> ll = new List<LuuLuongDenHo>();
            ll = _phuongan.To_LuuLuongDenHo(ll_val).ToList();

            return PartialView("_BieuDoLuuLuong", ll);
        }
        public ActionResult BieuDoLuuLuongJsonData(int hoCode, DateTime fromDate, int hours)
        {
            List<LuuLuongDenHo_giatri> ll_val = _phuongan.GetAllLuuLuongByThoiGian(hoCode, fromDate, hours);
            List<LuuLuongDenHo> ll = new List<LuuLuongDenHo>();
            ll = _phuongan.To_LuuLuongDenHo(ll_val).ToList();
            return Json(ll, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BieuDoLuuLuongTable(int hoCode, DateTime fromDate, int hours)
        {
            List<LuuLuongDenHo_giatri> ll_val = _phuongan.GetAllLuuLuongByThoiGian(hoCode, fromDate, hours);
            List<LuuLuongDenHo> ll = new List<LuuLuongDenHo>();
            ll = _phuongan.To_LuuLuongDenHo(ll_val).ToList();
            if(ll.Count > 0)
            { 
            int dem_VCH = (ll[0].luuLuong_VCH != null) ? ll[0].luuLuong_VCH.Count : 0;
            int dem_CPCTT = (ll[0].luuLuong_KTTVTW != null) ? ll[0].luuLuong_KTTVTW.Count : 0;
            int dem_DHTL = (ll[0].luuLuong_DHTL != null) ? ll[0].luuLuong_DHTL.Count : 0;
            int dem_KTTV = (ll[0].luuLuong_KTTV != null) ? ll[0].luuLuong_KTTV.Count : 0;
            int dem_QHTL = (ll[0].luuLuong_VQHTL != null) ? ll[0].luuLuong_VQHTL.Count : 0;
            int dem_KHTL = (ll[0].luuLuong_KHTL != null) ? ll[0].luuLuong_KHTL.Count : 0;
            ViewBag.soPA_VCH = dem_VCH;
            ViewBag.soPA_CPCTT = dem_CPCTT;
            ViewBag.soPA_DHTL = dem_DHTL;
            ViewBag.soPA_KTTV = dem_KTTV;
            ViewBag.soPA_QHTL = dem_QHTL;
            ViewBag.soPA_KHTL = dem_KHTL;
            }
            else
            {
                ViewBag.soPA_VCH = 0;
                ViewBag.soPA_CPCTT = 0;
                ViewBag.soPA_DHTL = 0;
                ViewBag.soPA_KTTV = 0;
                ViewBag.soPA_QHTL = 0;
                ViewBag.soPA_KHTL = 0;
            }

            return PartialView("_BieuDoLuuLuong_Table", ll.ToList());
        }
        //public ActionResult BieuDoLuuLuongChart()
        //{

        //}
        /// <summary>
        /// Phatpx: Trả về dữ liệu tên đơn vị, giá trị, ngày giờ cho biểu đồ lưu lượng
        /// </summary>
        /// <param name="tenHo"></param>
        /// <param name="thoiGianBatDau"></param>
        /// <param name="khoangTG"></param>
        /// <returns></returns>
        public ActionResult BieuDoLuuLuong_Data(int tenHo, DateTime thoiGianBatDau, int khoangTG)
        {
            //if (fc["tenHo"]!= null&& fc["thoiGianBatDau"]!=null && fc["khoangTG"]!= null)
            if (true)
                return Json(_phuongan.BieuDoLuuLuong_fill(tenHo, thoiGianBatDau, khoangTG), JsonRequestBehavior.AllowGet);
            else
                return PartialView("_BieuDoLuuLuong");

        }
        #endregion
        #region Biểu đồ hạ lưu
        public ActionResult BieuDoMucNuocHaLuu()
        {
            //IEnumerable<don_vi_tb> lst_DonVi = (IEnumerable<don_vi_tb>)_donvi.SelectDonViCbb();
            //MultiSelectList x = new MultiSelectList(lst_DonVi, "don_vi_code","don_vi_name");
            //ViewBag.lst_DonVi = (SelectList)lst_DonVi;
            IEnumerable<PhuongAn> pa = _phuongan.SelectAll();
            IList<MucNuocHaLuu> mnhl = _phuongan.To_MucNuocHaLuu(pa.ToList(), 1, DateTime.Now.AddDays(-1), 24);
            ViewBag.soPA = _phuongan.getSoPA("1", DateTime.Now);
            return PartialView("_BieuDoMucNuocHaLuu", mnhl);
        }
        public ActionResult BieuDoMucNuocHaLuu_test(int pa_Number, string ten_Dv, DateTime thoiGianBD, int khoangTG)
        {
            IList<Cls_MNHL> mnhl = _phuongan.LayDuLieuBieuDo_MNHL(pa_Number, ten_Dv, thoiGianBD, khoangTG);
            var HN = mnhl.Where(x => x.ten_Vi_Tri == "Hà Nội").Select(x => x).ToList();
            var PL = mnhl.Where(x => x.ten_Vi_Tri == "Phả Lại").Select(x => x).ToList();
            var TQ = mnhl.Where(x => x.ten_Vi_Tri == "Tx. Tuyên Quang").Select(x => x).ToList(); ;
            var query = (from u in HN
                         join p in PL
                         on u.thoi_Gian_Tao equals p.thoi_Gian_Tao
                         join t in TQ
                         on p.thoi_Gian_Tao equals t.thoi_Gian_Tao
                         select new MucNuocHaLuu
                         {
                             mucnuoc_HaNoi = u.gia_Tri,
                             mucnuoc_PhaLai = p.gia_Tri,
                             mucnuoc_TuyenQuang = t.gia_Tri,
                             thoi_gian = u.thoi_Gian_Tao
                         });

            //ViewBag.soPA = _phuongan.getSoPA(ten_Dv, thoiGianBD);
            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Change_PA(string ten_Dv, DateTime thoiGianBD)
        {
            return Json(_phuongan.getSoPA(ten_Dv, thoiGianBD));
        }
        public ActionResult BieuDoMucNuocHaLuuTable(int pa_Number, int donViCode, DateTime fromDate, int hours)
        {
            //List<MucNuoc_giatri> mngt = new List<MucNuoc_giatri>();
            //// mngt = _phuongan.GetAllMucNuocByThoiGian(donViCode, thoiGianBatDau, khoangTG);
            //List<MucNuoc> mnhl = _phuongan.To_MucNuocHaLuu(mngt).ToList();

            IEnumerable<PhuongAn> pa = _phuongan.LayDuLieuBieuDo_MNHL_DataTable_1PhuongAn(pa_Number, donViCode, fromDate, hours);
            IList<MucNuocHaLuu> mnhl = _phuongan.To_MucNuocHaLuu(pa.ToList(), donViCode, fromDate, hours);
            return PartialView("_BieuDoMucNuocHaLuu_Table", mnhl);
        }

        #endregion
        #region Biểu đồ so sánh
        public ActionResult BieuDoSoSanh()
        {
            IList<MucNuoc_giatri_SoSanh> mngt = _phuongan.BieuDoSoSanh_Select7H();
            IList<MucNuoc_SoSanh> mn = _phuongan.ChuyenDoi_MucNuoc(mngt.ToList(), 1, DateTime.Now.AddDays(-2), DateTime.Now);
            return PartialView("_BieuDoSoSanh", mn);
        }

        public JsonResult GetDataSoSanh(int viTriCode, DateTime fromDate, DateTime toDate)
        {
            IList<MucNuoc_giatri_SoSanh> mngt = _phuongan.BieuDoSoSanh_Select7H();
            IList<MucNuoc_SoSanh> mn = _phuongan.ChuyenDoi_MucNuoc(mngt.ToList(), viTriCode, fromDate, toDate);
            return Json(mn);
        }

        public ActionResult BieuDoSoSanhTable(int viTriCode, DateTime fromDate, DateTime toDate)
        {
            IList<MucNuoc_giatri_SoSanh> mngt = _phuongan.BieuDoSoSanh_Select7H();
            IList<MucNuoc_SoSanh> mn = _phuongan.ChuyenDoi_MucNuoc(mngt.ToList(), viTriCode, fromDate, toDate);
            return PartialView("_BieuDoSoSanh_Table", mn);
        }
        #endregion
        public ActionResult DemoBieuDo()
        {
            return View();
        }

        public JsonResult GetDataTuVanByHo(int dataType, int viTriCode, DateTime thoiGianBatDau, int khoangTG)
        {
            List<Cls_LuuLuong> tmpData = _phuongandetail.GetValueDataTypeByLake(dataType, viTriCode, thoiGianBatDau, khoangTG);

            List<string> DonViName = tmpData.Select(x => x.tenDonVi).Distinct().ToList();
            List<int> DonViCode = tmpData.Select(x => x.maDonVi).Distinct().ToList();
            return Json(new { DonVi = DonViName, Data = tmpData, Code = DonViCode });
        }
        public JsonResult MucNuocHaLuu_DaTaBieuDo(int donviCode, DateTime fromDate, int hours)
        {
            IEnumerable<PhuongAn> pa = _phuongan.SelectAll();
            IList<MucNuocHaLuu> mnhl = _phuongan.To_MucNuocHaLuu(pa.ToList(), donviCode, fromDate, hours);
            var MucNuoc_HaNoi = mnhl.Select(x => new { Ngay = x.thoi_gian, x.mucnuoc_HaNoi.Value }).ToList();
            var MucNuoc_PhaLai = mnhl.Select(x => new { Ngay = x.thoi_gian, x.mucnuoc_PhaLai.Value }).ToList();
            var MucNuoc_TuyenQuang = mnhl.Select(x => new { Ngay = x.thoi_gian, x.mucnuoc_TuyenQuang.Value }).ToList();
            var query = (from u in MucNuoc_HaNoi
                         join p in MucNuoc_PhaLai
                         on u.Ngay equals p.Ngay
                         join t in MucNuoc_TuyenQuang
                         on p.Ngay equals t.Ngay
                         select new MucNuocHaLuu
                         {
                             mucnuoc_HaNoi = u.Value,
                             mucnuoc_PhaLai = p.Value,
                             mucnuoc_TuyenQuang = t.Value,
                             thoi_gian = u.Ngay
                         });

            return Json(query.ToList(), JsonRequestBehavior.AllowGet);

        }
    }
}