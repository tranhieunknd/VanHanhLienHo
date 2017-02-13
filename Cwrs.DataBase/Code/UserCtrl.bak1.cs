using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;
using System.Net;
using System.IO;
using Cwrs.Lib;

namespace Cwrs.DataBase.Code
{
    public class UserCtrl : Common
    {
        public UserCtrl() { }

        /// <summary>
        /// Hieutm: Lấy tất cả tài khoản 
        /// </summary>
        /// <param name="usercode">Mã người dùng hoặc -1 nếu muốn lấy tất cá</param>
        /// <returns> trả về danh sách tất cả tài khoản hoặc thông tin 1 tài khoản.</returns>
        public List<UserDetail> SelectByID(int usercode)
        {
            List<UserDetail> tmp = null;
            #region Linq lấy thông tin của tài khoản.
            try
            {
                tmp = (from x in _database._databaseContext.user_tb.Where(c => c.deleted_status == 0)
                       join y in (_database._databaseContext.danh_muc_tb.Where(c => c.loai_danh_muc_ref == 1 && c.deleted_status == 0))
                       on x.account_status equals y.danh_muc_code
                       where usercode == -1 || x.user_code == usercode
                       select new UserDetail
                       {
                           user_code = x.user_code,
                           username = x.username,
                           password = x.password,
                           email = x.email,
                           dien_thoai = x.dien_thoai,
                           dia_chi = x.dia_chi,
                           mo_ta = x.mo_ta,
                           deleted_status = x.deleted_status,
                           account_status_ref = x.account_status,
                           account_status_name = y.danh_muc_name,
                           ghi_chu = x.ghi_chu,
                           row_id = x.row_id,
                           don_vi_ref = x.don_vi_ref
                       }
                    ).ToList();
                //don_vi_name

                tmp = (from x in tmp
                       join y in _database._databaseContext.don_vi_tb.Where(c => c.deleted_status == 0)
                       on x.don_vi_ref equals y.don_vi_code
                       select new UserDetail
                       {
                           user_code = x.user_code,
                           username = x.username,
                           password = x.password,
                           email = x.email,
                           dien_thoai = x.dien_thoai,
                           dia_chi = x.dia_chi,
                           mo_ta = x.mo_ta,
                           deleted_status = x.deleted_status,
                           account_status_ref = x.account_status_ref,
                           account_status_name = x.account_status_name,
                           ghi_chu = x.ghi_chu,
                           row_id = x.row_id,
                           don_vi_ref = x.don_vi_ref,
                           don_vi_name = y.don_vi_name
                       }
                           ).ToList();

            }
            catch (Exception ex)
            {
            }
            #endregion
            return tmp;
        }

        /// <summary>
        /// HieuTM: Thêm mới hoặc cập nhật thay đổi cho tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(UserDetail user)
        {
            bool result = true;
            #region Kiểm tra tồn tại và cập nhật dữ liệu.
            try
            {
                user_tb tmp = _database._databaseContext.user_tb.FirstOrDefault(c => c.user_code == user.user_code);
                if (tmp == null)
                {
                    tmp = new user_tb
                    {
                        user_code = user.user_code,
                        username = user.username,
                        password = "abcd1234",
                        email = user.email,
                        dien_thoai = user.dien_thoai,
                        dia_chi = user.dia_chi,
                        mo_ta = user.mo_ta,
                        deleted_status = 0,
                        account_status = user.account_status_ref,
                        ghi_chu = user.ghi_chu,
                        row_id = user.row_id,
                        don_vi_ref = user.don_vi_ref
                    };
                    _database._databaseContext.user_tb.Add(tmp);
                }
                else
                {
                    tmp.user_code = user.user_code;
                    tmp.username = user.username;
                    tmp.password = user.password;
                    tmp.email = user.email;
                    tmp.dien_thoai = user.dien_thoai;
                    tmp.dia_chi = user.dia_chi;
                    tmp.mo_ta = user.mo_ta;
                    tmp.deleted_status = user.deleted_status;
                    tmp.account_status = user.account_status_ref;
                    tmp.ghi_chu = user.ghi_chu;
                    tmp.row_id = user.row_id;
                    tmp.don_vi_ref = user.don_vi_ref;
                }
                _database._databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = false;
            }
            #endregion
            return result;
        }

