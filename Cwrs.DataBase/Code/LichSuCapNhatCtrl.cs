using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.Models;
using Microsoft.SqlServer.Server;
using Npgsql;

namespace Cwrs.DataBase.Code
{
    public class LichSuCapNhatCtrl : Common
    {
        public IEnumerable<LichSuCapNhat> SelectAll(String userGuid)
        {
            int donViCode = SelectMaDonVi(userGuid);
            if (donViCode == -1) return null;
            string sql =
                @"select pa_tv.row_id guid, pa_tv.link, pa_tv.thoi_gian, pa_tv.tu_van_ref, pa_tv.kien_nghi, pa_tv.created_at, don_vi_code, dv.ki_hieu  from (select tv.link, tv.row_id, tv.thoi_gian, pa.tu_van_ref, pa.kien_nghi, pa.created_at, tv.don_vi_ref  from( phuong_an_tb pa
                                inner join tu_van_tb tv
                                on pa.tu_van_ref=tv.tu_van_code )) pa_tv
                                inner join don_vi_tb dv
                                on pa_tv.don_vi_ref = dv.don_vi_code
                                where don_vi_code=@DonViCode
                               order by thoi_gian desc, tu_van_ref desc, created_at desc";
            IEnumerable<LichSuCapNhat> lichsu =
                _database._databaseContext.Database.SqlQuery<LichSuCapNhat>(sql, new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@DonViCode", donViCode)
                }).ToList();
            return lichsu;
        }

        public IEnumerable<LichSuCapNhat> SelectAllTongCuc()
        {
            string sql =
                @"select pa_tv.row_id guid, pa_tv.link, pa_tv.thoi_gian, pa_tv.tu_van_ref, pa_tv.kien_nghi, pa_tv.created_at, don_vi_code, dv.ki_hieu 
                            from (
	                            select tv.row_id, tv.link, tv.thoi_gian, pa.tu_van_ref, pa.kien_nghi, pa.created_at, tv.don_vi_ref 
	                            from( phuong_an_tb pa
                                inner join tu_van_tb tv
                                on pa.tu_van_ref=tv.tu_van_code )) pa_tv
                                inner join don_vi_tb dv
                                on pa_tv.don_vi_ref = dv.don_vi_code
                                order by thoi_gian desc, don_vi_code desc, tu_van_ref desc,created_at desc";
            IEnumerable<LichSuCapNhat> lichsu =
                _database._databaseContext.Database.SqlQuery<LichSuCapNhat>(sql).ToList();
            return lichsu;
        }

        public int SelectMaDonVi(string userGuid)                                   // chuyển từ mã userGuid về mã đơn vị
        {
            string sql = @"select don_vi_ref from user_tb where user_guid='"+userGuid+"' ";
            List<int> lst =
                _database._databaseContext.Database.SqlQuery<int>(sql).ToList();
            if (lst != null && lst.Count > 0) return lst[0]; 
            return -1;

        }
    }
}
