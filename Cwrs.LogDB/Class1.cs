using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.LogDB;
using Cwrs.LogDB.LogDB;
using System.Threading;
using System.Configuration;

namespace Cwrs.LogDB
{
    public class LogDb
    {
        private static string sysname = "";
        /// <summary>
        /// HieuTM: Ghi thông tin vao db
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sys"></param>
        /// <param name="fuc"></param>
        /// <param name="filef"></param>
        /// <param name="info"></param>
        public static void WriteLogInfo(string user, string fuc, string filef, string info)
        {
            new Thread(delegate()
            {
                try
                {
                    LogDBEntities _datacontext = new LogDBEntities();
                    sysname = ConfigurationManager.AppSettings["SystemName"].ToString();
                    log_tb newlog = new log_tb()
                        {
                            create_at = DateTime.Now,
                            create_by = user,
                            file = filef,
                            function = fuc,
                            is_error = 0,
                            messages = info,
                            system = sysname,
                            type = null
                        };
                    _datacontext.log_tb.Add(newlog);
                    _datacontext.SaveChanges();
                }
                catch(Exception ex)
                {
                }
            }).Start();
        }

        /// <summary>
        /// HieuTM: Ghi thông tin lỗi vào db
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sys"></param>
        /// <param name="fuc"></param>
        /// <param name="filef"></param>
        /// <param name="ex"></param>
        public static void WriteLogError(string user, string fuc, string filef, Exception ex)
        {
            new Thread(delegate()
           {
               try
               {
                   LogDBEntities _datacontext = new LogDBEntities();
                   sysname = ConfigurationManager.AppSettings["SystemName"].ToString();
                   log_tb newlog = new log_tb()
                   {
                       create_at = DateTime.Now,
                       create_by = user,
                       file = filef,
                       function = fuc,
                       is_error = 1,
                       messages = ex.Message,
                       system = sysname,
                       type = ex.GetType().Name
                   };
                   _datacontext.log_tb.Add(newlog);
                   _datacontext.SaveChanges();
               }
               catch
               {
               }
           }).Start();
        }
    }
}