        /// <summary>
        /// HieuTM: Lấy thông tin chi tiết theo dữ liệu user trên db VHLH
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public user_tb GetInfoUserVHLH(string userGuid)
        {
            user_tb result = null;
            try
            {
                result = _database._databaseContext.user_tb.FirstOrDefault(x => x.user_guid == userGuid);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// HieuTM: Cập nhật lại thông tin cho bảng user_tb
        /// </summary>
        /// <param name="accPassOld"></param>
        /// <param name="accPassNew"></param>
        /// <param name="accRePassNew"></param>
        /// <param name="accTel"></param>
        /// <param name="accEmail"></param>
        /// <param name="accGhiChu"></param>
        /// <param name="accDiaChi"></param>
        /// <returns></returns>
        public bool SetInfoAccount(user_tb data, string accPassNew, string accRePassNew)
        {
            bool result = false;
            try
            {
                #region Gọi service check cập nhật bên TLVN
                string url = linkServer + "Home/AcountAccSetVHLH";

                var request = (HttpWebRequest)WebRequest.Create(url);
                string postData = String.Format("userGuid={0}&accSetOldPassWord={1}&accSetPassWord={2}&accSetRePassWord={3}&accSetFullName={4}&accSetEmail={5}&accSetDiaChi={6}&inputAccSetTel={7}&accSetGhiChu",
                             Security.CurrentUserGuid, data.password, accPassNew, accRePassNew, data.username, data.email, data.dia_chi, data.dien_thoai, data.ghi_chu);

                var datatmp = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = datatmp.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(datatmp, 0, datatmp.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                #endregion

                if (responseString == "1")
                {
                    data.user_guid = Security.CurrentUserGuid;
                    if (UpdateUserDbVHLH(data, accPassNew) == 1)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }


        /// <summary>
        /// HieuTM: Cập nhật dữ liệu cho bảng user_tb của db postgress VHLH; 1: Thành công; 0: Thất bại; -1: Có lỗi cập nhật
        /// </summary>
        /// <param name="x"></param>
        /// <param name="passwordNew"></param>
        /// <returns></returns>
        private int UpdateUserDbVHLH(user_tb x, string passwordNew)
        {
            int result = 0;
            try
            {
                var user = _database._databaseContext.user_tb.FirstOrDefault(e => e.user_guid == x.user_guid);

                user.username = x.username;
                user.email = x.email;
                user.dia_chi = x.dia_chi;
                user.dien_thoai = x.dien_thoai;
                user.ghi_chu = x.ghi_chu;
                // user.password = passwordNew;
                _database._databaseContext.SaveChanges();
                result = 1;
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }

        public user_tb ThongTinUser(string x) // lấy ra thông tin user
        {
            var query = (from user in _database._databaseContext.user_tb
                         where user.user_guid == x
                         select user).FirstOrDefault();
            user_tb thongtin = query;
            return thongtin;
        }

        public List<UserDetail> selectAllUser()
        {
            List<UserDetail> user =new List<UserDetail>();
            user = (from us in _database._databaseContext.user_tb
                join dv in _database._databaseContext.don_vi_tb
                on us.don_vi_ref equals dv.don_vi_code
                select new UserDetail()
                {
                    user_code = us.user_code,
                    username = us.username,
                    password = us.password,
                    email = us.email,
                    mo_ta = us.mo_ta,
                    dia_chi = us.dia_chi,
                    deleted_status = us.deleted_status,
                    dien_thoai = us.dien_thoai,
                    ghi_chu = us.ghi_chu,
                    don_vi_ref = us.don_vi_ref,
                    don_vi_name = dv.ki_hieu
                }).ToList();
            return user;
        }

        public UserDetail ThôngTinUser1(int UserId)
        {
            UserDetail user = new UserDetail();
            user = (from us in _database._databaseContext.user_tb
                    join dv in _database._databaseContext.don_vi_tb
                    on us.don_vi_ref equals dv.don_vi_code
                    where us.user_code==UserId
                    select new UserDetail()
                    {
                        user_code = us.user_code,
                        username = us.username,
                        password = us.password,
                        email = us.email,
                        mo_ta = us.mo_ta,
                        dia_chi = us.dia_chi,
                        deleted_status = us.deleted_status,
                        dien_thoai = us.dien_thoai,
                        ghi_chu = us.ghi_chu,
                        don_vi_ref = us.don_vi_ref,
                        don_vi_name = dv.ki_hieu
                    }).FirstOrDefault();
            return user;
        }

        public bool EditUser(UserDetail user)
        {
            bool check = true;
            #region Kiểm tra tồn tại và cập nhật dữ liệu.
            try
            {
                user_tb tmp = _database._databaseContext.user_tb.FirstOrDefault(c => c.user_code == user.user_code);
                    tmp.username = user.username;
                    tmp.email = user.email;
                    tmp.dien_thoai = user.dien_thoai;
                    tmp.dia_chi = user.dia_chi;
                    tmp.ghi_chu = user.ghi_chu;
                
                _database._databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            #endregion
            return check;
        }

        public bool deleteUser( int userId )
        {
            bool check = true;
            #region Kiểm tra tồn tại và cập nhật dữ liệu.
            try
            {
                user_tb tmp = _database._databaseContext.user_tb.FirstOrDefault(c => c.user_code == userId);
                    tmp.deleted_status = 1;
                _database._databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                check = false;
            }
            #endregion
            return check;
        }
    }
}
