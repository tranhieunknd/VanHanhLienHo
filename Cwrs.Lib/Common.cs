using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cwrs.DataBase.Linq;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using Cwrs.Lib.Model;

namespace Cwrs.Lib
{
    public class Common
    {
        public class Const
        {
            public const string CREATE = "Thêm mới";
            public const string UPDATE = "Cập nhật";
            public const string DELETE = "Xóa";
            public const string CANCEL = "Hủy";
            public const string EDIT = "Xem";
            public const string CLOSE = "Đóng";
            public const string ALL = "Tất cả";
            public const string SEARCH = "Tìm kiếm";
            public const string SEARCH_ADVANCE = "Tìm kiếm nâng cao";
            public const string TU_SO_LIEU = "Từ số";
            public const string DEN_SO_LIEU = "Đến số";
            public const string TROGIUP_NHAPSO_TIM_KIEM = "1,Nếu chỉ nhập vào ô 'Từ số', chương trình sẽ tìm tất cả những bản ghi thỏa mãn điều kiện >= giá trị này;</br>2, Nếu chỉ nhập vào ô 'Tới số', chương trình sẽ tìm tất cả những bản ghi thỏa mãn điều kiện <= giá trị này;</br>3, Nếu nhập giá trị vào cả 2 ô, chương trình sẽ tìm kiếm những bản ghi thỏa mãn điều kiện trong khoảng 2 giá trị này.";
            public const string DANG_NHAP_SAI_TAI_KHOAN_SUPER = "Tài khoản của Quý khách không có quyền xem thông tin trang này!";
            public const string RAIN_FORECAST_TYPE = "Rain";
            public const string RESULT_FORECAST_TYPE = "ResultSim";
            public const string NGUON_DU_LIEU_TU = "Nguồn dữ liệu từ: ";
        }
        public class Utils
        {
            public static string GetCurrentTime()
            {
                return DateTime.Now.ToString();
            }
            /// <summary>
            /// Định dạng dữ liệu với n số sau dấu phẩy
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="number"></param>
            /// <returns></returns>
            public static string FormatNumber(double obj, int number)
            {
                string formatComma = string.Empty;
                for (int i = 1; i <= number; i++)
                    formatComma += "0";
                return obj.ToString("0." + formatComma, CultureInfo.InvariantCulture);
            }
            /// <summary>
            /// Thêm số 0 nếu giá trị nhỏ hơn 10
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static string FormatNumberLessThan10(int obj)
            {
                if (obj < 10)
                    return "0" + obj.ToString();
                return obj.ToString();
            }
            /// <summary>
            /// Chuyển List string sang dạng sử dụng trong lệnh In của Sql
            /// </summary>
            /// <param name="lstValue"></param>
            /// <returns></returns>
            public static string ConvertListToSqlCommandIn(List<string> lstValue)
            {
                string result = string.Empty;
                foreach (string strValue in lstValue)
                {
                    result += "'" + strValue + "',";
                }
                if (result.EndsWith(","))
                {
                    result = result.Remove(result.Length - 1);
                    result = "(" + result + ")";
                }
                return result;
            }
            public static System.Boolean FileInUse(System.String file)
            {
                try
                {
                    if (!System.IO.File.Exists(file)) // The path might also be invalid.
                    {
                        return false;
                    }

                    using (System.IO.FileStream stream = new System.IO.FileStream(file, System.IO.FileMode.Open))
                    {
                        return false;
                    }
                }
                catch
                {
                    return true;
                }
            }
            public static string StationGetCompare(out string mucCanhBao, out double giaTriChamCanhBao, out double giaTriVuotCanhBao, object mucNuoc, object bd1, object bd2, object bd3, object luls, bool isShort)
            {
                string strCompare = string.Empty;
                //Sắp xếp theo giaTriVuotCanhBao DESC,giaTriChamCanhBao DESC
                mucCanhBao = string.Empty;
                giaTriChamCanhBao = 0;
                giaTriVuotCanhBao = 0;
                if (mucNuoc != null)
                {
                    double mucNuocValue = Convert.ToDouble(mucNuoc);
                    bool findOk = false;
                    if (bd1 != null && bd1.ToString() != string.Empty)
                    {
                        double mbd1 = Convert.ToDouble(bd1);

                        if (mucNuocValue <= mbd1)
                        {
                            mucCanhBao = "01";
                            giaTriChamCanhBao = mbd1 - mucNuocValue;

                            if (isShort)
                                strCompare = "< BĐ 1:" + Math.Round(giaTriChamCanhBao, 2);
                            else
                                strCompare = "Nhỏ hơn(<) mức báo động 1 là " + Math.Round(giaTriChamCanhBao, 2);
                            findOk = true;
                        }
                        if (bd2 != null && bd2.ToString() != string.Empty && !findOk)
                        {
                            double mbd2 = Convert.ToDouble(bd2);
                            if (mucNuocValue <= mbd2)
                            {
                                mucCanhBao = "02";
                                giaTriChamCanhBao = mbd2 - mucNuocValue;
                                giaTriVuotCanhBao = mucNuocValue - mbd1;
                                strCompare = ">BĐ 1:" + Math.Round(giaTriVuotCanhBao, 2) + ",< BĐ 2:" + Math.Round(giaTriChamCanhBao, 2);
                                findOk = true;
                            }
                            if (bd3 != null && bd3.ToString() != string.Empty && !findOk)
                            {
                                double mbd3 = Convert.ToDouble(bd3);
                                if (mucNuocValue <= mbd3)
                                {
                                    mucCanhBao = "03";
                                    giaTriChamCanhBao = mbd3 - mucNuocValue;
                                    giaTriVuotCanhBao = mucNuocValue - mbd2;

                                    strCompare = ">BĐ 2:" + Math.Round(giaTriVuotCanhBao, 2) + ",< BĐ 3:" + Math.Round(giaTriChamCanhBao, 2);
                                    findOk = true;
                                }
                                if (luls != null && luls.ToString() != string.Empty && !findOk)
                                {
                                    double mlls = Convert.ToDouble(luls);
                                    if (mucNuocValue <= mlls)
                                    {
                                        mucCanhBao = "04";
                                        giaTriChamCanhBao = mlls - mucNuocValue;
                                        giaTriVuotCanhBao = mucNuocValue - mbd3;

                                        strCompare = ">BĐ 3:" + Math.Round(giaTriVuotCanhBao, 2) + ",< Lũ LS:" + Math.Round(giaTriChamCanhBao, 2);
                                        findOk = true;
                                    }
                                    else if (!findOk)
                                    {
                                        mucCanhBao = "05";
                                        giaTriVuotCanhBao = mucNuocValue - mlls;
                                        if (isShort)
                                            strCompare = "Vượt LS:" + Math.Round(giaTriVuotCanhBao, 2);
                                        else
                                            strCompare = "Vượt(>) mức lũ lịch sử là " + Math.Round(giaTriVuotCanhBao, 2);
                                    }
                                }
                                else if (!findOk)
                                {
                                    mucCanhBao = "04";
                                    giaTriVuotCanhBao = mucNuocValue - mbd3;
                                    if (isShort)
                                        strCompare = "Vượt BĐ3:" + Math.Round(giaTriVuotCanhBao, 2);
                                    else
                                        strCompare = "Vượt BĐ 3 là " + Math.Round(giaTriVuotCanhBao, 2);
                                }
                            }
                            else if (!findOk)
                            {
                                mucCanhBao = "03";
                                giaTriVuotCanhBao = mucNuocValue - mbd2;
                                if (isShort)
                                    strCompare = "Vượt BĐ2:" + Math.Round(giaTriVuotCanhBao, 2);
                                else
                                    strCompare = "Vượt BĐ 2 là " + Math.Round(giaTriVuotCanhBao, 2);
                            }
                        }
                        else if (!findOk)
                        {
                            mucCanhBao = "02";
                            giaTriVuotCanhBao = mucNuocValue - mbd1;
                            if (isShort)
                                strCompare = "Vượt BĐ1:" + Math.Round(giaTriVuotCanhBao, 2);
                            else
                                strCompare = "Vượt BĐ 1 là " + Math.Round(giaTriVuotCanhBao, 2);
                        }
                    }
                }
                return strCompare;
            }
            public static string FormatLonNhoHttmTag(string strHtml)
            {
                string result = string.Empty;
                result = strHtml.Replace("<", "&lt;");
                result = result.Replace(">", "&gt;");
                return result;
            }
            public static string FormatDateTime(object objDateTime, bool isShort)
            {
                if (objDateTime != null)
                {
                    objDateTime = Convert.ToDateTime(objDateTime);
                    if (isShort)
                        return String.Format("{0:HH:mm}", objDateTime) + " " + String.Format("{0:d/M/yyyy}", objDateTime);
                    else
                        return String.Format("{0:HH:mm}", objDateTime) + " ngày " + String.Format("{0:d/M/yyyy}", objDateTime);
                }
                return string.Empty;
            }
            public static string GetNameTableName(object IsTramThuyVanBitType)
            {
                string strReturn = string.Empty;
                if (IsTramThuyVanBitType != null)
                    if (IsTramThuyVanBitType.ToString() != string.Empty)
                    {
                        if (IsTramThuyVanBitType.ToString() == "True")
                            strReturn = "TramKTTV";
                        if (IsTramThuyVanBitType.ToString() == "False")
                            strReturn = "TramDoMua";
                    }
                return strReturn;
            }
            /// <summary>
            /// Nhận cảnh báo vào cột So Sánh
            /// </summary>
            /// <param name="lakeFk"></param>
            /// <param name="qtlh"></param>
            /// <param name="currentCanhBao"></param>
            /// <returns></returns>
            public static string LakeGetCanhBaoSoSanh(string lakeFk, Cwrs.Lib.QTLH_BonHoLon qtlh, string currentCanhBao)
            {
                string canhbao = currentCanhBao;
                string bdDo = "Đang vận hành sai quy trình, Htl cho phép là: ";
                if (lakeFk == "HBH_02_00003")
                {
                    if (qtlh._QuyTrinh_HoaBinh == "red")
                        canhbao = bdDo + qtlh._Htl_Chophep_HoaBinh;
                }
                else if (lakeFk == "SLA_02_00003")
                {
                    if (qtlh._QuyTrinh_SonLa == "red")
                        canhbao = bdDo + qtlh._Htl_Chophep_SonLa;
                }
                else if (lakeFk == "YBI_02_00001")
                {
                    if (qtlh._QuyTrinh_ThacBa == "red")
                        canhbao = bdDo + qtlh._Htl_Chophep_ThacBa;
                }
                else if (lakeFk == "TQG_02_00001")
                {
                    if (qtlh._QuyTrinh_TuyenQuang == "red")
                        canhbao = bdDo + qtlh._Htl_Chophep_TuyenQuang;
                }
                return canhbao;
            }
            /// <summary>
            /// Get Icon cảnh báo cho liên hồ
            /// </summary>
            /// <param name="lakeFk"></param>
            /// <param name="qtlh"></param>
            /// <param name="currentIcon"></param>
            /// <returns></returns>
            public static string LakeIconLienHo(string lakeFk, Cwrs.Lib.QTLH_BonHoLon qtlh, string currentIcon)
            {
                string icon = currentIcon;
                string bdDo = "/Images/Kml/MarkRed.png";
                string bdVang = "/Images/Kml/MarkYellow.png";
                if (lakeFk == "HBH_02_00003")
                {
                    if (qtlh._QuyTrinh_HoaBinh == "red")
                        icon = bdDo;
                    else if (qtlh._QuyTrinh_HoaBinh == "yel")
                        icon = bdVang;
                }
                else if (lakeFk == "SLA_02_00003")
                {
                    if (qtlh._QuyTrinh_SonLa == "red")
                        icon = bdDo;
                    else if (qtlh._QuyTrinh_SonLa == "yel")
                        icon = bdVang;
                }
                else if (lakeFk == "YBI_02_00001")
                {
                    if (qtlh._QuyTrinh_ThacBa == "red")
                        icon = bdDo;
                    else if (qtlh._QuyTrinh_ThacBa == "yel")
                        icon = bdVang;
                }
                else if (lakeFk == "TQG_02_00001")
                {
                    if (qtlh._QuyTrinh_TuyenQuang == "red")
                        icon = bdDo;
                    else if (qtlh._QuyTrinh_TuyenQuang == "yel")
                        icon = bdVang;
                }
                return icon;
            }
            /// <summary>
            /// Insert một loạt vào database
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connection"></param>
            /// <param name="tableName"></param>
            /// <param name="list"></param>
            public static void BulkInsertIList<T>(string connection, string tableName, IList<T> list)
            {
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BatchSize = list.Count;
                    bulkCopy.DestinationTableName = tableName;

                    var table = new DataTable();
                    var props = TypeDescriptor.GetProperties(typeof(T))
                                               //Dirty hack to make sure we only have system data types 
                                               //i.e. filter out the relationships/collections
                                               .Cast<PropertyDescriptor>()
                                               .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                               .ToArray();

                    foreach (var propertyInfo in props)
                    {
                        bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                        table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                    }

                    var values = new object[props.Length];
                    foreach (var item in list)
                    {
                        for (var i = 0; i < values.Length; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }

                        table.Rows.Add(values);
                    }

                    bulkCopy.WriteToServer(table);
                }
            }

