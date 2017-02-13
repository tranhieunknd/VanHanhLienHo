using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;
using Cwrs.Lib;

namespace Cwrs.DataBase.Code
{
    public class TuVanCtrl : Common
    {
        /// <summary>
        /// HieuTM: Hàm cập nhật thông tin của 1 file tư vấn.
        /// </summary>
        /// <param name="bttv"></param>
        /// <returns></returns>
        //public bool Insert(BanTinTuVan bttv, int donviref)
        //{
        //    bool result = false;
        //    using (var dbContextTransaction = _database._databaseContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            #region Thêm bản ghi cho bảng tư vấn.
        //            tu_van_tb tuvantmp = new tu_van_tb
        //            {
        //                created_at = DateTime.Now,
        //                created_by = Security.CurrentUserId,
        //                deleted_status = 0,
        //                don_vi_ref = donviref, // Lấy theo thông tin tài khoản. ==))
        //                du_bao_24h = bttv.DuKien,
        //                last_modified_at = DateTime.Now,
        //                last_modified_by = Security.CurrentUserId,
        //                nhan_xet_24h = bttv.NhanXetTinhHinh,
        //                thoi_gian = bttv.ThoiGian,
        //                user_ref = Convert.ToInt32(Security.CurrentUserId),
        //                row_id = Guid.NewGuid().ToString()
        //            };
        //            _database._databaseContext.tu_van_tb.Add(tuvantmp);
        //            _database._databaseContext.SaveChanges();
        //            #endregion
        //            #region Thêm bảng ghi cho bảng diễn biến.
        //            foreach (dien_bien_tb item in bttv.DienBienExcel)
        //            {
        //                item.row_id = Guid.NewGuid().ToString();
        //                item.tu_van_ref = tuvantmp.tu_van_code;
        //                item.created_at = DateTime.Now;
        //                item.created_by = Security.CurrentUserId;
        //                item.last_modified_at = DateTime.Now;
        //                item.last_modified_by = Security.CurrentUserId;
        //                item.deleted_status = 0;

        //                _database._databaseContext.dien_bien_tb.Add(item);
        //            }
        //            _database._databaseContext.SaveChanges();
        //            #endregion
        //            #region Thêm bản ghi cho bảng hiện trạng.
        //            foreach (hien_trang_tb item in bttv.HienTrangExcel)
        //            {
        //                item.row_id = Guid.NewGuid().ToString();
        //                item.created_at = DateTime.Now;
        //                item.created_by = Security.CurrentUserId;
        //                item.last_modified_at = DateTime.Now;
        //                item.last_modified_by = Security.CurrentUserId;
        //                item.deleted_status = 0;

