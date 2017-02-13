using Cwrs.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cwrs.DataBase.VanHanhLienHo;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using Cwrs.DataBase;
using Cwrs.Httl.Web.Models;
using Cwrs.Lib;

namespace Cwrs.Httl.Web.Controllers
{
    public class ThietLapXaController : BaseController
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        HandlEstablishSpillScenarios _handl = new HandlEstablishSpillScenarios();

        // GET: ThietLapXa
        public ActionResult Index()
        {
            KichBanXa kbx = new KichBanXa();
            DateTime tmp = DateTime.Today;
            tmp = tmp.AddHours(DateTime.Now.Hour + 1);

            ViewBag.MucNuoc = _common.GetDataLakeEVN(1, 1);
            ViewBag.LuuLuong = _common.GetDataLakeEVN(1, 5);
            var tmpListThietLap = _thietlapxa.GetListKichBanByUserTLVN(Convert.ToInt32(Security.CurrentUserId));
            tmpListThietLap.Insert(0, new thiet_lap_kich_ban_tb
            {
                thiet_lap_kich_ban_code = -1,
                thiet_lap_kich_ban_name = "--Không chọn--"
            });

            ViewBag.ListThietLap = new SelectList(tmpListThietLap, "thiet_lap_kich_ban_code", "thiet_lap_kich_ban_name", -1);
            kbx.KichBanChiTiet = new List<thiet_lap_kich_ban_detail_tb>()
            {
                new thiet_lap_kich_ban_detail_tb() {thoi_gian = tmp,luu_luong_du_bao = 2500, luu_luong_xa = 500,luu_luong_phat_dien = 100, tong_tran = 100},
                new thiet_lap_kich_ban_detail_tb() {thoi_gian = tmp.AddHours(1),luu_luong_du_bao = 2500, luu_luong_xa = 500,luu_luong_phat_dien = 100, tong_tran = 100},
                new thiet_lap_kich_ban_detail_tb() {thoi_gian = tmp.AddHours(2),luu_luong_du_bao = 2500, luu_luong_xa = 500,luu_luong_phat_dien = 100, tong_tran = 100},
                new thiet_lap_kich_ban_detail_tb() {thoi_gian = tmp.AddHours(3),luu_luong_du_bao = 2500, luu_luong_xa = 500,luu_luong_phat_dien = 100, tong_tran = 100},
                new thiet_lap_kich_ban_detail_tb() {thoi_gian = tmp.AddHours(4),luu_luong_du_bao = 2500, luu_luong_xa = 500,luu_luong_phat_dien = 100, tong_tran = 100}
            };
            return View(kbx);
        }

        public ActionResult ChangeGridRead([DataSourceRequest] DataSourceRequest request, string jsonData, DateTime dateSelect, int rowNumber)
        {
            List<thiet_lap_kich_ban_detail_tb> result = new List<thiet_lap_kich_ban_detail_tb>();
            List<thiet_lap_kich_ban_detail_tb> listKichBanChiTiet = serializer.Deserialize<List<thiet_lap_kich_ban_detail_tb>>(jsonData);
            listKichBanChiTiet = listKichBanChiTiet.Where(x => x.thoi_gian > dateSelect).OrderBy(x => x.thoi_gian).ToList();


            int tmpCountKb = listKichBanChiTiet.Count;
            int indexLoad = tmpCountKb > 0 ? 0 : -1;
            DateTime date = dateSelect.Date;
            date = date.AddHours(dateSelect.Hour + 1);
            for (int i = 0; i < rowNumber; i++)
            {
                if (tmpCountKb > 0 && indexLoad < tmpCountKb)
                {
                    if (date == listKichBanChiTiet[indexLoad].thoi_gian.Value)
                    {
                        result.Add(listKichBanChiTiet[indexLoad]);
                        indexLoad++;
                    }
                    else
                    {
                        result.Add(new thiet_lap_kich_ban_detail_tb() { thoi_gian = date });
                    }
                }
                else
                {
                    result.Add(new thiet_lap_kich_ban_detail_tb() { thoi_gian = date });
                }
                date = date.AddHours(1);
            }

            DataSourceResult xtmp = new DataSourceResult()
            {
                Data = result,
                Total = listKichBanChiTiet.Count
            };

            return Json(xtmp);
        }

        public ActionResult TaoKichBanXa(int vitricode, double mucnuoc, double luuluong, DateTime time, string jsondata)
        {
            List<thiet_lap_kich_ban_detail_tb> listKichBanChiTiet = serializer.Deserialize<List<thiet_lap_kich_ban_detail_tb>>(jsondata);
            listKichBanChiTiet = listKichBanChiTiet.OrderBy(x => x.thoi_gian).ToList();

            KichBanXa kichBanXa = new KichBanXa()
            {
                KichBan = new thiet_lap_kich_ban_tb
                {
                    thoi_gian = time,
                    vi_tri_ref = vitricode,
                    muc_nuoc_ho = mucnuoc,
                    luu_luong_ve_ho = luuluong
                },
                KichBanChiTiet = listKichBanChiTiet
            };

            kichBanXa = _handl.TinhToanXa(kichBanXa);

            return Json(kichBanXa.KichBanChiTiet);

        }

        public ActionResult LuuKichBanXa(int kichbancode, int vitricode, double mucnuoc, double luuluong, DateTime time,
            string jsondata)
        {

            List<thiet_lap_kich_ban_detail_tb> listKichBanChiTiet = serializer.Deserialize<List<thiet_lap_kich_ban_detail_tb>>(jsondata);
            listKichBanChiTiet = listKichBanChiTiet.OrderBy(x => x.thoi_gian).ToList();

            KichBanXa kichBanXa = new KichBanXa()
            {
                KichBan = new thiet_lap_kich_ban_tb
                {
                    thiet_lap_kich_ban_code = kichbancode,
                    thoi_gian = time,
                    vi_tri_ref = vitricode,
                    muc_nuoc_ho = mucnuoc,
                    luu_luong_ve_ho = luuluong
                },
                KichBanChiTiet = listKichBanChiTiet
            };

            bool result = _thietlapxa.UpdateKichBan(kichBanXa);
            if (result)
            {
                return Json(new
                {
                    listKichBan = _thietlapxa.GetListKichBanByUserTLVN(Convert.ToInt32(Security.CurrentUserId)),
                    codeKichBan = kichbancode
                });

            }
            return Json(result);
        }

        public ActionResult GetValueNowForLake(int vitricode, int datatype, double data)
        {
            double result = -1;
            try
            {
                result = _common.GetDataLakeEVN(vitricode, datatype);
            }
            catch (Exception ex)
            {
                // ignored
            }
            return Json(result);
        }

        public JsonResult GetInfoReadlTimeLake(int vitri)
        {
            return Json(new
            {
                MucNuoc = _common.GetDataLakeEVN(vitri, 1),
                LuuLuong = _common.GetDataLakeEVN(vitri, 5)
            });
        }

        public JsonResult GetInfoKichBan(int kichbancode)
        {
            KichBanXa tmp = _thietlapxa.GetInfoByKichBanCode(kichbancode);
            return Json(tmp);
        }
    }
}