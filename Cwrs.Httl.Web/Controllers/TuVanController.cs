using System;
using System.Collections.Generic;
//using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Web.Services.Description;
using Cwrs.Httl.Web.Models;
using System.Text;
using Cwrs.DataBase.Models;
using System.IO;
using System.Web.Hosting;
using Cwrs.DataBase;
using Cwrs.Lib;
using Cwrs.Core;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.LogDB;

namespace Cwrs.Httl.Web.Controllers
{
    public class TuVanController : BaseController
    {
        // GET: TuVan
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public PartialViewResult Index(HttpPostedFileBase fileexcel)
        {
            BanTinTuVan bantintuvan = new BanTinTuVan();
            if (fileexcel != null && fileexcel.ContentLength > 0)
            {
                try
                {
                    string name = ReadNameFile(fileexcel.FileName);
                    string path = "~/Files/somefiles/"; // path: đường dẫn lưu fileexcel tải lên
                    Path.Combine(HostingEnvironment.MapPath(path));
                    String pathOnServer = Path.Combine(path);
                    //var fullPath = Path.Combine(pathOnServer, Path.GetFileName(name));
                    var fullPath = Path.Combine(Server.MapPath(path),
                                      Path.GetFileName(name));
                    fileexcel.SaveAs(fullPath);// lua file vào server
                    string url = path + name; // url: đường dẫn lưu file excel trên server
                    ReadExcel excel = new ReadExcel();
                    url = Path.Combine(HostingEnvironment.MapPath(url));
                    List<DataTable> table = excel.makeDataTableFromSheetName(url, excel.MySheet(url));
                    bantintuvan = excel.BanTinTuVanExcel(table);
                }
                catch (Exception) { }
                return PartialView("TuVan_ViewChung", bantintuvan);
            }
            return PartialView();
        }

        public ActionResult KetQua()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Indexdemo(BanTinTuVan bantin)
        {
            return View("Index");
        }

        public string ReadNameFile(string fileName)
        {
            int index = fileName.IndexOf('.');
            string[] imageArray = fileName.Split('.');
            string tmp = fileName.Substring(0, index);
            string date = DateTime.Now.ToString("_yyyyMMdd_HHmmss.");
            return (tmp + date + imageArray[imageArray.Length - 1]);
        }

        /// <summary>
        /// HieuTM: Upload file, phân tích trả về mẫu dữ liệu.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UploadExcel()
        {
            LogDb.WriteLogInfo("HieuTM", "UploadExcel", "TuVanController", "Vao Upload");
            BanTinTuVan bttv = null;
            try
            {
                LogDb.WriteLogInfo("HieuTM", "UploadExcel", "TuVanController", "file count: " + Request.Files.Count.ToString());
                string linkSaveServer = "";
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    string directoryPath = Path.Combine((Server.MapPath("~/Files/tuvan/")));
                    var file = Request.Files[i];
                    string filename = file.FileName;

                    if (!System.IO.Directory.Exists(directoryPath)) System.IO.Directory.CreateDirectory(directoryPath);

                    string newname = Lib.Security.CurrentUserName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Path.GetFileName(file.FileName);
                    linkSaveServer = Path.Combine(Server.MapPath("~/Files/tuvan/"), newname);
                    ViewBag.LinkFile = linkSaveServer;
                    file.SaveAs(linkSaveServer);
                    ReadExcel excel = new ReadExcel();
                    List<DataTable> table = excel.makeDataTableFromSheetName(linkSaveServer, excel.MySheet(linkSaveServer));

                    bttv = table.Count == 0 ? null : excel.BanTinTuVanExcel(table);
                    if (bttv != null)
                    {
                        bttv.Link = @"Files/tuvan/" + newname;
                    }
                }
            }
            catch (Exception ex)
            {
                bttv = null;
                LogDb.WriteLogInfo("HieuTM", "UploadExcel", "TuVanController", "Error: " + ex.Message);
                LogDb.WriteLogError("HieuTM", "UploadExcel", "TuVanController", ex);
            }
            return PartialView("TuVan_All_Par", bttv);
        }

        public ActionResult InsertTuVan(BanTinTuVan bantintuvan)
        {
            // string _curentUsetID = Lib.Security.CurrentUserId;  //trả về mã gui(trong bảng user) nếu chuỗi trắng thì chưa đăng nhập.Khi nào đăng nhập thì mới được thêm
            //kiểm tra mã trong bảng usertb
            string _curentUsetGuid = Security.CurrentUserGuid;
            bool result = false;
            if (!string.IsNullOrEmpty(_curentUsetGuid))
            {
                user_tb tmpUser = _user.GetInfoUserVHLH(_curentUsetGuid);
                if (tmpUser != null & tmpUser.user_guid != null)
                    if (_tuvan.Insert(bantintuvan, tmpUser.don_vi_ref.Value))
                    {
                        result = true;
                    }
            }
            return Json(new { status = result });
        }

        //public ActionResult ThongTin()
        //{
        //    return View("ThongTin");
        //}

        // [Route("TuVan/GetInfoTuVan/{tuVanGuid?}")]
        public ActionResult GetInfoTuVan(string tuVanGuid)
        {
            BanTinTuVan bttv = null;
            try
            {
                if (!string.IsNullOrEmpty(Security.CurrentUserGuid))
                {
                    user_tb tmpUser = _user.GetInfoUserVHLH(Security.CurrentUserGuid);
                    tu_van_tb tmpTuVan = _tuvan.GetTuVaByGuid(tuVanGuid);
                    if (tmpUser != null && tmpTuVan != null)
                    {
                        if (tmpUser.don_vi_ref == tmpTuVan.don_vi_ref || _common.CheckAdmin())
                        {
                            bttv = _tuvan.GetInfoTuVanByGuid(tuVanGuid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bttv = null;
            }
            return View(bttv);
        }
    }
}