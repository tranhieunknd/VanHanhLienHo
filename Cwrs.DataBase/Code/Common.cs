using Cwrs.DataBase.VanHanhLienHo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.Models;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Cwrs.LogDB;

namespace Cwrs.DataBase.Code
{
    #region Log cu
    //public class Log
    //{
    //    DateTime _date = DateTime.Now;
    //    public string _link { get; set; }

    //    public void WriteFile(string link, string data)
    //    {
    //        FileStream fs;
    //        StreamWriter stw;
    //        try
    //        {
    //            #region Log Windows
    //            //string sSource;
    //            //string sLog;
    //            //string sEvent;

    //            //sSource = "LogVHLH";
    //            //sLog = data;
    //            //sEvent = "Sample Event";

    //            //if (!EventLog.SourceExists(sSource))
    //            //    EventLog.CreateEventSource(sSource, sLog);

    //            //EventLog.WriteEntry(sSource, sEvent);
    //            //EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Error);
    //            #endregion



    //            if (Directory.Exists(link))
    //            {
    //                fs = new FileStream(link, FileMode.Append, FileAccess.ReadWrite, FileShare.ReadWrite);
    //            }
    //            else
    //            {
    //                fs = new FileStream(link, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
    //            }

    //            stw = new StreamWriter(fs);
    //        }
    //        catch (Exception ex)
    //        {
    //            stw = new StreamWriter(link, true);
    //        }

    //        stw.WriteLine(DateTime.Now.ToString() + ": " + data);
    //        stw.Close();
    //    }

    //    public void WriteLog(string data)
    //    {
    //        string name = _date.Year.ToString();
    //        if (Directory.Exists(name))
    //        {
    //            name += '\\' + _date.Month.ToString();
    //            if (Directory.Exists(name))
    //            {
    //                name += string.Format(@"\log_{0}.txt", _date.ToString("yyyyMMdd"));
    //                WriteFile(name, data);
    //            }
    //            else
    //            {
    //                System.IO.Directory.CreateDirectory(name);
    //            }
    //        }
    //        else
    //        {
    //            System.IO.Directory.CreateDirectory(name);
    //        }
    //    }

    //    public void WriteError(string data)
    //    {
    //        WriteFile("E:\\log.txt", DateTime.Now.ToString() + ": " + data);
    //        //string name = _date.Year.ToString();
    //        //if (Directory.Exists(name))
    //        //{
    //        //    name += '\\' + _date.Month.ToString();
    //        //    if (Directory.Exists(name))
    //        //    {
    //        //        name += string.Format(@"\error_{0}.txt", _date.ToString("yyyyMMdd"));
    //        //        WriteFile(name, DateTime.Now.ToString() + ": " + data);
    //        //    }
    //        //    else
    //        //    {
    //        //        System.IO.Directory.CreateDirectory(name);
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    System.IO.Directory.CreateDirectory(name);
    //        //}
    //    }
    //}
    #endregion

    public class Log
    {
        private static string LogDir = "log.log";

        public static void setLogDir(String strLogDir)
        {
            LogDir = strLogDir;
        }

        public static String getLogDir()
        {
            String strLogDir = @ConfigurationManager.AppSettings["logPath"].ToString() + "\\log";
            setLogDir(strLogDir + getDate() + ".log");
            return LogDir;
        }

        public static String getLogDirError()
        {
            String strLogDir = @ConfigurationManager.AppSettings["logPath"].ToString() + "\\error";
            setLogDir(strLogDir + getDate() + ".log");
            return LogDir;
        }

        public Log()
        {
            setLogDir("log" + getDate() + ".log");
        }

        public Log(String strLogDir)
        {
            setLogDir(strLogDir + getDate() + ".log");
        }

        public static String getDate()
        {
            String date = DateTime.Now.Year.ToString();
            if (DateTime.Now.Month.ToString().Length <= 1)
            {
                date += "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                date += DateTime.Now.Month.ToString();
            }

            if (DateTime.Now.Day.ToString().Length <= 1)
            {
                date += "0" + DateTime.Now.Day.ToString();
            }
            else
            {
                date += DateTime.Now.Day.ToString();
            }

            return date;

        }