        //                _database._databaseContext.hien_trang_tb.Add(item);
        //            }
        //            _database._databaseContext.SaveChanges();
        //            #endregion
        //            foreach (PhuongAnExcel itempa in bttv.PhuongAnExcel)
        //            {
        //                #region Thêm bản ghi vào bảng phương án.
        //                phuong_an_tb phuongantmp = new phuong_an_tb
        //                {
        //                    row_id = Guid.NewGuid().ToString(),
        //                    created_at = DateTime.Now,
        //                    created_by = Security.CurrentUserId,
        //                    deleted_status = 0,
        //                    last_modified_at = DateTime.Now,
        //                    last_modified_by = Security.CurrentUserId,
        //                    kien_nghi = itempa.KienNghiPA,
        //                    phan_tich = itempa.PhanTichNhanXet,
        //                    tu_van_ref = tuvantmp.tu_van_code
        //                };
        //                _database._databaseContext.phuong_an_tb.Add(phuongantmp);
        //                //_database.phuong_an_tbs.Insert(phuongantmp, false);
        //                _database._databaseContext.SaveChanges();
        //                #endregion
        //                foreach (phuong_an_detail_tb itempad in itempa.PhuongAn_Detail)
        //                {
        //                    #region Thêm bản ghi vào bảng phương án chi tiết
        //                    itempad.row_id = Guid.NewGuid().ToString();
        //                    itempad.created_at = DateTime.Now;
        //                    itempad.created_by = Security.CurrentUserId;
        //                    itempad.last_modified_at = DateTime.Now;
        //                    itempad.last_modified_by = Security.CurrentUserId;
        //                    itempad.deleted_status = 0;
        //                    itempad.phuong_an_ref = phuongantmp.phuong_an_code;
        //                    _database._databaseContext.phuong_an_detail_tb.Add(itempad);
        //                    #endregion
        //                }
        //                _database._databaseContext.SaveChanges();
        //            }
        //            dbContextTransaction.Commit();
        //            result = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            dbContextTransaction.Rollback();
        //        }
        //    }
        //    return result;
        //}
        public bool Insert(BanTinTuVan bttv, int donviref)
        {
            bool result = false;
            if(!String.IsNullOrEmpty(Security.CurrentUserGuid))
            using (var dbContextTransaction = _database._databaseContext.Database.BeginTransaction())
            {
                try
                {
                    #region Thêm bản ghi cho bảng tư vấn.
                    tu_van_tb tuvantmp = new tu_van_tb
                    {
                        created_at = DateTime.Now,
                        created_by = Security.CurrentUserId,
                        deleted_status = 0,
                        don_vi_ref = donviref, // Lấy theo thông tin tài khoản. ==))
                        du_bao_24h = bttv.DuKien,
                        last_modified_at = DateTime.Now,
                        last_modified_by = Security.CurrentUserId,
                        nhan_xet_24h = bttv.NhanXetTinhHinh,
                        thoi_gian = bttv.ThoiGian,
                        link = bttv.Link,
                        user_ref = _database._databaseContext.user_tb.FirstOrDefault(x => x.user_guid == Security.CurrentUserGuid).user_code,// Convert.ToInt32(Security.CurrentUserId),
                        row_id = Guid.NewGuid().ToString()
                    };
                    _database._databaseContext.tu_van_tb.Add(tuvantmp);
                    _database._databaseContext.SaveChanges();
                    #endregion
                    #region Thêm bảng ghi cho bảng diễn biến.
                    dien_bien_tb item;
                    foreach (DienBien_Models item1 in bttv.DienBienExcel)
                    {
                        item = new dien_bien_tb();
                        item.row_id = Guid.NewGuid().ToString();
                        item.vi_tri_ref = item1.vi_tri_ref;
                        item.loai_du_lieu_ref = item1.loai_du_lieu_ref;
                        if (string.IsNullOrEmpty(item1.gia_tri))
                            item.gia_tri = -1;
                        else
                            item.gia_tri = double.Parse(item1.gia_tri);
                        item.is_thuc_do = item1.is_thuc_do;
                        item.tu_van_ref = tuvantmp.tu_van_code;
                        item.created_at = DateTime.Now;
                        item.created_by = Security.CurrentUserId;
                        item.last_modified_at = DateTime.Now;
                        item.last_modified_by = Security.CurrentUserId;
                        item.deleted_status = 0;

                        _database._databaseContext.dien_bien_tb.Add(item);
                    }
                    _database._databaseContext.SaveChanges();
                    #endregion
                    #region Thêm bản ghi cho bảng hiện trạng.
                    hien_trang_tb hientrang;
                    foreach (Hientrang_Model item1 in bttv.HienTrangExcel)
                    {
                        hientrang = new hien_trang_tb();
                        hientrang.vi_tri_ref = item1.vi_tri_ref;
                        hientrang.loai_du_lieu_ref = item1.loai_du_lieu_ref;
                        if (string.IsNullOrEmpty(item1.gia_tri))
                            hientrang.gia_tri = -1;
                        else
                            hientrang.gia_tri = double.Parse(item1.gia_tri);
                        hientrang.row_id = Guid.NewGuid().ToString();
                        hientrang.created_at = DateTime.Now;
                        hientrang.created_by = Security.CurrentUserId;
                        hientrang.last_modified_at = DateTime.Now;
                        hientrang.last_modified_by = Security.CurrentUserId;
                        hientrang.deleted_status = 0;
                        hientrang.tu_van_ref = tuvantmp.tu_van_code;

                        _database._databaseContext.hien_trang_tb.Add(hientrang);
                    }
                    _database._databaseContext.SaveChanges();
                    #endregion
                    foreach (PhuongAnExcel itempa in bttv.PhuongAnExcel)
                    {
                        #region Thêm bản ghi vào bảng phương án.
                        phuong_an_tb phuongantmp = new phuong_an_tb
                        {
                            row_id = Guid.NewGuid().ToString(),
                            created_at = DateTime.Now,
                            created_by = Security.CurrentUserId,
                            deleted_status = 0,
                            last_modified_at = DateTime.Now,
                            last_modified_by = Security.CurrentUserId,
                            kien_nghi = itempa.KienNghiPA,
                            phan_tich = itempa.PhanTichNhanXet,
                            tu_van_ref = tuvantmp.tu_van_code
                        };
                        _database._databaseContext.phuong_an_tb.Add(phuongantmp);
                        //_database.phuong_an_tbs.Insert(phuongantmp, false);
                        _database._databaseContext.SaveChanges();
                        #endregion
                        phuong_an_detail_tb phuongan;
                        foreach (PhuongAnDetail_Model itempad in itempa.PhuongAn_Detail)
                        {
                            phuongan = new phuong_an_detail_tb();
                            #region Thêm bản ghi vào bảng phương án chi tiết
                            phuongan.row_id = Guid.NewGuid().ToString();
                            phuongan.vi_tri_ref = itempad.vi_tri_ref;
                            phuongan.loai_du_lieu_ref = itempad.loai_du_lieu_ref;
                            phuongan.thoi_gian = itempad.thoi_gian;
                            if (string.IsNullOrEmpty(itempad.gia_tri))
                                phuongan.gia_tri = -1;
                            else
                                phuongan.gia_tri = double.Parse(itempad.gia_tri);
                            phuongan.created_at = DateTime.Now;
                            phuongan.created_by = Security.CurrentUserId;
                            phuongan.last_modified_at = DateTime.Now;
                            phuongan.last_modified_by = Security.CurrentUserId;
                            phuongan.deleted_status = 0;
                            phuongan.phuong_an_ref = phuongantmp.phuong_an_code;
                            _database._databaseContext.phuong_an_detail_tb.Add(phuongan);
                            #endregion
                        }
                        _database._databaseContext.SaveChanges();
                    }
                    dbContextTransaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return result;
        }
        
        /// <summary>
        /// ThamNT
        /// </summary>
        /// <param name="usergui">Mã grui tương ứng với user đăng nhập vào hệ thống</param>
        /// <returns>mã đơn vị lập bản tin tư vấn</returns>
        public int test_donvi_ref( string usergui)
        {
            user_tb donvi = _database._databaseContext.user_tb.Where(m => m.user_guid == usergui).SingleOrDefault();
            if (donvi == null)
            {
                return -1;
            }
            return int.Parse(donvi.don_vi_ref.ToString());
        }

        /// <summary>
        /// HieuTM: Lấy thông tin đầy đủ của một bản tin tư vấn với tuvanGuid
        /// </summary>
        /// <param name="tuvanGuid"></param>
        /// <returns></returns>
        public BanTinTuVan GetInfoTuVanByGuid(string tuvanGuid)
        {
            BanTinTuVan result = new BanTinTuVan();
            try
            {
                // Lay thong tin chung cua ban tin tu van
                var tuvanTmp = _database._databaseContext.tu_van_tb.FirstOrDefault(x => x.row_id == tuvanGuid && x.deleted_status == 0);
                if (tuvanTmp != null)
                {
                    // Nếu có giá trị tồn tại thì gán giá trị
                    result.DuKien = tuvanTmp.du_bao_24h;
                    result.NhanXetTinhHinh = tuvanTmp.nhan_xet_24h;
                    result.ThoiGian = tuvanTmp.thoi_gian.Value;

                    // Lấy thông tin của Diễn biến, hiện trạng, phương án
                    List<dien_bien_tb> tmpDienBien =
                        _database._databaseContext.dien_bien_tb.Where(
                            x => x.tu_van_ref == tuvanTmp.tu_van_code && x.deleted_status == 0).ToList();
                    List<hien_trang_tb> tmpHienTrang =
                        _database._databaseContext.hien_trang_tb.Where(
                            x => x.tu_van_ref == tuvanTmp.tu_van_code && x.deleted_status == 0).ToList();
                    List<phuong_an_tb> tmpPhuongAn =
                        _database._databaseContext.phuong_an_tb.Where(
                            x => x.tu_van_ref == tuvanTmp.tu_van_code && x.deleted_status == 0).ToList();

                    // Kiểm tra các giá trị cần tìm có tồn tại không và gán giá trị
                    if (tmpDienBien != null & tmpHienTrang != null & tmpPhuongAn != null)
                    {
                        result.DienBienExcel = tmpDienBien.Select(x => new DienBien_Models
                        {
                            thoi_gian = x.thoi_gian,
                            gia_tri = x.gia_tri.ToString(),
                            is_thuc_do = x.is_thuc_do,
                            loai_du_lieu_ref = x.loai_du_lieu_ref,
                            vi_tri_ref = x.vi_tri_ref
                        }).ToList();

                        result.HienTrangExcel = tmpHienTrang.Select(x => new Hientrang_Model
                        {
                            loai_du_lieu_ref = x.loai_du_lieu_ref,
                            vi_tri_ref = x.vi_tri_ref,
                            gia_tri = x.gia_tri.ToString()
                        }).ToList();

                        foreach (phuong_an_tb item in tmpPhuongAn)
                        {
                            List<phuong_an_detail_tb> tmpPhuongAnDetail =
                                _database._databaseContext.phuong_an_detail_tb.Where(
                                    x => x.deleted_status == 0 && x.phuong_an_ref == item.phuong_an_code).ToList();
                            if (tmpPhuongAnDetail != null & tmpPhuongAnDetail.Count() > 0)
                            {
                                PhuongAnExcel tmPhuongAnExcel = new PhuongAnExcel()
                                {
                                    KienNghiPA = item.kien_nghi,
                                    PhanTichNhanXet = item.phan_tich,
                                    PhuongAn_Detail = tmpPhuongAnDetail.Select(x => new PhuongAnDetail_Model
                                    {
                                        loai_du_lieu_ref = x.loai_du_lieu_ref,
                                        gia_tri =  x.gia_tri.ToString(),
                                        vi_tri_ref = x.vi_tri_ref,
                                        thoi_gian = x.thoi_gian
                                    }).ToList()
                                };
                                result.PhuongAnExcel.Add(tmPhuongAnExcel);
                            }
                        }
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
                result = null;
            }
            return result;
        }

        public tu_van_tb GetTuVaByGuid(string tuVanGuid)
        {
            tu_van_tb result = new tu_van_tb();
            try
            {
                result =
                    _database._databaseContext.tu_van_tb.FirstOrDefault(
                        x => x.deleted_status == 0 && x.row_id == tuVanGuid);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
    }
}
