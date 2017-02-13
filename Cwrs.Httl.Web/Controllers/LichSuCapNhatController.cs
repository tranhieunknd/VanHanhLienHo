using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cwrs.DataBase;
using Cwrs.DataBase.Models;
using Kendo.Mvc.UI;

namespace Cwrs.Httl.Web.Controllers
{
    public class LichSuCapNhatController : BaseController
    {
        //
        // GET: /LichSuCapNhat/
        public ActionResult Index()
        {
            List<LichSuCapNhat> lscn = new List<LichSuCapNhat>();
            if (!String.IsNullOrEmpty(Lib.Security.CurrentUserGuid))
            {
                if (_common.CheckAdmin())
                {
                    lscn = _lichsucapnhat.SelectAllTongCuc().ToList();
                    lscn = RowSpanDVTuVan(lscn);               // tìm số rowspan đơn vị cho table bên view
                    ViewBag.PartialName = "_LichSuCapNhatTongCuc";
                }
                else
                {
                    lscn = _lichsucapnhat.SelectAll(Lib.Security.CurrentUserGuid).ToList();
                    lscn = TimSoRowSpan(lscn);                    // tìm số rowspan tư vấn cho table bên view

                    ViewBag.PartialName = "_LichSuCapNhatTuVan";
                }
                return View(lscn);
            }
            else
            {
                lscn = null;
                ViewBag.ThongBao = "Bạn Không Có Quyền Xem Trang Này !";
                return View("LichSuCapNhatTuVan", lscn);
            }
        }
        #region OldCode
        //public ActionResult Index()
        //{
        //    List<LichSuCapNhat> lscn = new List<LichSuCapNhat>();
        //    if (!String.IsNullOrEmpty(Lib.Security.CurrentUserGuid))   // chỗ này sau a hiếu truyền mã user_guid vào, gọi đến controller bên kia để check trước khi lấy dữ liệu
        //    {
        //        lscn = _lichsucapnhat.SelectAll(Lib.Security.CurrentUserGuid).ToList();
        //    }
        //    else
        //    {
        //        lscn = null;
        //        ViewBag.ThongBao = "Bạn Không Có Quyền Xem Trang Này !";
        //    }
        //    return View(lscn);
        //}
        #endregion

        public List<LichSuCapNhat> RowSpanDVTuVan(List<LichSuCapNhat> lscn)           // tìm số rowspan cho cột đơn vị tư vấn của table bên trang view
        {
            #region old Code
            //for (int i = 0; i < lscn.Count; i++)
            //{
            //    lscn[i].rowDonVi = 1;
            //    for (int j = i + 1; j < lscn.Count; j++)        
            //    {
            //        if (lscn[i].don_vi_code == lscn[j].don_vi_code)          //trùng mã thì vào đếm số row hàng
            //        {
            //            lscn[i].rowDonVi++;
            //            lscn[j].rowDonVi = 1;
            //            if(j==lscn.Count-1)
            //            {
            //                i = j;
            //            }
            //        }
            //        else
            //        {
            //            lscn[j].rowDonVi = 1;
            //            i += lscn[i].rowDonVi.Value - 1; 
            //            j = lscn.Count;
            //        }
            //    }
            //}
            #endregion

            int tmpThoiGian = 0;
            int tmpDonVi = 0;
            int tmpTuVan = 0;

            LichSuCapNhat tmpData;
            if (lscn.Count > 1)
            {
                tmpData = lscn[0];
                for (int i = 1; i < lscn.Count; i++)
                {
                    lscn[i].rowThoiGian = 0;
                    lscn[i].rowDonVi = 0;
                    lscn[i].rowTuVan = 0;

                    if (lscn[i].thoi_gian != tmpData.thoi_gian)
                    {
                        lscn[tmpThoiGian].rowThoiGian = i - tmpThoiGian;
                        lscn[tmpDonVi].rowDonVi = i - tmpDonVi;
                        lscn[tmpTuVan].rowTuVan = i - tmpTuVan;
                        tmpData = lscn[i];
                        tmpThoiGian = i;
                        tmpDonVi = i;
                        tmpTuVan = i;
                    }
                    else
                    {
                        if (lscn[i].don_vi_code != tmpData.don_vi_code)
                        {
                            lscn[tmpDonVi].rowDonVi = i - tmpDonVi;
                            lscn[tmpTuVan].rowTuVan = i - tmpTuVan;
                            tmpData = lscn[i];
                            tmpDonVi = i;
                            tmpTuVan = i;
                        }
                        else
                        {
                            if (lscn[i].tu_van_ref != tmpData.tu_van_ref)
                            {
                                lscn[tmpTuVan].rowTuVan = i - tmpTuVan;
                                tmpData = lscn[i];
                                tmpTuVan = i;
                            }
                        }
                    }
                }
                lscn[tmpThoiGian].rowThoiGian = lscn.Count - tmpThoiGian;
                lscn[tmpDonVi].rowDonVi = lscn.Count - tmpDonVi;
                lscn[tmpTuVan].rowTuVan = lscn.Count - tmpTuVan;
            }
            return lscn;
        }
        public List<LichSuCapNhat> TimSoRowSpan(List<LichSuCapNhat> lscn)
        // tìm s? rowspan cho c?t t? v?n c?a table bên trang view
        {
            int tmpThoiGian = 0;
            int tmpTuVan = 0;

            LichSuCapNhat tmpData;
            if (lscn.Count > 0)
            {
                tmpData = lscn[0];
                for (int i = 1; i < lscn.Count; i++)
                {
                    lscn[i].rowThoiGian = 0;
                    lscn[i].rowTuVan = 0;

                    if (lscn[i].thoi_gian != tmpData.thoi_gian)
                    {
                        lscn[tmpThoiGian].rowThoiGian = i - tmpThoiGian;
                        lscn[tmpTuVan].rowTuVan = i - tmpTuVan;
                        tmpData = lscn[i];
                        tmpThoiGian = i;
                        tmpTuVan = i;
                    }
                    else
                    {
                        if (lscn[i].tu_van_ref != tmpData.tu_van_ref)
                        {
                            lscn[tmpTuVan].rowTuVan = i - tmpTuVan;
                            tmpData = lscn[i];
                            tmpTuVan = i;
                        }
                    }
                }
                lscn[tmpThoiGian].rowThoiGian = lscn.Count - tmpThoiGian;
                lscn[tmpTuVan].rowTuVan = lscn.Count - tmpTuVan;
            }
            return lscn;
        }
        public ActionResult LichSuCapNhatTongCuc()
        {
            List<LichSuCapNhat> lscn = new List<LichSuCapNhat>();
            if (_common.CheckAdmin() == true)
            {
                lscn = _lichsucapnhat.SelectAllTongCuc().ToList();
            }
            else
            {
                lscn = null;
                ViewBag.ThongBao = "Bạn Không Có Quyền Xem Trang Này !";
            }
            return View(lscn);
        }
    }
}