        public static void Write(String strData)
        {
            // Create a writer and open the file:
            StreamWriter log;
            if (!File.Exists(getLogDir()))
            {
                log = new StreamWriter(getLogDir());
            }
            else
            {
                log = File.AppendText(getLogDir());
            }

            // Write to the file:
            log.Write(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff") + ": ");
            log.WriteLine(strData);
            log.WriteLine();

            // Close the stream:
            log.Close();
        }

        public static void WriteLocation(String strData)
        {
            try
            {
                // Create a writer and open the file:
                StreamWriter log;
                string path = String.Format("logInfo_{0}", DateTime.Now.ToString("ddMMyyyy"));
                if (!File.Exists(path))
                {
                    log = new StreamWriter(path);
                }
                else
                {
                    log = File.AppendText(path);
                }

                // Write to the file:
                log.Write(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff") + ": ");
                log.WriteLine(strData);
                log.WriteLine();

                // Close the stream:
                log.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static void WriteError(String strData)
        {
            // Create a writer and open the file:
            StreamWriter log;
            if (!File.Exists(getLogDir()))
            {
                log = new StreamWriter(getLogDirError());
            }
            else
            {
                log = File.AppendText(getLogDirError());
            }

            // Write to the file:
            log.Write(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff") + ": ");
            log.WriteLine(strData);
            log.WriteLine();

            // Close the stream:
            log.Close();
        }

        public static void WriteErrorLocation(String strData)
        {
            try
            {
                // Create a writer and open the file:
                StreamWriter log;
                string path = String.Format("logError_{0}", DateTime.Now.ToString("ddMMyyyy"));
                if (!File.Exists(path))
                {
                    log = new StreamWriter(path);
                }
                else
                {
                    log = File.AppendText(path);
                }

                // Write to the file:
                log.Write(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff") + ": ");
                log.WriteLine(strData);
                log.WriteLine();

                // Close the stream:
                log.Close();
            }
            catch (Exception ex)
            {
            }
        }

    }

    public class LakeVitri
    {
        public string LakeCode { get; set; }
        public int ViTri { get; set; }
    }

    public class Common
    {
        protected string linkServer = ConfigurationManager.AppSettings["ServerAPI"].ToString();
        //protected Log _log = new Log();

        private LakeVitri[] LakeCode =
                {
            new LakeVitri {LakeCode = "c8023156-93ed-4657-a490-29cfc4ab8c1b", ViTri = 1},
            new LakeVitri {LakeCode = "15006573-718e-48c9-95e5-1b704d84a5fe", ViTri = 2},
            new LakeVitri {LakeCode = "d95808f0-d839-43de-96dd-71ff715ca4dd", ViTri = 3}
        };

        public string getLinkLog()
        {
            return Log.getLogDirError();
        }

        public Common() { }
        protected VanHanhLienHoDb _database = new VanHanhLienHoDb();

        /// <summary>
        /// HieuTM: Ham ho tro goi service tu ngoai
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link"></param>
        /// <param name="paramsmeter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public T CallTLVN<T>(string link, string paramsmeter, string method)
        {

            string url = link;

            var request = (HttpWebRequest)WebRequest.Create(url);
            string postData = paramsmeter;

            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            T dataTLVN = (T)JsonConvert.DeserializeObject(responseString, typeof(T));
            return dataTLVN;

        }

        public double GetDataLakeEVN(int vitricode, int datatype)
        {
            double result = -1;
            try
            {
                LakeVitri tmpLakeVitri = LakeCode.FirstOrDefault(x => x.ViTri == vitricode);
                string url = linkServer + "ApiService/GetThucHoHienTaiHoEVN";
                string param = String.Format(@"lakeCode={0}&dataType={1}", tmpLakeVitri.LakeCode, datatype);
                string method = "Post";
                result = CallTLVN<double>(url, param, method);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public double TimMucNuoc(int vitricode, double dungtich)
        {
            double result = -1;
            try
            {
                //switch (vitricode)
                //{
                //    case 1:
                //        dungtich = Math.Round(dungtich, 0);
                //        break;
                //    case 2:
                //        dungtich = Math.Round(dungtich, 1);
                //        break;
                //    case 3:
                //        dungtich = Math.Round(dungtich, 3);
                //        break;
                //}
                LakeVitri tmpLakeVitri = LakeCode.FirstOrDefault(x => x.ViTri == vitricode);
                string url = linkServer + "ApiService/TraCuuMucNuocByDungTichHo";
                string param = String.Format(@"lakecode={0}&dungtichho={1}", tmpLakeVitri.LakeCode, dungtich);
                string method = "Post";
                result = CallTLVN<double>(url, param, method);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public double TimDungTich(int vitricode, double mucnuoc)
        {
            double result = -1;
            try
            {
                LakeVitri tmpLakeVitri = LakeCode.FirstOrDefault(x => x.ViTri == vitricode);
                string url = linkServer + "ApiService/TraCuuDungTichHoByMucNuocHo";
                string param = String.Format(@"lakecode={0}&mucnuocho={1}", tmpLakeVitri.LakeCode, mucnuoc);
                string method = "Post";
                result = CallTLVN<double>(url, param, method);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// HieuTM: Lấy Danh sách trạng thái tài khoản cho combobox
        /// </summary>
        /// <returns></returns>
        public List<Item> SelectAccountStatusCbb()
        {
            List<Item> tmp = null;
            try
            {
                tmp = (from x in _database._databaseContext.danh_muc_tb
                       where x.loai_danh_muc_ref == 1 && x.deleted_status == 0
                       select new Item
                       {
                           Name = x.danh_muc_name,
                           Value = x.danh_muc_code
                       }).ToList();
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// HieuTM: Lấy tất cả Danh sách trạng thái tài khoản cho combobox
        /// </summary>
        /// <returns></returns>
        public List<Item> SelectAccountStatusFullCbb(bool isFull)
        {
            List<Item> tmp = null;
            try
            {
                tmp = (from x in _database._databaseContext.danh_muc_tb
                       where x.loai_danh_muc_ref == 1 && x.deleted_status == 0
                       select new Item
                       {
                           Name = x.danh_muc_name,
                           Value = x.danh_muc_code
                       }).ToList();
                if (isFull)
                    tmp.Insert(0, new Item
                    {
                        Name = "Tất cả",
                        Value = -1
                    });
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// HieuTM: Lấy danh sách đơn vị cho combobox
        /// </summary>
        /// <returns></returns>
        public List<Item> SelectDonViCbb()
        {
            List<Item> tmp = null;
            try
            {
                tmp = (from x in _database._databaseContext.don_vi_tb
                       where x.deleted_status == 0
                       select new Item
                       {
                           Name = x.don_vi_name,
                           Value = x.don_vi_code
                       }).ToList();
            }
            catch (Exception ex)
            {
            }
            return tmp;
        }

        /// <summary>
        /// HieuTM: Kiểm tra tài khoản có được xem hết các file dữ liệu không.
        /// </summary>
        /// <returns></returns>
        public bool CheckAdmin()
        {
            if (!String.IsNullOrEmpty(Lib.Security.CurrentGroup) & Lib.Security.CurrentGroup == "b9bacf66-2439-454d-868c-23d3b04c420e")
                return true;
            else
                return false;
        }

        /// <summary>
        /// HieuTM: Kiểm tra tài khoản bên TLVN
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public UserTLVN CheckLogin(string user, string pass)
        {
            UserTLVN result = null;
            try
            {
                #region Gọi service check đăng nhập bên TLVN
                string url = linkServer + "Home/AcountLoginVHLH";

                var request = (HttpWebRequest)WebRequest.Create(url);
                string postData = String.Format("userName={0}&passWord={1}", user, pass);

                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                #endregion

                #region Code cũ
                //// Truyền tham số cho reqest.

                //// Khởi tạo request goi API
                //WebRequest req = WebRequest.Create(url);
                //// req.Credentials = CredentialCache.DefaultCredentials;
                //req.ContentType = "application/x-www-form-urlencoded";
                //// Thiết lập phương thức cho request.
                //req.Method = "Post";

                ////byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                ////req.ContentLength = byteArray.Length;

                ////Stream dataStream = req.GetRequestStream();
                ////dataStream.Write(byteArray, 0, byteArray.Length);
                ////dataStream.Close();
                //// Nhận response.
                //WebResponse response = req.GetResponse();
                //Stream dataStream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(dataStream);
                //string responseFromServer = reader.ReadToEnd();
                //string str = responseFromServer;
                #endregion

                if (responseString == "-1")
                {
                    result = new UserTLVN();
                    result.UserId = -1;
                    result.GroupRef = "b9bacf66-2439-454d-868c-23d3b04c420e";
                    result.UserName = "Chua Xac Dinh";
                    result.FullName = "Chua Xac Dinh";
                    result.RoleRef = "asd";
                    result.IsAdminBitType = true;
                }
                else
                {
                    UserTLVN myDeserializedObjList = (UserTLVN)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString, typeof(UserTLVN));
                    result = myDeserializedObjList;
                }
                LogDb.WriteLogInfo("hieutm", "CheckLogin", "commonDataBase", "Đăng nhập thành công");
            }
            catch (Exception ex)
            {
                LogDb.WriteLogError("hieutm", "CheckLogin", "commonDataBase", ex);
                Log.WriteErrorLocation(ex.Message);
            }
            return result;

        }

        /// <summary>
        /// HieuTM: 
        /// </summary>
        /// <param name="vitriCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<ThucDoTLVN> GetThucDoTLVN(int vitriCode, DateTime fromDate, DateTime toDate)
        {
            List<ThucDoTLVN> result = null;
            try
            {
                if (CheckAdmin())
                {
                    result = new List<ThucDoTLVN>();

                    #region Gọi service check đăng nhập bên TLVN
                    string url = linkServer + "Home/GetThucDoByLake";

                    var request = (HttpWebRequest)WebRequest.Create(url);
                    string postData = String.Format("vitriCode={0}&fromDate={1}&toDate={2}", vitriCode, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));

                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    #endregion

                    result = (List<ThucDoTLVN>)JsonConvert.DeserializeObject(responseString, typeof(List<ThucDoTLVN>));
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
        public bool WriteData(string text)
        {
            bool result = false;
            try
            {
                Log.WriteError(text);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
