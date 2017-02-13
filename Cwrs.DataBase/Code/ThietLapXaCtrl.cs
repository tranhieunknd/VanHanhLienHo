using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.Models;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.Lib;

namespace Cwrs.DataBase.Code
{
    public class ThietLapXaCtrl : Common
    {
        public bool UpdateKichBan(KichBanXa kichBanXa)
        {
            bool result = false;
            if (kichBanXa != null & !String.IsNullOrEmpty(Security.CurrentUserGuid))
                try
                {
                    thiet_lap_kich_ban_tb tmpThietLapKichBan = kichBanXa.KichBan;
                    List<thiet_lap_kich_ban_detail_tb> tmpLapKichBanDetail = kichBanXa.KichBanChiTiet;

                    if (tmpThietLapKichBan.thiet_lap_kich_ban_code == -1)
                    {
                        #region Insert moi

                        var vitri = _database._databaseContext.vi_tri_tb.FirstOrDefault(x => x.vi_tri_code == tmpThietLapKichBan.vi_tri_ref);

                        // Them moi thiet_lap_kich_ban_tb
                        tmpThietLapKichBan.created_at = DateTime.Now;
                        tmpThietLapKichBan.last_modified_at = DateTime.Now;
                        tmpThietLapKichBan.created_by = Security.CurrentUserName;
                        tmpThietLapKichBan.last_modified_by = Security.CurrentUserName;
                        tmpThietLapKichBan.deleted_status = 0;
                        tmpThietLapKichBan.user_ref = Convert.ToInt32(Security.CurrentUserId);
                        if (vitri != null)
                            tmpThietLapKichBan.thiet_lap_kich_ban_name = String.Format("Hồ {0}: Kịch bản {1}", vitri.vi_tri_name, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                        _database._databaseContext.thiet_lap_kich_ban_tb.Add(tmpThietLapKichBan);
                        _database._databaseContext.SaveChanges();

                        // Them moi thiet_lap_kich_ban_detail_tb
                        foreach (thiet_lap_kich_ban_detail_tb item in tmpLapKichBanDetail)
                        {
                            item.created_at = DateTime.Now;
                            item.last_modified_at = DateTime.Now;
                            item.created_by = Security.CurrentUserName;
                            item.last_modified_by = Security.CurrentUserName;
                            item.deleted_status = 0;
                            item.thiet_lap_kich_ban_ref = tmpThietLapKichBan.thiet_lap_kich_ban_code;
                            _database._databaseContext.thiet_lap_kich_ban_detail_tb.Add(item);
                        }
                        _database._databaseContext.SaveChanges();
                        result = true;
                        #endregion
                    }
                    else
                    {
                        #region update
                        // Cap nhat thiet_lap_kich_ban_tb
                        thiet_lap_kich_ban_tb tmpGoc = _database._databaseContext.thiet_lap_kich_ban_tb.FirstOrDefault(
                            x => x.thiet_lap_kich_ban_code == tmpThietLapKichBan.thiet_lap_kich_ban_code);
                        if (tmpGoc != null)
                        {
                            tmpGoc.muc_nuoc_ho = tmpThietLapKichBan.muc_nuoc_ho;
                            tmpGoc.luu_luong_ve_ho = tmpThietLapKichBan.luu_luong_ve_ho;
                            tmpGoc.last_modified_at = DateTime.Now;
                            tmpGoc.last_modified_by = Security.CurrentUserName;
                            tmpGoc.deleted_status = 0;

                            _database._databaseContext.thiet_lap_kich_ban_tb.Attach(tmpGoc);
                            _database._databaseContext.Entry(tmpGoc).State = EntityState.Modified;
                            _database._databaseContext.SaveChanges();

                            // Them moi thiet_lap_kich_ban_detail_tb
                            string sqlDelete = String.Format(@"update thiet_lap_kich_ban_detail_tb
                                set deleted_status = 1
                                where thiet_lap_kich_ban_ref = {0}", tmpGoc.thiet_lap_kich_ban_code);
                            _database._databaseContext.Database.ExecuteSqlCommand(sqlDelete);

                            foreach (thiet_lap_kich_ban_detail_tb item in tmpLapKichBanDetail)
                            {
                                item.created_at = DateTime.Now;
                                item.last_modified_at = DateTime.Now;
                                item.created_by = Security.CurrentUserName;
                                item.last_modified_by = Security.CurrentUserName;
                                item.deleted_status = 0;
                                item.thiet_lap_kich_ban_ref = tmpThietLapKichBan.thiet_lap_kich_ban_code;
                                _database._databaseContext.thiet_lap_kich_ban_detail_tb.Add(item);
                            }
                            _database._databaseContext.SaveChanges();
                            result = true;
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                }
            return result;
        }

        public KichBanXa GetInfoByKichBanCode(int kichBanCode)
        {
            KichBanXa result = null;
            try
            {
                result = new KichBanXa();
                thiet_lap_kich_ban_tb tmpThietLapKichBan =
                    _database._databaseContext.thiet_lap_kich_ban_tb.FirstOrDefault(
                        x => x.deleted_status == 0 && x.thiet_lap_kich_ban_code == kichBanCode);
                if (tmpThietLapKichBan != null)
                {
                    result.KichBan = tmpThietLapKichBan;

                    List<thiet_lap_kich_ban_detail_tb> tmpLapKichBanDetail =
                        _database._databaseContext.thiet_lap_kich_ban_detail_tb.Where(
                            x =>
                                x.deleted_status == 0 &&
                                x.thiet_lap_kich_ban_ref == tmpThietLapKichBan.thiet_lap_kich_ban_code).ToList();
                    result.KichBanChiTiet = tmpLapKichBanDetail;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public List<thiet_lap_kich_ban_tb> GetListKichBanByUserTLVN(int userCodeTLVN)
        {
            List<thiet_lap_kich_ban_tb> result = new List<thiet_lap_kich_ban_tb>();
            try
            {
                int id = Convert.ToInt32(Security.CurrentUserId);
                var tmpdata = _database._databaseContext.thiet_lap_kich_ban_tb.Where(
                    x => x.deleted_status == 0 && x.user_ref == id).ToList();
                if (tmpdata != null)
                {
                    result = tmpdata.ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
