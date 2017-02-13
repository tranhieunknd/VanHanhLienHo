using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;
using System.Web.Mvc;

namespace Cwrs.DataBase.Code
{
    public class PhuongAnDetailCtrl : Common
    {
        /// <summary>
        /// HieuTM: Lấy dữ liệu dự báo trong nhiều bản tin tư vấn theo loại dữ liệu và vị trí lấy
        /// </summary>
        /// <param name="dataTypeRef">1: Lưu lượng; 2: Mực nước</param>
        /// <param name="viTriCode">1: Sơn La; 2: Hòa Bình; 3: Tuyên Quang</param>
        /// <param name="fromDate">Thời gian bắt đầu lấy</param>
        /// <param name="khoangThoiGian">Số giờ muốn lấy</param>
        /// <returns></returns>
        public List<Cls_LuuLuong> GetValueDataTypeByLake(int dataTypeRef,int viTriCode, DateTime fromDate, int khoangThoiGian)
        {
            List<Cls_LuuLuong> result = new List<Cls_LuuLuong>();

            DateTime toDate = fromDate.AddHours(khoangThoiGian); 
            try
            {
                #region Sql
                var query_tuvan = String.Format(@"WITH tuvanlive AS (
                                     select * 
                                     from(
                                      select row_number() over (partition by don_vi_ref ORDER BY tu_van_tb.created_at desc)as rownum,* 
                                      from tu_van_tb 
                                      join don_vi_tb
                                      on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
                                      where thoi_gian = '{0} 00:00'
                                     ) abc
                                     where abc.rownum = 1
                                    )

                                    select pad.gia_tri giaTri, pad.thoi_gian thoiGianTao, x.don_vi_name tenDonVi , x.don_vi_code maDonVi from
                                    (
                                     select tvl.don_vi_name, tvl.don_vi_code,
                                      row_number() over (partition by tvl.tu_van_code ORDER BY pa.created_at)as rownum, 
                                      pa.phuong_an_code,
                                      tvl.tu_van_code
                                     from phuong_an_tb pa
                                     join tuvanlive tvl
                                     on pa.tu_van_ref = tvl.tu_van_code
                                     order by tvl.tu_van_code, pa.created_at desc
                                    )x
                                    join phuong_an_detail_tb pad
                                    on x.phuong_an_code = pad.phuong_an_ref
                                    where x.rownum = 1 
                                        and pad.loai_du_lieu_ref = {1} 
                                        and pad.vi_tri_ref = {2}
					                    and (pad.thoi_gian between '{0} 07:00' and '{3} 07:00')
                                    order by pad.thoi_gian, x.don_vi_code, x.phuong_an_code", fromDate.ToString("yyyy-MM-dd"), dataTypeRef, viTriCode, toDate.ToString("yyyy-MM-dd"));
                result = _database._databaseContext.Database.SqlQuery<Cls_LuuLuong>(query_tuvan).AsEnumerable().ToList();
                #endregion
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
