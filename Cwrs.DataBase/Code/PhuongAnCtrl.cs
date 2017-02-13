using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;
using System.Web.Mvc;
using Npgsql;

namespace Cwrs.DataBase.Code
{
    public class Cls_MNHL
    {
        public int pA_number { set; get; }
        public string ten_Vi_Tri { set; get; }
        public double gia_Tri { set; get; }
        public DateTime? thoi_Gian_Tao { set; get; }
    }
    public class Cls_LuuLuong
    {
        public string tenDonVi { set; get; }
        public int maDonVi { set; get; }
        public double giaTri { set; get; }
        public DateTime? thoiGianTao { set; get; }
    }
    public class ViTri
    {
        public int viTriCode { set; get; }
        public string viTriName { set; get; }
    }
    public class PhuongAnCtrl : Common
    {

        public IEnumerable<PhuongAn> SelectAll()
        {

            string sql =
                @" select ki_hieu, don_vi_code, phuong_an_code, kien_nghi, loai_du_lieu_ref, gia_tri, thoi_gian, vi_tri_ref from(
                            (select pa.phuong_an_ref, pa.Gia_tri, pa.thoi_gian, pa.loai_du_lieu_ref, pa.vi_tri_ref from phuong_an_detail_tb pa 
                            order by thoi_gian desc)padt
                            inner join
                            (select * 
                            from(select  dv.don_vi_code,dv.don_vi_name as ki_hieu, dv.don_vi_name from don_vi_tb dv) dv
                            inner join
                            (select pa.phuong_an_code,pa.kien_nghi,TuVan_DonVi.tu_van_code,TuVan_DonVi.don_vi_ref 
                            from phuong_an_tb pa
                            inner join (select tv.tu_van_code, tv.don_vi_ref from tu_van_tb tv
                            where tv.thoi_gian=( (select max(thoi_gian)from tu_van_tb))) as TuVan_DonVi
                            on pa.tu_van_ref=TuVan_DonVi.tu_van_code) tb
                            on dv.don_vi_code=tb.don_vi_ref)tv_dv
                            on padt.phuong_an_ref=tv_dv.phuong_an_code) ";
            IEnumerable<PhuongAn> x = _database._databaseContext.Database.SqlQuery<PhuongAn>(sql).ToList();
            return x;
        }

        public PhuongAn SelectTongHop()
        {
            string sql = @"select tong_hop_code as TongHop_Code, phan_tich_nhan_xet as TongHop_phanTich, tong_hop_tinh_toan as TongHop_TongHop, kien_nghi_dieu_hanh as TongHop_KienNghi, thoi_gian as TongHop_ThoiGian, created_at as TongHop_CreateAt from tong_hop_tb where to_char(thoi_gian, 'DD:mm:YYYY') = to_char(CURRENT_DATE, 'DD:mm:YYYY')
                            order by tong_hop_code desc
                            limit 1";
            PhuongAn x = _database._databaseContext.Database.SqlQuery<PhuongAn>(sql).FirstOrDefault();
            return x;
        }
        public IEnumerable<PhuongAn> Select7H()
        {
            string sql = @"WITH tuvanlive AS (
                                     select * 
                                     from(
                                      select row_number() over (partition by don_vi_ref ORDER BY tu_van_tb.created_at desc)as rownum,* 
                                      from tu_van_tb 
                                      join don_vi_tb
                                      on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
                                      where thoi_gian = (select Max(thoi_gian) from tu_van_tb)
                                     ) abc
                                     where abc.rownum = 1
                                    )

                                    select x.ki_hieu, x.phuong_an_code, x.kien_nghi, pad.loai_du_lieu_ref, pad.gia_tri, pad.thoi_gian, pad.vi_tri_ref from
                                    (
                                     select tvl.don_vi_name, tvl.don_vi_code,
                                      row_number() over (partition by tvl.tu_van_code ORDER BY pa.created_at desc)as rownum, 
                                      pa.phuong_an_code,
                                      pa.kien_nghi,
                                      tvl.ki_hieu,
                                      tvl.tu_van_code
                                     from phuong_an_tb pa
                                     join tuvanlive tvl
                                     on pa.tu_van_ref = tvl.tu_van_code
                                     order by tvl.tu_van_code, pa.created_at desc
                                    )x
                                    join phuong_an_detail_tb pad
                                    on x.phuong_an_code = pad.phuong_an_ref
                                    where  EXTRACT(hour FROM pad.thoi_gian)in(7,19)  -- x.rownum = 1 and
					and EXTRACT(day FROM pad.thoi_gian) <> EXTRACT(day FROM (select Max(thoi_gian) from tu_van_tb))
					--EXTRACT(day FROM pad.thoi_gian) <> EXTRACT(day FROM pad.thoi_gian)
                                    --(pad.thoi_gian between '{0} 07:00' and '{3} 07:00')
                                    order by x.don_vi_code, x.phuong_an_code, pad.thoi_gian";
            #region Old Code
            //@"  select ki_hieu, don_vi_code, phuong_an_code, kien_nghi, loai_du_lieu_ref, gia_tri, thoi_gian, vi_tri_ref from 
            //            ((select A.* 
            //            from(select pa.phuong_an_ref, pa.Gia_tri, pa.thoi_gian, pa.loai_du_lieu_ref, pa.vi_tri_ref from phuong_an_detail_tb pa
            //                where ((SELECT EXTRACT(hour FROM thoi_gian)=7 
            //                or (SELECT EXTRACT(hour FROM thoi_gian)=19)))) A,
            //                (select phuong_an_ref,min(thoi_gian) as thoi_gian from phuong_an_detail_tb
            //                where ((SELECT EXTRACT(hour FROM thoi_gian)=7 
            //                or (SELECT EXTRACT(hour FROM thoi_gian)=19)))
            //                group by phuong_an_ref) B
            //                where A.phuong_an_ref = B.phuong_an_ref and A.thoi_gian <> B.thoi_gian ) padt
            //            inner join 
            //            (select * 
            //            from((select  dv.don_vi_code,dv.don_vi_name as ki_hieu, dv.don_vi_name from don_vi_tb dv) dv
            //                inner join
            //                (select pa.phuong_an_code,pa.kien_nghi,TuVan_DonVi.tu_van_code,TuVan_DonVi.don_vi_ref 
            //                from phuong_an_tb pa
            //                inner join (select tv.tu_van_code, tv.don_vi_ref from tu_van_tb tv
            //                where tv.thoi_gian=( (select max(thoi_gian)from tu_van_tb))) as TuVan_DonVi
            //                on pa.tu_van_ref=TuVan_DonVi.tu_van_code) tb
            //                on dv.don_vi_code=tb.don_vi_ref)
            //            )tv_dv
            //            on padt.phuong_an_ref=tv_dv.phuong_an_code) ";
            #endregion

