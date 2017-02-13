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

        public JsonResult CapNhatThongTinUser(string PostData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            UserDetail listposition = serializer.Deserialize<UserDetail>(PostData);
            bool check = _user.EditUser(listposition);
            if (check == true)
            {
                return Json(-2);
            }
            else
            {
                return Json(-1);
            }
        }

        public JsonResult DeleteUser(int userId)
        {
            bool check = _user.deleteUser(userId);
            if (check == true)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public ActionResult CreateUser()
        {
            return View();

        }
    }
}
