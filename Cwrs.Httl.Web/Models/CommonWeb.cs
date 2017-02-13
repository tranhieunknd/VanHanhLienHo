using Cwrs.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cwrs.Httl.Web.Models
{
    public class CommonWeb : BaseController
    {
        /// <summary>
        /// HieuTM: Check kiểm tra trang khi vào
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult CheckPage()
        {
            if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                return Redirect("/DangNhap/Index");
                //return View("Index", "DangNhap");
            }
            if (_common.CheckAdmin())
            {
                return Redirect("/Download/Index");
                //return View("Index", "Download");
            }
            return View();
        }
        /// <summary>
        /// HieuTM: Check kiểm tra trang khi vào
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult CheckPage(object obj)
        {
            if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                return Redirect("/DangNhap/Index");
            }
            if (_common.CheckAdmin())
            {
                return Redirect("/Download/Index");
            }
            return View(obj);
        }
        /// <summary>
        /// HieuTM: Check kiểm tra trang khi vào
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult CheckPage(string view, object obj)
        {
            if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                return Redirect("DangNhap/Index");
            }
            if (_common.CheckAdmin())
            {
                return Redirect("Download/Index");
            }
            return View(view, obj);
        }
        /// <summary>
        /// HieuTM: Check kiểm tra trang khi vào
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult CheckPage(string view)
        {
            if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                return Redirect("DangNhap/Index");
            }
            if (_common.CheckAdmin())
            {
                return Redirect("Download/Index");
            }
            return View(view);
        }

        /// <summary>
        /// HieuTM: Check kiểm tra trang khi vào
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult CheckPagePar()
        {
            if (string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                return Redirect("/DangNhap/Index");
                //return View("Index", "DangNhap");
            }
            if (_common.CheckAdmin())
            {
                return Redirect("/Download/Index");
                //return View("Index", "Download");
            }
            return PartialView();
        }
    }
}