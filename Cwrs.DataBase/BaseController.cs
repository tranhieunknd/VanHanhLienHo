using Cwrs.DataBase.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cwrs.DataBase
{
    public class BaseController : Controller
    {
        protected Common _common = new Common();
        protected UserCtrl _user = new UserCtrl();
        protected DanhMucCtrl _danhmuc = new DanhMucCtrl();
        protected DienBienCtrl _dienbien = new DienBienCtrl();
        protected DonViCtrl _donvi = new DonViCtrl();
        protected HienTrangCtrl _hientrang = new HienTrangCtrl();
        protected LoaiDanhMucCtrl _loaidanhmuc = new LoaiDanhMucCtrl();
        protected LoaiDuLieuCtrl _loaidulieu = new LoaiDuLieuCtrl();
        protected PhuongAnCtrl _phuongan = new PhuongAnCtrl();
        protected PhuongAnDetailCtrl _phuongandetail = new PhuongAnDetailCtrl();
        protected ViTriCtrl _vitri = new ViTriCtrl();
        protected UploadFileCtrl _uploadfile = new UploadFileCtrl();
        protected TuVanCtrl _tuvan = new TuVanCtrl();
        protected LichSuCapNhatCtrl _lichsucapnhat = new LichSuCapNhatCtrl();
        protected ThietLapXaCtrl _thietlapxa = new ThietLapXaCtrl();
        protected TongHopCtrl _tonghop = new TongHopCtrl();
    }
}
