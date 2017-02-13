using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;
using System.IO;
using Cwrs;
using System.Collections;
using Cwrs.DataBase;
using System.Data.Entity;
using PagedList;
using Cwrs.Httl.Web.Models;
using System.Web.Hosting;

namespace Cwrs.Httl.Web.Controllers
{
    public class DownloadController : BaseController
    {
        // GET: Download
        /// <summary>
        /// PhatPT: View Hiển thị và download file
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int ? page)
        {
            
            List<upload_file_tb> kq = _uploadfile.GetUpload();
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            ViewBag.Write = _common.WriteData(_uploadfile.error);
            ViewBag.Link = _common.getLinkLog();

            ViewBag.IsAdmin = _common.CheckAdmin();
            if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                return Redirect("/DangNhap/Index");
            }
            return View("Index", kq.ToPagedList(pageNumber, pageSize));
            // return new CommonWeb().CheckPage(kq.ToPagedList(pageNumber, pageSize));
            // return View(kq.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// PhatPT: Partial của view download file
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Page_Ajax(int? page) {
            List<upload_file_tb> kq = _uploadfile.GetUpload();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return PartialView("Page_Ajax", kq.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// HieuTM:
        /// </summary>
        /// <returns></returns>
        public FileResult DownloadPetPicture(string link)
        { 
            // var picture = "/Content/Uploads/" + name + ".jpg";
            var picture = Path.Combine(HostingEnvironment.MapPath("~"+link));
            var contentType = "application/vnd.ms-excel";
            var fileName =  Lib.Security.CurrentUserName + "_" + Path.GetFileNameWithoutExtension(picture) + Path.GetExtension(picture);
            return File(link, contentType, fileName);
        }
    }
}