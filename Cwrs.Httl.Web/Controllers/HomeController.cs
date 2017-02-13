using Cwrs.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cwrs.Lib;

namespace Cwrs.Httl.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.CallToController = true;
            if (string.IsNullOrEmpty(Security.CurrentUserGuid))
            {
                ViewBag.ActionHome = "LoginNew";
                ViewBag.ControllerHome = "DangNhap";
                ViewBag.LayoutHome = null;
                ViewBag.IsLogin = "0";
            }
            else
            {
                ViewBag.LayoutHome = "~/Views/Shared/_LayoutClipTwo.cshtml";
                if (!_common.CheckAdmin())
                {
                    ViewBag.ActionHome = "Index";
                    ViewBag.ControllerHome = "LichSuCapNhat";
                    ViewBag.DanhDauMenu = "DuLieuTinhToan";
                    ViewBag.AddBreadCrumb = "Dữ liệu tính toán";
                }
                else
                {
                    ViewBag.ActionHome = "Index";
                    ViewBag.ControllerHome = "PhuongAn";
                    ViewBag.DanhDauMenu = "PhuongAn";
                    ViewBag.AddBreadCrumb = "Phương án vận hành";
                }
                ViewBag.IsLogin = "1";
            }
            return View();
        }

        public ActionResult NotAccess()
        {
            return PartialView("_NotAccess");
        }
    }
}