            private static readonly string[] VietnameseSigns = new string[]
 {
  "aAeEoOuUiIdDyY",
  "áàạảãâấầậẩẫăắằặẳẵ",
  "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
  "éèẹẻẽêếềệểễ",
  "ÉÈẸẺẼÊẾỀỆỂỄ",
  "óòọỏõôốồộổỗơớờợởỡ",
  "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
  "úùụủũưứừựửữ",
  "ÚÙỤỦŨƯỨỪỰỬỮ",
  "íìịỉĩ",
  "ÍÌỊỈĨ",
  "đ",
  "Đ",
  "ýỳỵỷỹ",
  "ÝỲỴỶỸ"
 };

            public static string RemoveSign4VietnameseString(string str)
            {

                //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
                for (int i = 1; i < VietnameseSigns.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns[i].Length; j++)
                        str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
                return str;
            }
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class FileDownloadAttribute : ActionFilterAttribute
    {
        public FileDownloadAttribute(string cookieName = "fileDownload", string cookiePath = "/")
        {
            CookieName = cookieName;
            CookiePath = cookiePath;
        }

        public string CookieName { get; set; }

        public string CookiePath { get; set; }

        /// <summary>
        /// If the current response is a FileResult (an MVC base class for files) then write a
        /// cookie to inform jquery.fileDownload that a successful file download has occured
        /// </summary>
        /// <param name="filterContext"></param>
        private void CheckAndHandleFileResult(ActionExecutedContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var response = httpContext.Response;

            if (filterContext.Result is FileResult)
                //jquery.fileDownload uses this cookie to determine that a file download has completed successfully
                response.AppendCookie(new HttpCookie(CookieName, "true") { Path = CookiePath });
            else
                //ensure that the cookie is removed in case someone did a file download without using jquery.fileDownload
                if (httpContext.Request.Cookies[CookieName] != null)
            {
                response.AppendCookie(new HttpCookie(CookieName, "true") { Expires = DateTime.Now.AddYears(-1), Path = CookiePath });
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            CheckAndHandleFileResult(filterContext);

            base.OnActionExecuted(filterContext);
        }
    }
    public class Security
    {
        /// <summary>
        /// Kiểm tra xem tài khoản có phải là User khách hay không
        /// </summary>
        /// <returns></returns>
        public static bool IsGuest()
        {
            string userId = CurrentUserId;
            bool isGuest = false;
            if (userId == string.Empty)
                isGuest = true;
            return isGuest;
        }
        public static bool IsAdmin()
        {
            string userId = CurrentUserId;
            bool isAdmin = false;
            if (userId == string.Empty)
                isAdmin = true;
            return isAdmin;
        }

        /// <summary>
        /// Nhận UserId đăng nhập hiện tại
        /// </summary>
        public static string CurrentUserId
        {
            get
            {
                string userId = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    String jsonString = HttpContext.Current.User.Identity.Name;
                    try
                    {
                        SecurityUser jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SecurityUser>(jsonString);
                        // userId = HttpContext.Current.User.Identity.Name.Split('|')[0];
                        userId = jObject.UserId.ToString();
                    }
                    catch (Exception)
                    {
                    }

                }

                return userId;
            }
        }
        /// <summary>
        /// Nhận UserName đăng nhập hiện tại
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                string userName = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    try
                    {
                        String jsonString = HttpContext.Current.User.Identity.Name;
                        SecurityUser jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SecurityUser>(jsonString);
                        //userName = HttpContext.Current.User.Identity.Name.Split('|')[1];
                        userName = jObject.UserName;
                    }
                    catch (Exception)
                    {
                    }
                }

