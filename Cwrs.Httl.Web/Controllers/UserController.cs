using Cwrs.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PagedList;
using PagedList.Mvc;
using Cwrs.DataBase.Models;
using Cwrs.DataBase.VanHanhLienHo;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Cwrs.Httl.Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult SelectAllUser()
        {
            List<UserDetail> User =new List<UserDetail>();
            if (_common.CheckAdmin())
            {
                 User =  _user.selectAllUser();
                User.OrderByDescending(s => s.user_code);
            }
            else
            {
                User = null;
            }
            return View(User);
        }

        public ActionResult UserDetail(int UserId)
        {
            DataBase.Models.UserDetail ThongTin = _user.ThôngTinUser1(UserId);
            return View(ThongTin);
        }
        [HttpPost]
        public int CapNhatThongTinUser(UserDetail user)
        {
            int result =1;
            if (user.password == user.confirmPW)
            {
                user_tb tmp=new user_tb();
                tmp.username = user.username;
                tmp.password = user.password;
                tmp.email = user.email;
                tmp.dien_thoai = user.dien_thoai;
                tmp.dia_chi = user.dia_chi;
                tmp.ghi_chu = user.ghi_chu;
                tmp.user_guid = user.user_guid;

                bool check = _user.SetInfoAccount(tmp,user.password, user.confirmPW);
                if (check == true)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }
            }
            return result;
        }

        public JsonResult DeleteUser(int userId)
        {
            bool check = _user.deleteUser(userId);
            if (check == true)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateUser()
        {
            UserDetail y =new UserDetail();
            SelectList x = new SelectList(_user.getAllDonVi(), "Value", "Name");
            ViewBag.lstDonVi = x;
            return View(y);
        }
        [HttpPost]
        public int createUser(UserDetail user)
        {
            int result = 0;
            if (user.password == user.confirmPW)
            {
                bool check = _user.NewAccount(user);
                if (check == true)
                {
                    SelectList x = new SelectList(_user.getAllDonVi(), "Value", "Name",user.don_vi_ref);
                    ViewBag.lstDonVi = x;
                }
            }
            else
            {
                SelectList x = new SelectList(_user.getAllDonVi(), "Value", "Name",user.don_vi_ref);
                ViewBag.lstDonVi = x;
                result = 1;
            }
            return result;
        }

        public ActionResult TestMultiInput()
        {
            return View();
        }
    }
}