            IEnumerable<PhuongAn> x = _database._databaseContext.Database.SqlQuery<PhuongAn>(sql).ToList();
            return x;
        }

        #region Mực nước (cũ)
        //public IEnumerable<MucNuoc_SoSanh> To_MucNuoc(List<PhuongAn> list, int vi_tri_code)
        //{
        //    List<MucNuoc_SoSanh> lstMN = new List<MucNuoc_SoSanh>();
        //    list = list.Where(x => (x.loai_du_lieu_ref == 2 && x.vi_tri_ref == vi_tri_code)).OrderByDescending(x => x.thoi_gian).ToList();
        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        MucNuoc_SoSanh mn = TimKiem_MucNuoc(list, list[i].thoi_gian, list[i].vi_tri_ref);
        //        if (!(lstMN.Exists(MucNuoc => MucNuoc.thoi_gian == mn.thoi_gian)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_DHTL == mn.mucnuoc_DHTL)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_KHTL == mn.mucnuoc_KHTL)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_KTTV == mn.mucnuoc_KTTV)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_KTTVTW == mn.mucnuoc_KTTVTW)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_VCH == mn.mucnuoc_VCH)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_VQHTL == mn.mucnuoc_VQHTL)))
        //            lstMN.Add(mn);
        //    }
        //    return lstMN;
        //}
        //public IEnumerable<MucNuoc_SoSanh> To_MucNuoc(List<PhuongAn> list)
        //{
        //    List<MucNuoc_SoSanh> lstMN = new List<MucNuoc_SoSanh>();
        //    list = list.Where(x => x.loai_du_lieu_ref == 2).OrderByDescending(x => x.thoi_gian).ToList();
        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        MucNuoc_SoSanh mn = TimKiem_MucNuoc(list, list[i].thoi_gian, list[i].vi_tri_ref);
        //        if (!(lstMN.Exists(MucNuoc => MucNuoc.thoi_gian == mn.thoi_gian)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_DHTL == mn.mucnuoc_DHTL)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_KHTL == mn.mucnuoc_KHTL)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_KTTV == mn.mucnuoc_KTTV)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_KTTVTW == mn.mucnuoc_KTTVTW)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_VCH == mn.mucnuoc_VCH)
        //            && lstMN.Exists(MucNuoc => MucNuoc.mucnuoc_VQHTL == mn.mucnuoc_VQHTL)))
        //            lstMN.Add(mn);
        //    }
        //    return lstMN;
        //}
        //public MucNuoc_SoSanh TimKiem_MucNuoc(List<PhuongAn> list, DateTime ThoiGian, int ViTri)
        //{
        //    MucNuoc_SoSanh mn = new MucNuoc_SoSanh();
        //    foreach (PhuongAn item in list)
        //    {
        //        if (item.thoi_gian == ThoiGian && item.vi_tri_ref == ViTri)
        //        {
        //            mn.thoi_gian = ThoiGian;
        //            mn.vi_tri_code = ViTri;
        //            switch (item.don_vi_code)
        //            {
        //                case 1: if (mn.mucnuoc_KTTVTW == null) mn.mucnuoc_KTTVTW = item.gia_tri; break;
        //                case 2: if (mn.mucnuoc_VQHTL == null) mn.mucnuoc_VQHTL = item.gia_tri; break;
        //                case 3: if (mn.mucnuoc_KHTL == null) mn.mucnuoc_KHTL = item.gia_tri; break;
        //                case 4: if (mn.mucnuoc_DHTL == null) mn.mucnuoc_DHTL = item.gia_tri; break;
        //                case 5: if (mn.mucnuoc_KTTV == null) mn.mucnuoc_KTTV = item.gia_tri; break;
        //                case 6: if (mn.mucnuoc_VCH == null) mn.mucnuoc_VCH = item.gia_tri; break;
        //                default: break;
        //            }
        //        }
        //    }
        //    return mn;
        //}
        #endregion

        #region Mực nước
        public List<MucNuoc_giatri> GetAllMucNuocByThoiGian(int viTri, DateTime fromDate, int hours)
        {
            List<MucNuoc_giatri> lst = new List<MucNuoc_giatri>();
            DateTime begin = fromDate;
            if (fromDate.Hour == 0) begin = fromDate.AddHours(7);
            string sql = @" WITH tuvanlive AS (
	                    select * 
	                    from(
	                    select row_number() over (partition by thoi_gian, don_vi_ref  ORDER BY tu_van_tb.created_at desc)as rownum,* 
	                    from tu_van_tb 
	                    join don_vi_tb
	                    on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
                        where (thoi_gian =' " + fromDate.ToString("yyyy-MM-dd HH:mm") + " ')) abc where abc.rownum = 1 ) ";
            string select = @" select thoi_gian,phuong_an_code,tu_van_code,don_vi_code,vi_tri_ref,gia_tri from(
                            (select phuong_an_ref, vi_tri_ref, loai_du_lieu_ref, gia_tri, thoi_gian from phuong_an_detail_tb
                                where loai_du_lieu_ref = 2 and vi_tri_ref in (1, 2, 3))padt 
                            join (select phuong_an_code, tu_van_code, don_vi_code from(
                                    (select * from phuong_an_tb )pa
                                    join tuvanlive tvl
                                    on pa.tu_van_ref = tvl.tu_van_code)A)pa_tv_dv
                                    on padt.phuong_an_ref = pa_tv_dv.phuong_an_code
	                        )x
                            where x.vi_tri_ref= " + viTri + " and (x.thoi_gian between ' " + begin.ToString("yyyy-MM-dd HH:mm") + " ' and ' " + begin.AddHours(hours).ToString("yyyy-MM-dd HH:mm") + " ') order by x.thoi_gian,x.phuong_an_code ";
            sql += select;

            lst = _database._databaseContext.Database.SqlQuery<MucNuoc_giatri>(sql).ToList();
            return lst;
        }
        public List<MucNuoc> To_MucNuoc(List<MucNuoc_giatri> list)
        {
            List<MucNuoc> lstMN = new List<MucNuoc>();

            for (int i = 0; i < list.Count(); i++)
            {
                MucNuoc mn = TimKiem_MucNuoc(list, list[i].thoi_gian, list[i].vi_tri_ref);
                if (!(lstMN.Exists(MucNuoc => MucNuoc.thoi_gian == mn.thoi_gian)
                    && lstMN.Exists(MucNuoc => SoSanhBangListDouble(MucNuoc.mucNuoc_DHTL, mn.mucNuoc_DHTL))
                    && lstMN.Exists(MucNuoc => SoSanhBangListDouble(MucNuoc.mucNuoc_KHTL, mn.mucNuoc_KHTL))
                    && lstMN.Exists(MucNuoc => SoSanhBangListDouble(MucNuoc.mucNuoc_KTTV, mn.mucNuoc_KTTV))
                    && lstMN.Exists(MucNuoc => SoSanhBangListDouble(MucNuoc.mucNuoc_KTTVTW, mn.mucNuoc_KTTVTW))
                    && lstMN.Exists(MucNuoc => SoSanhBangListDouble(MucNuoc.mucNuoc_VQHTL, mn.mucNuoc_VQHTL))
                    && lstMN.Exists(MucNuoc => SoSanhBangListDouble(MucNuoc.mucNuoc_VCH, mn.mucNuoc_VCH))))
                //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_DHTL == lldh.mucNuoc_DHTL)
                //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_KHTL == lldh.mucNuoc_KHTL)
                //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_KTTV == lldh.mucNuoc_KTTV)
                //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_KTTVTW == lldh.mucNuoc_KTTVTW)
                //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_VCH == lldh.mucNuoc_VCH)
                //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_VQHTL == lldh.mucNuoc_VQHTL)))
                {
                    int[] tmp = { mn.mucNuoc_KTTVTW.Count, mn.mucNuoc_VQHTL.Count, mn.mucNuoc_KHTL.Count, mn.mucNuoc_DHTL.Count, mn.mucNuoc_KTTV.Count, mn.mucNuoc_VCH.Count };
                    mn.rowspan = tmp.Max();
                    lstMN.Add(mn);
                }
            }
            return lstMN;
        }
        public MucNuoc TimKiem_MucNuoc(List<MucNuoc_giatri> list, DateTime ThoiGian, int ViTri)
        {
            MucNuoc mn = new MucNuoc();
            foreach (MucNuoc_giatri item in list)
            {
                if (item.thoi_gian == ThoiGian && item.vi_tri_ref == ViTri) 
                {
                    mn.thoi_gian = ThoiGian;
                    mn.vi_tri_code = ViTri;
                    switch (item.don_vi_code)
                    {
                        //case 1: if (lldh.mucNuoc_KTTVTW == null) lldh.mucNuoc_KTTVTW = item.gia_tri; break;
                        //case 2: if (lldh.mucNuoc_VQHTL == null) lldh.mucNuoc_VQHTL = item.gia_tri; break;
                        //case 3: if (lldh.mucNuoc_KHTL == null) lldh.mucNuoc_KHTL = item.gia_tri; break;
                        //case 4: if (lldh.mucNuoc_DHTL == null) lldh.mucNuoc_DHTL = item.gia_tri; break;
                        //case 5: if (lldh.mucNuoc_KTTV == null) lldh.mucNuoc_KTTV = item.gia_tri; break;
                        //case 6: if (lldh.mucNuoc_VCH == null) lldh.mucNuoc_VCH = item.gia_tri; break;
                        //default: break;

                        case 1: mn.mucNuoc_KTTVTW.Add(item.gia_tri); break;
                        case 2: mn.mucNuoc_VQHTL.Add(item.gia_tri); break;
                        case 3: mn.mucNuoc_KHTL.Add(item.gia_tri); break;
                        case 4: mn.mucNuoc_DHTL.Add(item.gia_tri); break;
                        case 5: mn.mucNuoc_KTTV.Add(item.gia_tri); break;
                        case 6: mn.mucNuoc_VCH.Add(item.gia_tri); break;
                        default: break;
                    }
                }
            }
            return mn;
        }

        #endregion
        #region Lưu lượng đến hồ
        public List<LuuLuongDenHo_giatri> GetAllLuuLuongByThoiGian(int viTri, DateTime fromDate, int hours)
        {
            List<LuuLuongDenHo_giatri> lst = new List<LuuLuongDenHo_giatri>();
            DateTime begin = fromDate;
            if (fromDate.Hour == 0) begin = fromDate.AddHours(7);
            string sql = @" WITH tuvanlive AS (
	                    select * 
	                    from(
	                    select row_number() over (partition by thoi_gian, don_vi_ref  ORDER BY tu_van_tb.created_at desc)as rownum,* 
	                    from tu_van_tb 
	                    join don_vi_tb
	                    on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
                        where (thoi_gian =' " + fromDate.ToString("yyyy-MM-dd HH:mm") + " ')) abc where abc.rownum = 1 ) ";
            string select = @" select thoi_gian,phuong_an_code,tu_van_code,don_vi_code,vi_tri_ref,gia_tri from(
                            (select phuong_an_ref, vi_tri_ref, loai_du_lieu_ref, gia_tri, thoi_gian from phuong_an_detail_tb
                                where loai_du_lieu_ref = 1 and vi_tri_ref in (1, 2, 3))padt 
                            join (select phuong_an_code, tu_van_code, don_vi_code from(
                                    (select * from phuong_an_tb )pa
                                    join tuvanlive tvl
                                    on pa.tu_van_ref = tvl.tu_van_code)A)pa_tv_dv
                                    on padt.phuong_an_ref = pa_tv_dv.phuong_an_code
	                        )x
                            where x.vi_tri_ref= " + viTri + " and (x.thoi_gian between ' " + begin.ToString("yyyy-MM-dd HH:mm") + " ' and ' " + begin.AddHours(hours).ToString("yyyy-MM-dd HH:mm") + " ') order by x.thoi_gian,x.phuong_an_code ";
            sql += select;

            lst = _database._databaseContext.Database.SqlQuery<LuuLuongDenHo_giatri>(sql).ToList();
            return lst;
        }
        public List<LuuLuongDenHo> To_LuuLuongDenHo(List<LuuLuongDenHo_giatri> list)
        {
            List<LuuLuongDenHo> lstLLDH = new List<LuuLuongDenHo>();

            for (int i = 0; i < list.Count(); i++)
            {
                LuuLuongDenHo lldh = TimKiem_LuuLuongDenHo(list, list[i].thoi_gian, list[i].vi_tri_ref);
                if (!(lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.thoi_gian == lldh.thoi_gian)
                    && lstLLDH.Exists(LuuLuongDenHo => SoSanhBangListDouble(LuuLuongDenHo.luuLuong_DHTL, lldh.luuLuong_DHTL))
                    && lstLLDH.Exists(LuuLuongDenHo => SoSanhBangListDouble(LuuLuongDenHo.luuLuong_KHTL, lldh.luuLuong_KHTL))
                    && lstLLDH.Exists(LuuLuongDenHo => SoSanhBangListDouble(LuuLuongDenHo.luuLuong_KTTV, lldh.luuLuong_KTTV))
                    && lstLLDH.Exists(LuuLuongDenHo => SoSanhBangListDouble(LuuLuongDenHo.luuLuong_KTTVTW, lldh.luuLuong_KTTVTW))
                    && lstLLDH.Exists(LuuLuongDenHo => SoSanhBangListDouble(LuuLuongDenHo.luuLuong_VQHTL, lldh.luuLuong_VQHTL))
                    && lstLLDH.Exists(LuuLuongDenHo => SoSanhBangListDouble(LuuLuongDenHo.luuLuong_VCH, lldh.luuLuong_VCH))))
                   
                {
                    int[] tmp = { lldh.luuLuong_KTTVTW.Count, lldh.luuLuong_VQHTL.Count, lldh.luuLuong_KHTL.Count, lldh.luuLuong_DHTL.Count, lldh.luuLuong_KTTV.Count, lldh.luuLuong_VCH.Count };
                    lldh.rowspan = tmp.Max();
                    lstLLDH.Add(lldh);
                }
            }
            return lstLLDH;
        }
        public LuuLuongDenHo TimKiem_LuuLuongDenHo(List<LuuLuongDenHo_giatri> list, DateTime ThoiGian, int ViTri)
        {
            LuuLuongDenHo lldh = new LuuLuongDenHo();
            foreach (LuuLuongDenHo_giatri item in list)
            {
                if (item.thoi_gian == ThoiGian && item.vi_tri_ref == ViTri)  //|| item.phuong_an_code == phuongAnCode))
                {
                    lldh.thoi_gian = ThoiGian;
                    lldh.vi_tri_code = ViTri;
                    switch (item.don_vi_code)
                    {
                        //case 1: if (lldh.luuLuong_KTTVTW == null) lldh.luuLuong_KTTVTW = item.gia_tri; break;
                        //case 2: if (lldh.luuLuong_VQHTL == null) lldh.luuLuong_VQHTL = item.gia_tri; break;
                        //case 3: if (lldh.luuLuong_KHTL == null) lldh.luuLuong_KHTL = item.gia_tri; break;
                        //case 4: if (lldh.luuLuong_DHTL == null) lldh.luuLuong_DHTL = item.gia_tri; break;
                        //case 5: if (lldh.luuLuong_KTTV == null) lldh.luuLuong_KTTV = item.gia_tri; break;
                        //case 6: if (lldh.luuLuong_VCH == null) lldh.luuLuong_VCH = item.gia_tri; break;
                        //default: break;

                        case 1: lldh.luuLuong_KTTVTW.Add(item.gia_tri); break;
                        case 2: lldh.luuLuong_VQHTL.Add(item.gia_tri); break;
                        case 3: lldh.luuLuong_KHTL.Add(item.gia_tri); break;
                        case 4: lldh.luuLuong_DHTL.Add(item.gia_tri); break;
                        case 5: lldh.luuLuong_KTTV.Add(item.gia_tri); break;
                        case 6: lldh.luuLuong_VCH.Add(item.gia_tri); break;
                        default: break;
                    }
                }
            }
            return lldh;
        }
        /// <summary>
        /// Phatpx: Hàm lấy về tên các hồ
        /// </summary>
        /// <returns></returns>
        //public List<LuuLuongDenHo> Get_ViTri()
        //{

        //    var query = (from u in _database._databaseContext.vi_tri_tb
        //                 where u.vi_tri_type == 3
        //                 select new LuuLuongDenHo
        //                 {
        //                     vi_tri_code = u.vi_tri_code,
        //                     viTriName = u.vi_tri_name
        //                 }
        //              );

        //    return query.ToList();
        //}
        /// <summary>
        /// Phatpx: Hàm lấy về dữ liệu : tên đơn vị, giá trị đầu đo, ngày giờ lấy giá trị theo tên hồ, ngày bắt đầu và khoảng thời gian xét dữ liệu
        /// </summary>
        /// <param name="tenHo"></param>
        /// <param name="thoiGianBatDau"></param>
        /// <param name="khoangThoiGian"></param>
        /// <returns></returns>
        public List<Cls_LuuLuong> BieuDoLuuLuong_fill(int tenHo, DateTime thoiGianBatDau, int khoangThoiGian)
        {
            List<Cls_LuuLuong> tmp_lst_LuuLuong = new List<Cls_LuuLuong>();
            List<Cls_LuuLuong> lst_LuuLuong = new List<Cls_LuuLuong>();

            string tmp_DateFinish = thoiGianBatDau.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                //'2016-09-28 00:00:00'
                var query_tuvan = String.Format(@"WITH tuvanlive AS (
                                     select * 
                                     from(
                                      select row_number() over (partition by don_vi_ref ORDER BY tu_van_tb.created_at desc)as rownum,* 
                                      from tu_van_tb 
                                      join don_vi_tb
                                      on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
                                      where thoi_gian = '{0}'
                                     ) abc
                                     where abc.rownum = 1
                                    )

                                    select pad.gia_tri giaTri, pad.thoi_gian thoiGianTao, x.don_vi_name tenDonVi from
                                    (
                                     select tvl.don_vi_name, 
                                      row_number() over (partition by tvl.tu_van_code ORDER BY pa.created_at desc)as rownum, 
                                      pa.phuong_an_code,
                                      tvl.tu_van_code
                                     from phuong_an_tb pa
                                     join tuvanlive tvl
                                     on pa.tu_van_ref = tvl.tu_van_code
                                     order by tvl.tu_van_code, pa.created_at
                                    )x
                                    join phuong_an_detail_tb pad
                                    on x.phuong_an_code = pad.phuong_an_ref
                                    where x.rownum = 1 and pad.loai_du_lieu_ref =3 and pad.vi_tri_ref={1}
                                    order by x.tu_van_code, x.phuong_an_code, pad.thoi_gian", tmp_DateFinish, tenHo);
                lst_LuuLuong = _database._databaseContext.Database.SqlQuery<Cls_LuuLuong>(query_tuvan).AsEnumerable().ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
            return (lst_LuuLuong);
        }
        #endregion
        #region Mực nước hạ lưu
        //
        //public List<MucNuoc_giatri> GetAllMucNuocHaLuuByThoiGian(int viTri, DateTime fromDate, int hours)
        //{
        //    List<MucNuoc_giatri> lst = new List<MucNuoc_giatri>();
        //    DateTime begin = fromDate;
        //    if (fromDate.Hour == 0) begin = fromDate.AddHours(7);
        //    string sql = @" WITH tuvanlive AS (
	       //             select * 
	       //             from(
	       //             select row_number() over (partition by thoi_gian, don_vi_ref  ORDER BY tu_van_tb.created_at desc)as rownum,* 
	       //             from tu_van_tb 
	       //             join don_vi_tb
	       //             on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
        //                where (thoi_gian =' " + fromDate.ToString("yyyy-MM-dd HH:mm") + " ')) abc where abc.rownum = 1 ) ";
        //    string select = @" select thoi_gian,phuong_an_code,tu_van_code,don_vi_code,vi_tri_ref,gia_tri from(
        //                    (select phuong_an_ref, vi_tri_ref, loai_du_lieu_ref, gia_tri, thoi_gian from phuong_an_detail_tb
        //                        where loai_du_lieu_ref = 2 and vi_tri_ref in (6, 7, 8))padt 
        //                    join (select phuong_an_code, tu_van_code, don_vi_code from(
        //                            (select * from phuong_an_tb )pa
        //                            join tuvanlive tvl
        //                            on pa.tu_van_ref = tvl.tu_van_code)A)pa_tv_dv
        //                            on padt.phuong_an_ref = pa_tv_dv.phuong_an_code
	       //                 )x
        //                    where x.vi_tri_ref= " + viTri + " and (x.thoi_gian between ' " + begin.ToString("yyyy-MM-dd HH:mm") + " ' and ' " + begin.AddHours(hours).ToString("yyyy-MM-dd HH:mm") + " ') order by x.thoi_gian,x.phuong_an_code ";
        //    sql += select;

        //    lst = _database._databaseContext.Database.SqlQuery<MucNuoc_giatri>(sql).ToList();
        //    return lst;
        //}
        //
        //public List<MucNuoc> To_MucNuocHaLuu(List<MucNuoc_giatri> list)
        //{
        //    List<MucNuoc> lstMNHL = new List<MucNuoc>();

        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        MucNuocHaLuu mnhl = TimKiem_MucNuocHaLuu(list, list[i].thoi_gian, list[i].don_vi_code);
        //        if (!(lstMNHL.Exists(MucNuocHaLuu => MucNuocHaLuu.thoi_gian == mnhl.thoi_gian)
        //            && lstMNHL.Exists(MucNuocHaLuu => SoSanhBangListDouble(MucNuocHaLuu.mucNuoc_DHTL, mnhl.mucNuoc_DHTL))
        //            && lstMNHL.Exists(MucNuocHaLuu => SoSanhBangListDouble(MucNuocHaLuu.mucNuoc_KHTL, mnhl.mucNuoc_KHTL))
        //            && lstMNHL.Exists(MucNuocHaLuu => SoSanhBangListDouble(MucNuocHaLuu.mucNuoc_KTTV, mnhl.mucNuoc_KTTV))
        //            && lstMNHL.Exists(MucNuocHaLuu => SoSanhBangListDouble(MucNuocHaLuu.mucNuoc_KTTVTW, mnhl.mucNuoc_KTTVTW))
        //            && lstMNHL.Exists(MucNuocHaLuu => SoSanhBangListDouble(MucNuocHaLuu.mucNuoc_VQHTL, mnhl.mucNuoc_VQHTL))
        //            && lstMNHL.Exists(MucNuocHaLuu => SoSanhBangListDouble(MucNuocHaLuu.mucNuoc_VCH, mnhl.mucNuoc_VCH))))
        //        //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_DHTL == lldh.mucNuoc_DHTL)
        //        //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_KHTL == lldh.mucNuoc_KHTL)
        //        //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_KTTV == lldh.mucNuoc_KTTV)
        //        //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_KTTVTW == lldh.mucNuoc_KTTVTW)
        //        //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_VCH == lldh.mucNuoc_VCH)
        //        //&& lstLLDH.Exists(LuuLuongDenHo => LuuLuongDenHo.mucNuoc_VQHTL == lldh.mucNuoc_VQHTL)))
        //        {
        //            int[] tmp = { mn.mucNuoc_KTTVTW.Count, mn.mucNuoc_VQHTL.Count, mn.mucNuoc_KHTL.Count, mn.mucNuoc_DHTL.Count, mn.mucNuoc_KTTV.Count, mn.mucNuoc_VCH.Count };
        //            mn.rowspan = tmp.Max();
        //            lstMNHL.Add(mn);
        //        }
        //    }
        //    return lstMNHL;
        //}
        //
        //public MucNuocHaLuu TimKiem_MucNuocHaLuu(List<MucNuoc_giatri> list, DateTime ThoiGian, int DonVi)
        //{
        //    MucNuocHaLuu mnhl = new MucNuocHaLuu();
        //    foreach (MucNuoc_giatri item in list)
        //    {
        //        if (item.thoi_gian == ThoiGian && item.don_vi_code == DonVi)
        //        {
        //            mnhl.thoi_gian = ThoiGian;
        //            mnhl.don_vi_code = DonVi;
        //            switch (item.don_vi_code)
        //            {
        //                //case 1: if (lldh.mucNuoc_KTTVTW == null) lldh.mucNuoc_KTTVTW = item.gia_tri; break;
        //                //case 2: if (lldh.mucNuoc_VQHTL == null) lldh.mucNuoc_VQHTL = item.gia_tri; break;
        //                //case 3: if (lldh.mucNuoc_KHTL == null) lldh.mucNuoc_KHTL = item.gia_tri; break;
        //                //case 4: if (lldh.mucNuoc_DHTL == null) lldh.mucNuoc_DHTL = item.gia_tri; break;
        //                //case 5: if (lldh.mucNuoc_KTTV == null) lldh.mucNuoc_KTTV = item.gia_tri; break;
        //                //case 6: if (lldh.mucNuoc_VCH == null) lldh.mucNuoc_VCH = item.gia_tri; break;
        //                //default: break;

        //                case 1: mn.mucNuoc_KTTVTW.Add(item.gia_tri); break;
        //                case 2: mn.mucNuoc_VQHTL.Add(item.gia_tri); break;
        //                case 3: mn.mucNuoc_KHTL.Add(item.gia_tri); break;
        //                case 4: mn.mucNuoc_DHTL.Add(item.gia_tri); break;
        //                case 5: mn.mucNuoc_KTTV.Add(item.gia_tri); break;
        //                case 6: mn.mucNuoc_VCH.Add(item.gia_tri); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    return mn;
        //}
        public List<MucNuocHaLuu> To_MucNuocHaLuu(List<PhuongAn> list, int donViCode, DateTime fromDate, int hours)
        {
            List<MucNuocHaLuu> lstMNHL = new List<MucNuocHaLuu>();
            //DateTime tgBatDau = DateTime.Parse(fromDate.ToString());
            list = list.Where(x => x.loai_du_lieu_ref == 2 && (x.vi_tri_ref == 6 || x.vi_tri_ref == 7 || x.vi_tri_ref == 8) && x.don_vi_code == donViCode && x.thoi_gian > fromDate.AddHours(7) && x.thoi_gian <= fromDate.AddHours(hours + 7)).OrderBy(x => x.thoi_gian).ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                MucNuocHaLuu mnhl = TimKiem_MucNuocHaLuu(list, list[i].thoi_gian, list[i].don_vi_code);
                if (!(lstMNHL.Exists(MucNuocHaLuu => MucNuocHaLuu.thoi_gian == mnhl.thoi_gian)
                    && lstMNHL.Exists(MucNuocHaLuu => MucNuocHaLuu.mucnuoc_HaNoi == mnhl.mucnuoc_HaNoi)
                    && lstMNHL.Exists(MucNuocHaLuu => MucNuocHaLuu.mucnuoc_PhaLai == mnhl.mucnuoc_PhaLai)
                    && lstMNHL.Exists(MucNuocHaLuu => MucNuocHaLuu.mucnuoc_TuyenQuang == mnhl.mucnuoc_TuyenQuang)
                    && lstMNHL.Exists(MucNuocHaLuu => MucNuocHaLuu.MucNuocQuyTrinh == mnhl.MucNuocQuyTrinh)))
                    lstMNHL.Add(mnhl);
            }
            return lstMNHL;
        }
        public MucNuocHaLuu TimKiem_MucNuocHaLuu(List<PhuongAn> list, DateTime ThoiGian, int DonVi)
        {
            MucNuocHaLuu mnhl = new MucNuocHaLuu();
            foreach (PhuongAn item in list)
            {
                if (item.thoi_gian == ThoiGian && item.don_vi_code == DonVi)
                {
                    mnhl.thoi_gian = ThoiGian;
                    mnhl.don_vi_code = DonVi;
                    switch (item.vi_tri_ref)
                    {
                        case 6: if (mnhl.mucnuoc_HaNoi == null) mnhl.mucnuoc_HaNoi = item.gia_tri; break;
                        case 7: if (mnhl.mucnuoc_PhaLai == null) mnhl.mucnuoc_PhaLai = item.gia_tri; break;
                        case 8: if (mnhl.mucnuoc_TuyenQuang == null) mnhl.mucnuoc_TuyenQuang = item.gia_tri; break;
                        default: break;
                    }
                }
            }
            return mnhl;
        }
        public int getSoPA(string ten_Dv, DateTime thoiGianBD)
        {
            try
            {
                var so_PA = "";
                var query_SoPhuongAn = String.Format(@"select count(distinct pad.phuong_an_ref) from phuong_an_detail_tb pad
                                        join vi_tri_tb vt on
                                        pad.vi_tri_ref = vt.vi_tri_code
                                        where pad.phuong_an_ref in( 

                                        select phuong_an_code from phuong_an_tb where tu_van_ref =(
                                        select  tu_van_code from tu_van_tb where don_vi_ref ={0} and thoi_gian ='{1}' order by  created_at desc limit 1))
                                        and vt.vi_tri_type=4
                                    ", ten_Dv, thoiGianBD.ToString("yyyy-MM-dd"));
                so_PA = (_database._databaseContext.Database.SqlQuery<Int64>(query_SoPhuongAn).FirstOrDefault().ToString());
                if (so_PA != "0")
                    return int.Parse(so_PA);
                else return 1;
            
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<Cls_MNHL> LayDuLieuBieuDo_MNHL(int pa_Number, string ten_Dv, DateTime thoiGianBD, int khoangTG)
        {
            thoiGianBD = thoiGianBD.AddHours(7);
            DateTime thoiGianKT = thoiGianBD.AddHours(khoangTG);

            List<Cls_MNHL> result = new List<Cls_MNHL>();
            try
            {
                var query_bieuDo_MNHL = String.Format(@"select *from (select row_number() over (partition by pad.thoi_gian, vt.vi_tri_name ORDER BY pad.phuong_an_ref )as rownum, pad.phuong_an_ref,
                                     vt.vi_tri_name ten_Vi_Tri , pad.gia_tri, pad.thoi_gian thoi_Gian_Tao
                                    from vi_tri_tb vt
                                    join phuong_an_detail_tb pad
                                    on vt.vi_tri_code = pad.vi_tri_ref 
                                    where pad.phuong_an_ref in (select phuong_an_code from phuong_an_tb where tu_van_ref =(
                                    select  tu_van_code from tu_van_tb where don_vi_ref ={0} and thoi_gian = '{1}'  order by  created_at desc limit 1
                                    )) and vt.vi_tri_type=4 and (pad.thoi_gian between '{2}' and '{3}')

                                    order by pad.phuong_an_ref, pad.thoi_gian)as dt where dt.rownum = {4}
                                    ", ten_Dv, thoiGianBD.ToString("yyyy-MM-dd"), thoiGianBD.ToString("yyyy-MM-dd HH:mm"), thoiGianKT.ToString("yyyy-MM-dd HH:mm"), pa_Number);

                result = _database._databaseContext.Database.SqlQuery<Cls_MNHL>(query_bieuDo_MNHL).AsEnumerable().ToList();
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        /// <summary>
        /// HieuTM: Viet Fix tam loi cua Hieu
        /// </summary>
        /// <param name="pa_Number"></param>
        /// <param name="madonvi"></param>
        /// <param name="thoiGianBD"></param>
        /// <param name="khoangTG"></param>
        /// <returns></returns>
        public List<PhuongAn> LayDuLieuBieuDo_MNHL_DataTable_1PhuongAn(int pa_Number, int madonvi, DateTime thoiGianBD, int khoangTG)
        {
            thoiGianBD = thoiGianBD.AddHours(7);
            DateTime thoiGianKT = thoiGianBD.AddHours(khoangTG);

            List<PhuongAn> result = new List<PhuongAn>();
            try
            {
                var query_bieuDo_MNHL = String.Format(@"select *from (select row_number() over (partition by pad.thoi_gian, vt.vi_tri_name ORDER BY pad.phuong_an_ref )as rownum, pad.phuong_an_ref,
                                     vt.vi_tri_code vi_tri_ref , pad.gia_tri gia_tri, pad.thoi_gian, {0} don_vi_code, 2 loai_du_lieu_ref
                                    from vi_tri_tb vt
                                    join phuong_an_detail_tb pad
                                    on vt.vi_tri_code = pad.vi_tri_ref 
                                    where pad.phuong_an_ref in (select phuong_an_code from phuong_an_tb where tu_van_ref =(
                                    select  tu_van_code from tu_van_tb where don_vi_ref ={0} and thoi_gian = '{1}'  order by  created_at desc limit 1
                                    )) and vt.vi_tri_type=4 and (pad.thoi_gian between '{2}' and '{3}')

                                    order by pad.phuong_an_ref, pad.thoi_gian)as dt where dt.rownum = {4}
                                    ", madonvi, thoiGianBD.ToString("yyyy-MM-dd"), thoiGianBD.ToString("yyyy-MM-dd HH:mm"), thoiGianKT.ToString("yyyy-MM-dd HH:mm"), pa_Number);

                result = _database._databaseContext.Database.SqlQuery<PhuongAn>(query_bieuDo_MNHL).AsEnumerable().ToList();
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        #endregion
        #region Phương án chuyển đổi
        public IEnumerable<PhuongAnChuyenDoi> ChuyenDoi(List<PhuongAn> x)
        {
            List<PhuongAnChuyenDoi> lstPACD = new List<PhuongAnChuyenDoi>();
            int indexdonvi = 0;
            int indexphuongan = 0;
            for (int i = 0; i < x.Count; i = i + 15)
            {
                int nguyen = i / 15;
                PhuongAnChuyenDoi tmp = TimKiem(x, x[i].don_vi_code, x[i].phuong_an_code, x[i].thoi_gian);
                if (i > 0)
                {
                    if (tmp.CodePhuongAn != lstPACD[indexphuongan].CodePhuongAn)
                    {
                        lstPACD[indexphuongan].RowPhuongAn = nguyen - indexphuongan;
                        indexphuongan = nguyen;
                    }

                    if (tmp.Don_Vi != lstPACD[indexdonvi].Don_Vi)
                    {
                        lstPACD[indexdonvi].RowDonVi = nguyen - indexdonvi;
                        indexdonvi = nguyen;
                    }
                }
                lstPACD.Add(tmp);
            }

            lstPACD[indexphuongan].RowPhuongAn = x.Count / 15 - indexphuongan;
            lstPACD[indexdonvi].RowDonVi = x.Count / 15 - indexdonvi;
            return lstPACD;
        }
        public PhuongAnChuyenDoi TimKiem(List<PhuongAn> list, int DonViCode, int PhuongAnCode, DateTime ThoiGian)
        {
            PhuongAnChuyenDoi pacd = new PhuongAnChuyenDoi();
            foreach (PhuongAn item in list)
            {
                if (item.don_vi_code == DonViCode && item.phuong_an_code == PhuongAnCode && item.thoi_gian == ThoiGian)
                {
                    pacd.CodeDonVi = DonViCode;
                    pacd.Don_Vi = item.ki_hieu;
                    pacd.CodePhuongAn = PhuongAnCode;
                    pacd.Phuong_An = item.kien_nghi;
                    pacd.Thoi_Diem = ThoiGian;
                    if (item.vi_tri_ref == 1)
                    {
                        if (item.loai_du_lieu_ref == 3)
                            pacd.SonLa_XaDay = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 4)
                            pacd.SonLa_XaMat = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 1)
                            pacd.SonLa_LuuLuong = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 2)
                            pacd.SonLa_MucNuoc = item.gia_tri;
                    }
                    else if (item.vi_tri_ref == 2)
                    {
                        if (item.loai_du_lieu_ref == 3)
                            pacd.HoaBinh_XaDay = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 4)
                            pacd.HoaBinh_XaMat = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 1)
                            pacd.HoaBinh_LuuLuong = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 2)
                            pacd.HoaBinh_MucNuoc = item.gia_tri;
                    }
                    else if (item.vi_tri_ref == 3)
                    {
                        if (item.loai_du_lieu_ref == 3)
                            pacd.TuyenQuang_XaDay = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 4)
                            pacd.TuyenQuang_XaMat = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 1)
                            pacd.TuyenQuang_LuuLuong = item.gia_tri;
                        else if (item.loai_du_lieu_ref == 2)
                            pacd.TuyenQuang_MucNuoc = item.gia_tri;
                    }
                    else if (item.vi_tri_ref == 6)
                    {
                        pacd.HaLuu_HaNoi = item.gia_tri;
                    }
                    else if (item.vi_tri_ref == 7)
                    {
                        pacd.HaLuu_PhaLai = item.gia_tri;
                    }
                    else if (item.vi_tri_ref == 8)
                    {
                        pacd.HaLuu_TuyenQuang = item.gia_tri;
                    }
                }
            }
            return pacd;
        }
        #endregion
        #region So sánh
        public List<MucNuoc_giatri_SoSanh> BieuDoSoSanh_Select7H()
        {
            List<MucNuoc_giatri_SoSanh> lstMNGT = new List<MucNuoc_giatri_SoSanh>();
            string sql = @" WITH tuvanlive AS (
	                    select * 
	                    from(
	                    select row_number() over (partition by thoi_gian, don_vi_ref  ORDER BY tu_van_tb.created_at desc)as rownum,* 
	                    from tu_van_tb 
	                    join don_vi_tb
	                    on tu_van_tb.don_vi_ref = don_vi_tb.don_vi_code
	                    ) abc
	                    where abc.rownum = 1
	                    )
                    select padt.thoi_gian ThoiGian,vi_tri_ref ViTri, phuong_an_code PhuongAn,tu_van_code TuVan,don_vi_code DonVi,gia_tri GiaTri from
                    ((select phuong_an_code,tu_van_code,don_vi_code from(
	                    select tvl.don_vi_name, tvl.don_vi_code, pa.created_at, tvl.thoi_gian,
	                    row_number() over (partition by tvl.thoi_gian, tvl.tu_van_code ORDER BY pa.created_at)as rownumsum, 
	                    pa.phuong_an_code,
	                    tvl.tu_van_code
	                    from phuong_an_tb pa
	                    join tuvanlive tvl
	                    on pa.tu_van_ref = tvl.tu_van_code 
	                    )x
	                    where  x.rownumsum = 1
	                    order by x.thoi_gian desc, x.don_vi_code, x.created_at)pa_tv_dv
                    join
                    (select * from phuong_an_detail_tb 
                    where loai_du_lieu_ref=2 
                        and (SELECT EXTRACT(hour FROM thoi_gian)=7 or (SELECT EXTRACT(hour FROM thoi_gian)=19)) 
                        and vi_tri_ref in(1,2,3) and deleted_status=0) padt
                    on pa_tv_dv.phuong_an_code=padt.phuong_an_ref)
                    order by thoi_gian, tu_van_code desc, don_vi_code, vi_tri_ref ";
            lstMNGT = _database._databaseContext.Database.SqlQuery<MucNuoc_giatri_SoSanh>(sql).ToList();
            return lstMNGT;
        }
        public List<MucNuoc_SoSanh> ChuyenDoi_MucNuoc(List<MucNuoc_giatri_SoSanh> lstMNGT, int vi_tri_code, DateTime from_date, DateTime to_date)
        {
            List<MucNuoc_SoSanh> lstMN = new List<MucNuoc_SoSanh>();
            List<ThucDoTLVN> lstThucDo = new List<ThucDoTLVN>();
            lstThucDo = GetThucDoTLVN(vi_tri_code, from_date, to_date);
            lstMNGT = lstMNGT.Where(x => x.ViTri == vi_tri_code && x.ThoiGian >= DateTime.Parse(from_date.Date.ToString()) && x.ThoiGian < DateTime.Parse(to_date.AddDays(1).Date.ToString())).ToList();
            foreach (MucNuoc_giatri_SoSanh mngt in lstMNGT)
            {
                MucNuoc_SoSanh mn = TimKiem_ChuyenDoi_MucNuoc(lstMNGT, vi_tri_code, mngt.ThoiGian);
                mn.vi_tri_code = mngt.ViTri;
                mn.thoi_gian = mngt.ThoiGian;
                if (!(lstMN.Exists(x => x.vi_tri_code == mn.vi_tri_code
                && x.thoi_gian == mngt.ThoiGian
                && x.mucnuoc_DHTL == mn.mucnuoc_DHTL
                && x.mucnuoc_KHTL == mn.mucnuoc_KHTL
                && x.mucnuoc_KTTV == mn.mucnuoc_KTTV
                && x.mucnuoc_KTTVTW == mn.mucnuoc_KTTVTW
                && x.mucnuoc_VCH == mn.mucnuoc_VCH
                && x.mucnuoc_VQHTL == mn.mucnuoc_VQHTL)))
                {
                    try
                    {
                        var tmp = lstThucDo.FirstOrDefault(x => x.ThoiGian.Date == mn.thoi_gian.Date && x.Gio == mn.thoi_gian.Hour);
                        mn.MucNuocThucDo = tmp != null ? tmp.Value : -1;
                        lstMN.Add(mn);
                    }
                    catch (Exception ex)
                    { }
                }
            }
            return lstMN;
        }
        public MucNuoc_SoSanh TimKiem_ChuyenDoi_MucNuoc(List<MucNuoc_giatri_SoSanh> lstMNGT, int vi_tri_code, DateTime? thoi_gian)
        {
            MucNuoc_SoSanh mn = new MucNuoc_SoSanh();
            List<MucNuoc_giatri_SoSanh> lstMNGT_tmp = lstMNGT.Where(x => x.ViTri == vi_tri_code && x.ThoiGian == thoi_gian).ToList();
            foreach (MucNuoc_giatri_SoSanh mngt in lstMNGT_tmp)
                switch (mngt.DonVi)
                {
                    case 1:
                        mn.mucnuoc_KTTVTW = mngt.GiaTri;
                        break;
                    case 2:
                        mn.mucnuoc_VQHTL = mngt.GiaTri;
                        break;
                    case 3:
                        mn.mucnuoc_KHTL = mngt.GiaTri;
                        break;
                    case 4:
                        mn.mucnuoc_DHTL = mngt.GiaTri;
                        break;
                    case 5:
                        mn.mucnuoc_KTTV = mngt.GiaTri;
                        break;
                    case 6:
                        mn.mucnuoc_VCH = mngt.GiaTri;
                        break;
                    default:
                        break;
                }
            return mn;
        }
        #endregion
        
        public bool SoSanhBangListDouble(List<double> lst1, List<double> lst2)
        {
            if (lst1.Count != lst2.Count) return false;
            else
            {
                for (int i = 0; i < lst1.Count; i++)
                    if (lst1[i] != lst2[i]) return false;
            }
            return true;
        }
        public bool SaveTongHop(PhuongAn PhuongAn)
        {
            bool check = true;
            tong_hop_tb TongHop = new tong_hop_tb();
            #region Kiểm tra tồn tại và cập nhật dữ liệu.

            try
            {
                var dateUpdate = _database._databaseContext.tu_van_tb.Select(x => x.thoi_gian)
                    .Distinct()
                    .OrderByDescending(x => x.Value)
                    .FirstOrDefault();
                TongHop.kien_nghi_dieu_hanh =PhuongAn.TongHop_KienNghi;
                TongHop.tong_hop_tinh_toan =PhuongAn.TongHop_TongHop;
                TongHop.phan_tich_nhan_xet =PhuongAn.TongHop_phanTich;
                TongHop.created_at =DateTime.Now;
                TongHop.deleted_status = 0;
               
                TongHop.thoi_gian = dateUpdate;
               
                _database._databaseContext.tong_hop_tb.Add(TongHop);
                _database._databaseContext.SaveChanges();


                //= _database._databaseContext.user_tb.FirstOrDefault(c => c.user_code == user.user_code);
                //tmp.username = user.username;
                //tmp.email = user.email;
                //tmp.dien_thoai = user.dien_thoai;
                //tmp.dia_chi = user.dia_chi;
                //tmp.ghi_chu = user.ghi_chu;

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

