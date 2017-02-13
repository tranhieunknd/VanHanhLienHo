using Cwrs.DataBase;
using System;
using Cwrs;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cwrs.DataBase.Models;
using Cwrs.Httl.Web.Models;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using Cwrs.DataBase.VanHanhLienHo;

namespace Cwrs.Httl.Web.Controllers
{
    public class PhuongAnController : BaseController
    {

        // GET: PhuongAn
        public ActionResult Index()
        {

            IEnumerable<PhuongAn> dv = _phuongan.Select7H();
            IEnumerable<PhuongAnChuyenDoi> pa = null;
            if (dv != null & dv.Count() > 0)
            {
                pa = _phuongan.ChuyenDoi(dv.ToList());
            }
            return View(pa);
        }
        public PhuongAn thongtin()
        {
            PhuongAn pa = new PhuongAn();
            return pa;
        }
        public ActionResult QuyDinhQuyTrinh()
        {

            IEnumerable<PhuongAn> dv = _phuongan.Select7H();
            IEnumerable<PhuongAnChuyenDoi> pa = null;
            if (dv != null & dv.Count() > 0)
            {
                pa = _phuongan.ChuyenDoi(dv.ToList());
            }
            return PartialView("_QuyDinhQuyTrinh", pa);
        }
        public JsonResult FillTongHop()     // lấy ra tổng hợp cuối ở phần cuối trang chủ
        {
            PhuongAn TongHop = new PhuongAn();

            TongHop = _phuongan.SelectTongHop();
            if (TongHop == null)
            {
                TongHop = new PhuongAn();
            }

            return Json(TongHop, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveTongHop(string PostData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            PhuongAn listposition = serializer.Deserialize<PhuongAn>(PostData);
            bool check = _phuongan.SaveTongHop(listposition);
            if (check == true)
            {
                return Json(-2);
            }
            else
            {
                return Json(-1);
            }
        }

    }
}