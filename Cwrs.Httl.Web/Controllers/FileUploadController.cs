using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Cwrs.Httl.Web.Models;
using System.Net;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase;
using Cwrs.Lib;

namespace Cwrs.Httl.Web.Controllers
{
    public class FileUploadController : BaseController
    {
        // GET: FileUpload
        //UploadFileCtrl upload = new UploadFileCtrl();
        FilesHelper filesHelper;
        String tempPath = "~/somefiles/";
        String serverMapPath = "~/Files/somefiles/";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Files/somefiles/";
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public FileUploadController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }

        public ActionResult Index()
        {
            //if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            //{
            //    return View("Index", "DangNhap");
            //}
            //if (_common.CheckAdmin())
            //{
            //    return View("Index", "Download");
            //}
            //return View();
            // ViewBag.IsAdmin = _common.CheckAdmin();

            return new CommonWeb().CheckPage();
        }
        public ActionResult Show()
        {
            JsonFiles ListOfFiles = filesHelper.GetFileList();
            var model = new FilesViewModel()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;
            filesHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);
            ViewDataUploadFilesResult Result = resultList.ElementAt(resultList.Count - 1);
            upload_file_tb filedetail = new upload_file_tb()
            {
                upload_file_name = Result.name,
                deleted_status = 0,
                user_ref = Lib.Security.CurrentUserName,
                link = Result.url,
                created_at = DateTime.Now
            };
            bool error = _uploadfile.Insert(filedetail);
            if (!error) return Json("Error");

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error");
            }
            else
            {
                return Json(files);
            }
        }
        public JsonResult GetFileList()
        {
            var list = filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexPar()
        {
            //if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            //{
            //    return View("Index", "DangNhap");
            //}
            //if (_common.CheckAdmin())
            //{
            //    return View("Index", "Download");
            //}
            //return View();
            ViewBag.IsAdmin = _common.CheckAdmin();
            return new CommonWeb().CheckPagePar();
        }
        /// <summary>
        /// HieuTM: Download File
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FileResult DownloadPetPicture(int fileuploadcode)
        {
            var name = (string)RouteData.Values["id"];
            //var picture = "/Content/Uploads/" + name + ".jpg";
            upload_file_tb tmp = _uploadfile.GetUploadById(fileuploadcode);
            if (tmp == null)
            {
                //return HttpNotFound();
            }
            var picture = tmp.link;
            var contentType = "image/jpg";
            var fileName = Lib.Security.CurrentUserName + "_" + tmp.upload_file_name;
            return File(picture, contentType, fileName);
        }
    }
}