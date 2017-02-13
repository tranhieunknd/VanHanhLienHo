using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cwrs.DataBase.Models;
using Cwrs.DataBase.Code;
using System.Configuration;
using Cwrs.DataBase.VanHanhLienHo;

namespace Cwrs.Httl.Web.Models
{
    public class LakeVitri
    {
        public string LakeCode { get; set; }
        public int ViTri { get; set; }
    }
    public class HandlEstablishSpillScenarios
    {
        Common _common = new Common();

        private double? TinhDungTichDuBao(double wHienTai, double tongQDen, double tongQXa,
            double totalTimeSecond)
        {
            double? result = null;
            try
            {
                result = (wHienTai * Math.Pow(10, 6) + (tongQDen - tongQXa) * totalTimeSecond) / Math.Pow(10, 6);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public KichBanXa TinhToanXa(KichBanXa kichbanxa)
        {
            KichBanXa result = null;
            if (kichbanxa != null & kichbanxa.KichBanChiTiet.Count > 0)
                try
                {
                    thiet_lap_kich_ban_tb kichBan = kichbanxa.KichBan;
                    var kichBanChiTiet = kichbanxa.KichBanChiTiet;

                    // Tính toán cho bản ghi đầu
                    double hHienTai = _common.TimDungTich(kichBan.vi_tri_ref, kichBan.muc_nuoc_ho);
                    TimeSpan tmpTimeSpan = kichBanChiTiet[0].thoi_gian.Value - kichBan.thoi_gian.Value;

                    double totalSecond = tmpTimeSpan.TotalSeconds;
                    double totalQden = kichBanChiTiet[0].luu_luong_du_bao;
                    double totalQXa = kichBanChiTiet[0].tong_tran + kichBanChiTiet[0].luu_luong_phat_dien +
                                      kichBanChiTiet[0].luu_luong_xa;

                    double? tmpDungTich = TinhDungTichDuBao(hHienTai, totalQden, totalQXa, totalSecond);

                    //kichBanChiTiet[0].dung_tich_ho = Math.Round(tmpDungTich == null ? 0 : tmpDungTich.Value,3);
                    kichBanChiTiet[0].dung_tich_ho = tmpDungTich == null ? 0 : Math.Round(tmpDungTich.Value,3);
                    kichBanChiTiet[0].muc_nuoc_ho = Math.Round(_common.TimMucNuoc(kichBan.vi_tri_ref,
                        kichBanChiTiet[0].dung_tich_ho), 3);

                    // Tính cho các bản ghi tiếp theo
                    for (int i = 1; i < kichBanChiTiet.Count; i++)
                    {
                        thiet_lap_kich_ban_detail_tb kb1 = kichBanChiTiet[i - 1];
                        thiet_lap_kich_ban_detail_tb kb2 = kichBanChiTiet[i];

                        tmpTimeSpan = kb2.thoi_gian.Value - kb1.thoi_gian.Value;

                        totalSecond = tmpTimeSpan.TotalSeconds;
                        totalQden = kb2.luu_luong_du_bao;
                        totalQXa = kb2.tong_tran + kb2.luu_luong_phat_dien + kb2.luu_luong_xa;

                        tmpDungTich = TinhDungTichDuBao(hHienTai, totalQden, totalQXa, totalSecond);

                        //kb2.dung_tich_ho = Math.Round(tmpDungTich == null ? 0 : tmpDungTich.Value, 3);
                        kb2.dung_tich_ho = tmpDungTich == null ? 0 : Math.Round(tmpDungTich.Value, 3);
                        kb2.muc_nuoc_ho = Math.Round(_common.TimMucNuoc(kichBan.vi_tri_ref, kb2.dung_tich_ho),3);
                    }
                    result = kichbanxa;
                }
                catch (Exception ex)
                {
                }
            return result;
        }
    }
}