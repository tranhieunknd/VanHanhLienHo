using Cwrs.DataBase;
using Cwrs.DataBase.Models;
using Cwrs.Httl.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.Lib;

namespace Cwrs.Httl.Web.Controllers
{
    public class DangNhapController : BaseController
    {
        // GET: DangNhap
        public ActionResult Index(string x)
        {
            if (!string.IsNullOrEmpty(Lib.Security.CurrentUserId))
            {
                if (x == Lib.Security.CurrentUserId)
                {
                    FormsAuthentication.SetAuthCookie("|||||", true);
                    return View();
                }


                if (_common.CheckAdmin())
                {
                    return Redirect("/Download/Index");
                    //return View("Index", "Download");
                }
                else
                {
                    return Redirect("/FileUpload/Index");
                }
            }
            return View();
            // return View();
        }

        public ActionResult LoginNew()
        {
            return View();
        }

        /// <summary>
        /// HiemTM: Kiểm tra thông tin đăng nhập từ TLVN
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CheckLogin(string username, string password)
        {
            Cwrs.DataBase.Models.UserTLVN user = _common.CheckLogin(username, password);
            if (user != null)
            {
                Response.Cookies["TLVNAppToken"].Value = "VHLH";
                Response.Cookies["TLVNAppToken"].Expires = DateTime.Now.AddDays(7);

                string stringJson = JsonConvert.SerializeObject(user);
                // FormsAuthentication.SetAuthCookie(user.UserId + "|" + user.UserName + "|" + user.FullName + "|" + user.RoleRef + "|" + user.IsAdminBitType + "|" + user.GroupRef, true);
                FormsAuthentication.SetAuthCookie(stringJson, true);

                return "/Home/Index";
                //if (!_common.CheckAdmin())
                //{
                //    return "/FileUpload/Index";
                //}
                //else
                //{
                //    return "/Download/Index";
                //}
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// HieuTM:
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ///
        [HttpPost]
        public string CheckLoginAjacMVC(FormCollection fc)
        {
            string username = fc["usName"].ToString();
            string password = fc["paWord"].ToString();
            Cwrs.DataBase.Models.UserTLVN user = _common.CheckLogin(username, password);
            if (user != null)
            {
                Response.Cookies["TLVNAppToken"].Value = "VHLH";
                Response.Cookies["TLVNAppToken"].Expires = DateTime.Now.AddDays(7);
                FormsAuthentication.SetAuthCookie(user.UserId + "|" + user.UserName + "|" + user.FullName + "|" + user.RoleRef + "|" + user.IsAdminBitType + "|" + user.GroupRef, true);

                if (!_common.CheckAdmin())
                {
                    return "/FileUpload/Index";
                }
                else
                {
                    return "/Download/Index";
                }
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// HiemTM: Đăng xuất hệ thống
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string LogOut()
        {
            FormsAuthentication.SetAuthCookie("|||||", true);
            return "/DangNhap/Index";
        }
        /// <summary>
        /// HiemTM: Đăng xuất hệ thống sử dụng load trang
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult LogOutMVC(string x)
        {
            if (x == Lib.Security.CurrentUserId)
            {
                FormsAuthentication.SetAuthCookie("|||||", true);
            }
            return View();
        }
        /// <summary>
        /// HieuTM
        /// </summary>
        /// <param name="token">Hash cua UserName+datetime</param>
        /// <param name="token1">Md5 của Datetime</param>
        /// <param name="token2">Md5 của UserID+datetime</param>
        /// <param name="token3">Md5 cua Json doi tuong User+Datetime</param>
        /// <returns></returns>
        private bool LoginTLVN(string token, string token1, string token2, string token3)
        {
            bool result = false;
            try
            {
                string strdate = Cwrs.Core.Utils.DeCryptMD5(token1, "VHLH");
                string struserid = Cwrs.Core.Utils.DeCryptMD5(token2, "VHLH");
                int indexuserid = struserid.IndexOf(strdate);
                if (indexuserid == -1) return false;
                struserid = struserid.Substring(0, indexuserid);
                string jsonuser = Cwrs.Core.Utils.DeCryptMD5(token3, "VHLH");
                jsonuser = struserid.Substring(0, struserid.IndexOf(strdate));


                Cwrs.Httl.Web.Models.UserTLVN dataTLVN = (Cwrs.Httl.Web.Models.UserTLVN)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonuser, typeof(Cwrs.Httl.Web.Models.UserTLVN));
                if (Convert.ToInt16(struserid) == dataTLVN.UserId)
                {

                }
                // tmp.GetHashCode();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// HieuTM: Xac minh thong tin tai khoan
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SetAuthCookie("|||||", true);
            return Json(new {status = true});
        }
        
        public JsonResult ThongTinUse()     // lấy ra thông tin của user
        {
            user_tb ThongTin = new user_tb();
            if (string.IsNullOrEmpty(Security.CurrentUserGuid.Trim()))  
            {
                ThongTin = null;
            }
            else
            {
                ThongTin = _user.ThongTinUser(Security.CurrentUserGuid);
            }
            return Json(ThongTin, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// HieuTM: Thay doi thong tin tai khoan trong he thong
        /// </summary>
        /// <param name="PostData"></param>
        /// <param name="passwordNew"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public JsonResult ChangeInForUser(string PostData, string passwordNew, string confirmPassword)    // đổi thông tin của user ( bên js em có các kiểu giá trị trả về, a xem rồi trả về cho phù hợp ạ 
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            user_tb listposition = serializer.Deserialize<user_tb>(PostData);
            if (_user.SetInfoAccount(listposition, passwordNew, confirmPassword))
            {
                return Json(-2);
            }
            else
            {
                return Json(-1);
            }
            // int x = _phuongan.udateUser(listposition, passwordNew);
            
        }
    }
}