                return userName;
            }
        }
        /// <summary>
        /// Nhận User full name đăng nhập hiện tại
        /// </summary>
        public static string CurrentUserFullName
        {
            get
            {
                string userFullName = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    try
                    {
                        String jsonString = HttpContext.Current.User.Identity.Name;
                        SecurityUser jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SecurityUser>(jsonString);
                        userFullName = jObject.UserName; //HttpContext.Current.User.Identity.Name.Split('|')[2];
                    }
                    catch (Exception)
                    {
                    }
                }

                return userFullName;
            }
        }
        /// <summary>
        /// Nhận Role hiện tại
        /// </summary>
        public static string CurrentUserRole
        {
            get
            {
                string roleId = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    try
                    {
                        String jsonString = HttpContext.Current.User.Identity.Name;
                        SecurityUser jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SecurityUser>(jsonString);
                        roleId = jObject.RoleRef; //HttpContext.Current.User.Identity.Name.Split('|')[3];
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return roleId;
            }
        }
        /// <summary>
        /// Nhận User Guid hien tai
        /// </summary>
        public static string CurrentUserGuid
        {
            get
            {
                string userGuid = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    try
                    {
                        String jsonString = HttpContext.Current.User.Identity.Name;
                        SecurityUser jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SecurityUser>(jsonString);
                        userGuid = jObject.UserGuid; //HttpContext.Current.User.Identity.Name.Split('|')[3];
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return userGuid;
            }
        }
        /// <summary>
        /// Nhận biết User admin
        /// </summary>
        public static bool CurrentUserAdmin
        {
            get
            {
                bool roleId = false;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    string strRoleId = HttpContext.Current.User.Identity.Name.Split('|')[4];
                    if (strRoleId != string.Empty)
                        roleId = Convert.ToBoolean(strRoleId);
                }

                return roleId;
            }
        }
        /// <summary>
        /// Nhận Gourp đăng nhập hiện tại
        /// </summary>
        public static string CurrentGroup
        {
            get
            {
                string userId = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    try
                    {
                        String jsonString = HttpContext.Current.User.Identity.Name;
                        SecurityUser jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SecurityUser>(jsonString);
                        userId = jObject.GroupRef; //HttpContext.Current.User.Identity.Name.Split('|')[5];
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return userId;
            }
        }
        /// <summary>
        /// Nhận danh sách các hồ hiện tại được quản lý
        /// </summary>
        public static string CurrentUserLakeList
        {
            get
            {

                string lakeList = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    if (!Cwrs.Lib.Security.IsGuest())
                    {
                        //lakeList = HttpContext.Current.User.Identity.Name.Split('|')[5];
                        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["VnLakev3ConnectionString"].ToString();
                        HttlLinqDataContext context = new HttlLinqDataContext(conn);
                        int iUserId = Convert.ToInt32(Security.CurrentUserId);
                        var ListUserLake = context.User_Lakes.Where(p => p.UserId == iUserId).ToList();
                        foreach (var UserLake in ListUserLake)
                        {
                            lakeList += UserLake.LakeCode + "|";
                        }
                        if (lakeList.EndsWith("|"))
                            lakeList = lakeList.Substring(0, lakeList.Length - 1);

                    }
                }

                return lakeList;
            }
        }
        /// <summary>
        /// Nhận biết User Supper Admin
        /// </summary>
        public static bool CurrentUserSupperAdmin
        {
            get
            {
                bool roleId = false;
                if (CurrentUserRole == "01")
                    roleId = true;
                return roleId;
            }
        }
        /// <summary>
        /// Nhận biết User Admin view all lake
        /// </summary>
        public static bool CurrentUserAdminViewAllLake
        {
            get
            {
                bool roleId = false;
                if (CurrentUserRole == "04")
                    roleId = true;
                return roleId;
            }
        }
        /// <summary>
        /// User được quyền thêm sửa xóa dữ liệu
        /// </summary>
        public static bool CurrentUserCrud
        {
            get
            {
                bool roleId = false;
                if (CurrentUserRole == "02")
                    roleId = true;
                return roleId;
            }
        }
        /// <summary>
        /// Nhận biết User của Cwrs hay User khác
        /// </summary>
        public static bool IsUserCwrs
        {
            get
            {
                var url = HttpContext.Current.Request.Url;
                //var baseurl = url.GetLeftPart(UriPartial.Authority);
                bool IsCwrs = false;
                if (url.AbsoluteUri.Contains("localhost") || url.AbsoluteUri.Contains(":199")
                    || url.AbsoluteUri.Contains("thuyloivietnam.vn") || url.AbsoluteUri.Contains("wris.vn")
                    || url.AbsoluteUri.Contains("vn-wris.vn"))
                    return false;
                if (string.IsNullOrEmpty(GetSubDomain(url)))
                {
                    IsCwrs = true;
                }
                return IsCwrs;
            }
        }
        /// Retrieves the subdomain from the specified URL.
        /// </summary>
        /// <param name="url">The URL from which to retrieve the subdomain.</param>
        /// <returns>The subdomain if it exist, otherwise null.</returns>
        private static string GetSubDomain(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                string host = url.Host;
                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".");
                    int index = host.LastIndexOf(".", lastIndex - 1);
                    return host.Substring(0, index);
                }
            }

            return null;
        }
    }
    public class CryptorEngine
    {
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }

   
}