using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;

namespace Cwrs.DataBase.Code
{
    public class UploadFileCtrl : Common
    {
        public string error { get; set; }
        public List<upload_file_tb> result = new List<upload_file_tb>();
        /// <summary>
        /// Phạm Phát: Hàm GetUpLoad trả về thông tin của file tư vấn và link down, nếu checkAdmind=false-ko phải admin thì chỉ hiển thị những file tải lên của tài khản đó
        /// </summary>
        /// <returns></returns>
        public List<upload_file_tb> GetUpload()
        {
            try
            {
                if (CheckAdmin())
                {
                    var result1 = from u in _database._databaseContext.upload_file_tb
                                  orderby u.created_at descending
                                  select u;
                    result = result1.ToList();
                }

                else
                {
                    var x = from u in _database._databaseContext.upload_file_tb
                            where u.user_ref == Lib.Security.CurrentUserName
                            orderby u.created_at descending
                            select u;
                    result = x.ToList();
                    // sai so sanh user ref voi lib username
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                //_log.WriteError(ex.Message); Path.Combine(HostingEnvironment.MapPath(serverMapPath))
                //string url = Path.Combine(HostingEnvironment.MapPath("~/Log/log.txt"));
                // Log.WriteError(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// ThamNT: Thêm mới file vao DB
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public bool Insert(upload_file_tb up)
        {
            bool result = true;
            try
            {
                _database._databaseContext.upload_file_tb.Add(up);
                _database._databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
                result = false;
            }
            return result;
        }
        public upload_file_tb GetUploadById(int uploadfilecode)
        {
            upload_file_tb result = null;
            try
            {
                result = _database._databaseContext.upload_file_tb.FirstOrDefault(c => c.upload_file_code == uploadfilecode);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
