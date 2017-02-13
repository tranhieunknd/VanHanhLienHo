using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cwrs.Lib
{
    public class QTLH_BonHoLon
    {
        #region Private member.
        /// <summary>
        /// Dự báo mực nước Hà Nội
        /// </summary>
        private double _MNDB_HaNoi;

        /// <summary>
        /// Dự báo mực nước Tuyên Quang
        /// </summary>
        private double _MNDB_TuyenQuang;

        /// <summary>
        /// Xu thế mực nước sông Đà
        /// <para>1: lên</para><para>0: xuống</para>
        /// </summary>
        private int _XuThe_SongDa;

        /// <summary>
        /// Lưu lượng vào hồ Hòa Bình
        /// </summary>
        private double _Qvao_HoaBinh;

        /// <summary>
        /// Lưu lượng vào hồ Tuyên Quang
        /// </summary>
        private double _Qvao_TuyenQuang;

        /// <summary>
        /// Xu thế mực nước tại Hà Nội
        /// <para>1: lên</para><para>0: xuống</para>
        /// </summary>
        private int _XuThe_HaNoi;

        ///// <summary>
        ///// Xu thế mực nước tại Hòa Bình
        ///// <para>1: lên</para><para>0: xuống</para>
        ///// </summary>
        //private int _XuThe_HoaBinh;

        /// <summary>
        /// Ngày tính toán trong chương trình
        /// </summary>
        private DateTime _NgayTinhToan;

        private string _MauVang = "yel";
        private string _MauDo = "red";
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Hòa Bình -  từ 15 tháng 6 đến 19 tháng 7.
        /// </summary>
        private double _MucNuocCaoNhat_LuSom_HoaBinh = 105;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Hòa Bình - từ 20 tháng 7 đến 21 tháng 8.
        /// </summary>
        private double _MucNuocCaoNhat_LuChinhVu_HoaBinh = 101;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Hòa Bình - từ 22 tháng 8 đến 15 tháng 9.
        /// </summary>
        private double _MucNuocCaoNhat_LuMuon_HoaBinh = 117;

        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Sơn La -  từ 15 tháng 6 đến 19 tháng 7.
        /// </summary>
        private double _MucNuocCaoNhat_LuSom_SonLa = 200;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Sơn La - từ 20 tháng 7 đến 21 tháng 8.
        /// </summary>
        private double _MucNuocCaoNhat_LuChinhVu_SonLa = 194;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Sơn La - từ 22 tháng 8 đến 15 tháng 9.
        /// </summary>
        private double _MucNuocCaoNhat_LuMuon_SonLa = 215;

        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Thác Bà -  từ 15 tháng 6 đến 19 tháng 7.
        /// </summary>
        private double _MucNuocCaoNhat_LuSom_ThacBa = 56;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Thác Bà - từ 20 tháng 7 đến 21 tháng 8.
        /// </summary>
        private double _MucNuocCaoNhat_LuChinhVu_ThacBa = 56;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Thác Bà - từ 22 tháng 8 đến 15 tháng 9.
        /// </summary>
        private double _MucNuocCaoNhat_LuMuon_ThacBa = 58;

        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Tuyên Quang -  từ 15 tháng 6 đến 19 tháng 7.
        /// </summary>
        private double _MucNuocCaoNhat_LuSom_TuyenQuang = 105.2;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Tuyên Quang - từ 20 tháng 7 đến 21 tháng 8.
        /// </summary>
        private double _MucNuocCaoNhat_LuChinhVu_TuyenQuang = 105.2;
        /// <summary>
        /// Mực nước lớn nhất cho phép của hồ Tuyên Quang - từ 22 tháng 8 đến 15 tháng 9.
        /// </summary>
        private double _MucNuocCaoNhat_LuMuon_TuyenQuang = 120;
        #endregion

        #region Output member.
        /// <summary>
        /// Mực nước thượng lưu cho phép
        /// </summary>
        public string _Htl_Chophep_SonLa = "";
        /// <summary>
        /// Lưu lượng ra cho phép
        /// </summary>
        public string _Qra_Chophep_SonLa = "";
        /// <summary>
        /// Mực nước thượng lưu cho phép
        /// </summary>
        public string _Htl_Chophep_HoaBinh = "";
        /// <summary>
        /// Lưu lượng ra cho phép
        /// </summary>
        public string _Qra_Chophep_HoaBinh = "";
        /// <summary>
        /// Mực nước thượng lưu cho phép
        /// </summary>
        public string _Htl_Chophep_TuyenQuang = "";
        /// <summary>
        /// Lưu lượng ra cho phép
        /// </summary>
        public string _Qra_Chophep_TuyenQuang = "";
        /// <summary>
        /// Mực nước thượng lưu cho phép
        /// </summary>
        public string _Htl_Chophep_ThacBa = "";
        /// <summary>
        /// Lưu lượng ra cho phép
        /// </summary>
        public string _Qra_Chophep_ThacBa = "";
        /// <summary>
        /// Dự báo mực nước hồ Hòa Bình
        /// </summary>
        private double _DBMN_HoHoaBinh = 0;

        
        public string _QuyTrinh_HoaBinh = "";
        public string _QuyTrinh_SonLa = "";
        public string _QuyTrinh_ThacBa = "";
        public string _QuyTrinh_TuyenQuang = "";


        /// <summary>
        /// Mực nước thượng lưu cho phép - Số
        /// </summary>
        private double _Htl_Chophep_SonLa_So = 0;
        /// <summary>
        /// Lưu lượng ra cho phép - Số
        /// </summary>
        private double _Qra_Chophep_SonLa_So = 0;
        /// <summary>
        /// Mực nước thượng lưu cho phép - Số
        /// </summary>
        private double _Htl_Chophep_HoaBinh_So = 0;
        /// <summary>
        /// Lưu lượng ra cho phép - Số
        /// </summary>
        private double _Qra_Chophep_HoaBinh_So = 0;
        /// <summary>
        /// Mực nước thượng lưu cho phép - Số
        /// </summary>
        private double _Htl_Chophep_TuyenQuang_So = 0;
        /// <summary>
        /// Lưu lượng ra cho phép - Số
        /// </summary>
        private double _Qra_Chophep_TuyenQuang_So = 0;
        /// <summary>
        /// Mực nước thượng lưu cho phép - Số
        /// </summary>
        private double _Htl_Chophep_ThacBa_So = 0;
        /// <summary>
        /// Lưu lượng ra cho phép - Số
        /// </summary>
        private double _Qra_Chophep_ThacBa_So = 0;

        /// <summary>
        /// Tất cả nguyên nhân sai quy trình của hồ Sơn La được ghi trong biến này
        /// </summary>
        public string _GhiChu_SonLa = "";
        /// <summary>
        /// Tất cả nguyên nhân sai quy trình của hồ Tuyên Quang được ghi trong biến này
        /// </summary>
        public string _GhiChu_TuyenQuang = "";
        /// <summary>
        /// Tất cả nguyên nhân sai quy trình của hồ Hòa Bình được ghi trong biến này
        /// </summary>
        public string _GhiChu_HoaBinh = "";
        /// <summary>
        /// Tất cả nguyên nhân sai quy trình của hồ Thác Bà được ghi trong biến này
        /// </summary>
        public string _GhiChu_ThacBa = "";
        #endregion


        //================================================================================//

        #region Properties member.

        public double MNDB_HaNoi
        {
            get { return _MNDB_HaNoi; }
            set { _MNDB_HaNoi = value; }
        }

        public double MNDB_TuyenQuang
        {
            get { return _MNDB_TuyenQuang; }
            set { _MNDB_TuyenQuang = value; }
        }
        public int XuThe_SongDa
        {
            get { return _XuThe_SongDa; }
            set { _XuThe_SongDa = value; }
        }
        public double Qvao_HoaBinh
        {
            get { return _Qvao_HoaBinh; }
            set { _Qvao_HoaBinh = value; }
        }
        public double Qvao_TuyenQuang
        {
            get { return _Qvao_TuyenQuang; }
            set { _Qvao_TuyenQuang = value; }
        }
        public int XuThe_HaNoi
        {
            get { return _XuThe_HaNoi; }
            set { _XuThe_HaNoi = value; }
        }
        public DateTime NgayTinhToan
        {
            get { return _NgayTinhToan; }
            set { _NgayTinhToan = value; }
        }
        public double DBMN_HoHoaBinh
        {
            get { return _DBMN_HoHoaBinh; }
            set { _DBMN_HoHoaBinh = value; }
        }
        #endregion

        //================================================================================//

        #region Quy trình 4 hồ lớn (Sơn La, Tuyên Quang, Thác Bà, Hòa Bình): Tính toán Htl cho phép và Qra cho phép
        /*
            DateTime temp1 = new DateTime(2011, 6, 9);
            DateTime temp2 = new DateTime(2011, 6, 8);

            int thu = DateTime.Compare(temp1, temp2);
            thu = 1
         * Tức là lấy Hàm Date.Compare lấy đầu trừ cuối
         * Ngày lớn hơn theo quy tắc về số
         */


        void _TruongHopDacBiet(double ip_MucNuocHo,double ip_Nguong, out string _QuyTrinhHo, out string _HtlChoPhep, out string _GhiChu)
        {
            _QuyTrinhHo = "";
            _HtlChoPhep = "";
            _GhiChu = "";
            if (ip_MucNuocHo > ip_Nguong - 0.001)
            {
                _QuyTrinhHo = _MauDo;
                _HtlChoPhep = "<= +" + ip_Nguong + "m";
                _GhiChu = "Vh bảo đảm an toàn công trình";
            }
        }

        /// <summary>
        /// Thời đoạn ở đây bao gồm 6 thời đoạn tất cả (15-6 tới 25-6),(26-6 tới 19-7), (20-7 tới 21-8), (22-8 tới 15-9) và 2 thời đoạn đầu và cuối
        /// </summary>
        /// <param name="Ip_Date"></param>
        /// <returns> Tính từ 1-6</returns>
        private int _ThoiDoan(DateTime ip_Date)
        {
            int temp_year = ip_Date.Year;
            DateTime[] mocdau = new DateTime[5];
            mocdau[0] = new DateTime(temp_year, 6, 15);
            mocdau[1] = new DateTime(temp_year, 6, 26);
            mocdau[2] = new DateTime(temp_year, 7, 20);
            mocdau[3] = new DateTime(temp_year, 8, 22);
            mocdau[4] = new DateTime(temp_year, 9, 16);
            // Nhỏ hơn khoảng nào thì trả ra giá trị của khoảng đó
            for (int i = 0; i < mocdau.Length; i++)
            {
                if (DateTime.Compare(ip_Date, mocdau[i]) <= 0)
                    return (i + 1);
            }
            // Nếu lớn hơn tất cả các khoảng thì trả ra giá trị khoảng cuối cùng
            return 6;
        }


        string _SoSanhQuyTrinh(double ip_Qss, double ip_Hss, double ip_Hv, double ip_Qv)
        {
            if (ip_Hv > ip_Hss)
                return "Sai";
            if (ip_Qv > ip_Qss)
                return "Sai";
            return "";
        }

        /// <summary>
        /// Tính toán ra Htl cho phép và Q ra cho phép của 4 hồ
        /// </summary>
        /// <param name="htl_HoaBinh"> Htl đo của Hòa Bình</param>
        /// <param name="htl_SonLa"> Htl đo của Sơn La</param>
        /// <param name="htl_TuyenQuang"> Htl đo của Tuyên Quang</param>
        /// <param name="qvao_HoaBinh"> Qvào  đo của Hòa Bình</param>
        /// <param name="qvao_TuyenQuang"> Qvào  đo của Tuyên Quang</param>
        /// <param name="mn_HaNoi"> Mực nước đo của Hà Nội - Do trạm gửi về</param>
        /// <param name="mn_TuyenQuang"> Mực nước đo của trạm thủy văn Tuyên Quang gửi về</param>
        /// <param name="htl_ThacBa"></param>
        /// <param name="qra_TuyenQuang"></param>
        /// <param name="qra_HoaBinh"></param>
        public void _TinhToan(float htl_HoaBinh,
                                float htl_SonLa,
                                float htl_TuyenQuang,
                                float htl_ThacBa,
                                float qvao_HoaBinh,
                                float qvao_TuyenQuang,
                                float qra_TuyenQuang,
                                float qra_HoaBinh,
                                float mn_HaNoi,
                                float mn_TuyenQuang
                                )
        {
            int khoangtinhtoan = _ThoiDoan(_NgayTinhToan);
            string _DoiTuongSaiQuyTrinh_HoaBinh = "";
            string _DoiTuongSaiQuyTrinh_SonLa = "";
            string _DoiTuongSaiQuyTrinh_ThacBa = "";
            string _DoiTuongSaiQuyTrinh_TuyenQuang = "";
            switch (khoangtinhtoan)
            {
                case 1: // Thời gian trước 15/6     
                    #region Trường hợp 1
                    if (htl_HoaBinh > 117)
                        _QuyTrinh_HoaBinh = _MauDo;
                    if (htl_SonLa > 215)
                        _QuyTrinh_SonLa = _MauDo;
                    if (htl_TuyenQuang > 120)
                        _QuyTrinh_TuyenQuang = _MauDo;
                    if (htl_ThacBa > 58)
                        _QuyTrinh_ThacBa = _MauDo;
                    _Htl_Chophep_HoaBinh = "<= +117.0m";
                    _Htl_Chophep_SonLa = "<= +215.0m";
                    _Htl_Chophep_ThacBa = "<= +58.0m";
                    _Htl_Chophep_TuyenQuang = "<= +120.0m";
                    break;
                    #endregion
                case 2: // Thời gian từ 15-6 tới 25-6
                    #region Trường hợp 2
                    
                    _Qra_Chophep_SonLa = "";
                    _Qra_Chophep_TuyenQuang = "";
                    _Qra_Chophep_HoaBinh = "<= 4000 m3/s";
                    _Qra_Chophep_HoaBinh_So = 4000;
                    _Qra_Chophep_TuyenQuang = "<= 1500 m3/s";
                    _Qra_Chophep_TuyenQuang_So = 1500;

                    if ((DBMN_HoHoaBinh > 107) && (Math.Abs(htl_SonLa - 205) < 0.001))
                    {
                        _Qra_Chophep_HoaBinh = qvao_HoaBinh.ToString();
                        _Qra_Chophep_HoaBinh_So = qvao_HoaBinh;
                        _GhiChu_HoaBinh = "Cho phép: Qra = Qvào";
                    }
                    if ((Math.Abs(htl_TuyenQuang - 113) < 0.001) && (_Qvao_TuyenQuang > 1500))
                    {
                        //_Qra_Chophep_TuyenQuang = String.Format("{#0:00}", ip_Qvao_TuyenQuang.ToString());
                        _Qra_Chophep_TuyenQuang = qvao_TuyenQuang.ToString();
                        _Qra_Chophep_TuyenQuang_So = qvao_TuyenQuang;
                        _GhiChu_TuyenQuang = "Cho phép: Qra = Qvào";
                    }
                    if (qvao_HoaBinh < 4000)
                    {
                        _Htl_Chophep_HoaBinh = "<=105m";
                        _Htl_Chophep_HoaBinh_So = 105;
                        // Thêm ghi chú
                        if ((200 < htl_SonLa) && (htl_SonLa < 205))
                        {
                            _GhiChu_SonLa = _GhiChu_SonLa + "Đưa Htl về +200m,";
                            _GhiChu_HoaBinh = _GhiChu_HoaBinh + "Đưa Htl về +105m,";
                        }
                    }
                    else
                    {
                        _Htl_Chophep_HoaBinh = "<=107m";
                        _Htl_Chophep_HoaBinh_So = 107;
                    }
                    _Htl_Chophep_ThacBa = "< +56m";
                    _Htl_Chophep_ThacBa_So = 56;
                    _Htl_Chophep_SonLa = "<= +200m";
                    _Htl_Chophep_SonLa_So = 200;
                    _Htl_Chophep_TuyenQuang = "<= +105.2m";
                    _Htl_Chophep_TuyenQuang_So = 105.2;
                    if ((htl_HoaBinh <= 107) && (_Qvao_HoaBinh > 4000))
                    {
                        _Htl_Chophep_SonLa = "<= +205";
                        _Htl_Chophep_SonLa_So = 205;
                    }

                    if (qvao_TuyenQuang > 1500)
                    {
                        _Htl_Chophep_TuyenQuang = "<= +113m";
                        _Htl_Chophep_TuyenQuang_So = 113;
                    }
                    else // Thêm ghi chú
                    {
                        if ((105.2 < htl_TuyenQuang) && (htl_TuyenQuang < 113))
                            _GhiChu_TuyenQuang = _GhiChu_TuyenQuang + "Đưa Htl về +105.2m";
                    }
                  
                    if (qra_HoaBinh > _Qra_Chophep_HoaBinh_So)
                    {
                        _QuyTrinh_HoaBinh = _MauDo;
                        _DoiTuongSaiQuyTrinh_HoaBinh = _DoiTuongSaiQuyTrinh_HoaBinh + "Vượt Qracp";
                    }
                    if (qra_TuyenQuang > _Qra_Chophep_TuyenQuang_So) 
                    {
                        _QuyTrinh_TuyenQuang = _MauDo;
                        _DoiTuongSaiQuyTrinh_TuyenQuang = _DoiTuongSaiQuyTrinh_TuyenQuang + "Vượt Qracp";
                    }
                    //------------//
                    if (htl_TuyenQuang > _Htl_Chophep_TuyenQuang_So)
                    {
                        _QuyTrinh_TuyenQuang = _MauDo;
                        _DoiTuongSaiQuyTrinh_TuyenQuang = _DoiTuongSaiQuyTrinh_TuyenQuang + "Vượt Htlcp";
                    }
                    else // ip_Htl_TuyenQuang < _Htl_Chophep_TuyenQuang_So
                    { 
                        if(htl_TuyenQuang > _MucNuocCaoNhat_LuSom_TuyenQuang)
                            _QuyTrinh_TuyenQuang = _MauVang;
                    }
                    //------------//
                    if (htl_ThacBa > _Htl_Chophep_ThacBa_So)
                    {
                        _QuyTrinh_ThacBa = _MauDo;
                        _DoiTuongSaiQuyTrinh_ThacBa = _DoiTuongSaiQuyTrinh_ThacBa + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_ThacBa > _MucNuocCaoNhat_LuSom_ThacBa)
                            _QuyTrinh_ThacBa = _MauVang;
                    }
                    //------------//
                    if (htl_HoaBinh > _Htl_Chophep_HoaBinh_So)
                    {
                        _QuyTrinh_HoaBinh = _MauDo;
                        _DoiTuongSaiQuyTrinh_HoaBinh = _DoiTuongSaiQuyTrinh_HoaBinh + "Vượt Htlcp";
                    }
                    else
                    {
                        if(qra_HoaBinh < _Qra_Chophep_HoaBinh_So)
                            if(htl_HoaBinh > _MucNuocCaoNhat_LuSom_HoaBinh)
                                _QuyTrinh_HoaBinh = _MauVang;
                    }
                    //------------//

                    if (htl_SonLa > _Htl_Chophep_SonLa_So)
                    {
                        _QuyTrinh_SonLa = _MauDo;
                        _DoiTuongSaiQuyTrinh_SonLa = _DoiTuongSaiQuyTrinh_SonLa + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_SonLa > _MucNuocCaoNhat_LuSom_SonLa)
                            _QuyTrinh_SonLa = _MauVang;
                    }
                    break;
                    #endregion
                case 3: // Thời gian từ 26-6 tới 19-7
                    #region Trường hợp 3
                    _Qra_Chophep_HoaBinh = "";
                    _Qra_Chophep_SonLa = "";
                    _Qra_Chophep_ThacBa = "";
                    _Qra_Chophep_TuyenQuang = "";
                    _Htl_Chophep_HoaBinh = "<= +105m";
                    _Htl_Chophep_HoaBinh_So = 105;
                    _Htl_Chophep_SonLa = "<= +200m";
                    _Htl_Chophep_SonLa_So = 200;
                    _Htl_Chophep_ThacBa = "<= +56m";
                    _Htl_Chophep_ThacBa_So = 56;
                    _Htl_Chophep_TuyenQuang = "<= +105.2m";
                    _Htl_Chophep_TuyenQuang_So = 105.2;

                    if ((_XuThe_HaNoi == 1) && (11.5 - mn_HaNoi > 0.001))
                    {
                        _Htl_Chophep_SonLa = "<= +205m";
                        _Htl_Chophep_SonLa_So = 205;
                    }
                    if (mn_HaNoi > 11.5)
                    {
                        _Htl_Chophep_SonLa = "<= +208m";
                        _Htl_Chophep_HoaBinh = "<= +108m";
                        _Htl_Chophep_SonLa_So = 208;
                        _Htl_Chophep_HoaBinh_So = 108;
                    }

                    if (mn_HaNoi < 11) 
                    {
                        if ((200 < htl_SonLa) && (htl_SonLa <= 208))
                            _GhiChu_SonLa = _GhiChu_SonLa + "Đưa Htl về +200m,";
                        if ((105 < htl_HoaBinh) && (htl_HoaBinh <= 108))
                            _GhiChu_HoaBinh = _GhiChu_HoaBinh + "Đưa Htl về +105m,";
                    }
                    // Ghi chú
                    if ((mn_TuyenQuang < 26) && (105.2 < htl_TuyenQuang) && (htl_TuyenQuang < 113))
                    {
                        _GhiChu_TuyenQuang = _GhiChu_TuyenQuang + "Đưa Htl về +105.2m";
                    }
                    if (_MNDB_TuyenQuang > 26)
                    {
                        _Htl_Chophep_TuyenQuang = "<= +113m";
                        _Htl_Chophep_TuyenQuang_So = 113;
                    }
                    if (htl_TuyenQuang > _Htl_Chophep_TuyenQuang_So)
                    {
                        _QuyTrinh_TuyenQuang = _MauDo;
                        _DoiTuongSaiQuyTrinh_TuyenQuang = _DoiTuongSaiQuyTrinh_TuyenQuang + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_TuyenQuang > _MucNuocCaoNhat_LuSom_TuyenQuang)
                            _QuyTrinh_TuyenQuang = _MauVang;
                    }
                    //------------//
                    if (htl_ThacBa > _Htl_Chophep_ThacBa_So)
                    {
                        _QuyTrinh_ThacBa = _MauDo;
                        _DoiTuongSaiQuyTrinh_ThacBa = _DoiTuongSaiQuyTrinh_ThacBa + "Vượt Htlcp";
                    }
                    else
                    { 
                        if(htl_ThacBa > _MucNuocCaoNhat_LuSom_ThacBa)
                            _QuyTrinh_ThacBa = _MauVang ;
                    }
                    //------------//
                    if (htl_SonLa > _Htl_Chophep_SonLa_So)
                    {
                        _QuyTrinh_SonLa = _MauDo;
                        _DoiTuongSaiQuyTrinh_SonLa = _DoiTuongSaiQuyTrinh_SonLa + "Vượt Htlcp";
                    }
                    else
                    {
                        if(htl_SonLa > _MucNuocCaoNhat_LuSom_SonLa)
                            _QuyTrinh_SonLa = _MauVang;
                    }
                    //------------//
                    if (htl_HoaBinh > _Htl_Chophep_HoaBinh_So)
                    {
                        _QuyTrinh_HoaBinh = _MauDo;
                        _DoiTuongSaiQuyTrinh_HoaBinh = _DoiTuongSaiQuyTrinh_HoaBinh + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_HoaBinh > _MucNuocCaoNhat_LuSom_HoaBinh)
                            _QuyTrinh_HoaBinh = _MauVang;
                    }
                    //------------//
                    break;
                    #endregion
                case 4: // Từ 20-7 tới 21-8
                    #region Trường hợp 4
                     _Qra_Chophep_HoaBinh = "";
                    _Qra_Chophep_SonLa = "";
                    _Qra_Chophep_ThacBa = "";
                    _Qra_Chophep_TuyenQuang = "";

                    //if ((_MNDB_HaNoi > 13.4 - 0.001)
                    //    && (ip_Htl_SonLa > 215 - 0.001)
                    //    && (ip_Htl_HoaBinh > 117 - 0.001)
                    //    && (ip_Htl_TuyenQuang > 120 - 0.001)
                    //    && (ip_Htl_ThacBa > 58 - 0.001))
                    //{
                    //    _GhiChu_HoaBinh = "->Vận hành BĐAT";
                    //    _GhiChu_SonLa = "->Vận hành BĐAT";
                    //    _GhiChu_ThacBa = "->Vận hành BĐAT";
                    //    _GhiChu_TuyenQuang = "->Vận hành BĐAT";
                    //    _QuyTrinh_HoaBinh = _MauDo;
                    //    _Htl_Chophep_HoaBinh = "";
                    //    return;
                    //}

                    _Htl_Chophep_SonLa = "<= +194m";
                    _Htl_Chophep_SonLa_So = 194;

                    // Điều kiện này bao 2 điều kiện trên
                    if (((mn_HaNoi < 11) && (194 < htl_SonLa) && (htl_SonLa < 200 + 0.001)) ||
                        (((11.5 - 0.001 < mn_HaNoi)) && (mn_HaNoi < 12.5) && (200 < htl_SonLa) && (htl_SonLa < 205 + 0.001)) ||
                        ((mn_HaNoi < 13) && (205 < htl_SonLa) && (htl_SonLa < 215)))
                    {
                        _GhiChu_SonLa = _GhiChu_SonLa + "Đưa Htl về +194m";
                    }

                    if (((mn_HaNoi < 11) && (101 < htl_HoaBinh) && (htl_HoaBinh < 107 + 0.001)) ||
                        ((11.5 < mn_HaNoi) && (mn_HaNoi < 12.5) && (107 < htl_HoaBinh) && (htl_HoaBinh < 109 + 0.001)) ||
                        ((mn_HaNoi < 13) && (109 < htl_HoaBinh) && (htl_HoaBinh < 117 + 0.001)))
                        _GhiChu_HoaBinh = _GhiChu_HoaBinh + "Đưa Htl về +101m";


                    if (((mn_TuyenQuang < 27) && (105.2 < htl_TuyenQuang) && (htl_TuyenQuang < 115)) ||
                        ((105.2 < htl_TuyenQuang) && (htl_TuyenQuang < 120)))
                    {
                        _GhiChu_TuyenQuang = _GhiChu_TuyenQuang + "Đưa Htl về +105.2m,";
                    }
                    if (mn_HaNoi < 12.5)
                    {
                        if ((56 < htl_ThacBa) && (htl_ThacBa < 58))
                            _GhiChu_ThacBa = _GhiChu_ThacBa + "Đưa Htl về +56m";
                    }

                    if ((_MNDB_HaNoi > 11.5) && (_XuThe_SongDa == 0))
                    {
                        _Htl_Chophep_SonLa = "<= +196m";
                        _Htl_Chophep_SonLa_So = 196;
                    }
                    if ((_MNDB_HaNoi > 11.5) && (_XuThe_SongDa == 1))
                    {
                        _Htl_Chophep_SonLa = "<= +200m";
                        _Htl_Chophep_SonLa_So = 200;
                    }
                    if ((11.5 < mn_HaNoi) && (mn_HaNoi < 13.1) && (_XuThe_HaNoi == 1))
                    {
                        _Htl_Chophep_SonLa = "<= +203m";
                        _Htl_Chophep_SonLa_So = 203;
                    }
                    if ((11.5 < mn_HaNoi) && (mn_HaNoi < 13.1) && (_XuThe_HaNoi == 1) && (_XuThe_SongDa == 1))
                    {
                        _Htl_Chophep_SonLa = "<= +205m";
                        _Htl_Chophep_SonLa_So = 205;
                    }
                    if ((13.1 < mn_HaNoi) && (_MNDB_HaNoi > 13.4))
                    {
                        _Htl_Chophep_SonLa = "<= +215m";
                        _Htl_Chophep_SonLa_So = 215;
                    }

                    _Htl_Chophep_HoaBinh = "<= +101m";
                    _Htl_Chophep_HoaBinh_So = 101;
                    if (_MNDB_HaNoi > 11.5)// && (_XuThe_SongDa == 1))
                    {
                        _Htl_Chophep_HoaBinh = "<= +107m";
                        _Htl_Chophep_HoaBinh_So = 107;
                    }
                    if ((11.5 < mn_HaNoi) && (mn_HaNoi < 13.1) && (_XuThe_SongDa == 1) && (_XuThe_HaNoi == 1))
                    {
                        _Htl_Chophep_HoaBinh = "<= +109m";
                        _Htl_Chophep_HoaBinh_So = 109;
                    }
                    if ((mn_HaNoi > 13.1) && (_MNDB_HaNoi > 13.4))
                    {
                        _Htl_Chophep_HoaBinh = "<= +117m";
                        _Htl_Chophep_HoaBinh_So = 117;
                    }
                    _Htl_Chophep_TuyenQuang = "<= +105.2m";
                    _Htl_Chophep_TuyenQuang_So = 105.2;
                    if (_MNDB_TuyenQuang > 27)
                    {
                        _Htl_Chophep_TuyenQuang = "<= +115m";
                        _Htl_Chophep_TuyenQuang_So = 115;
                    }
                    if (((_MNDB_HaNoi > 12.5) && (htl_HoaBinh > 109)) || (mn_HaNoi > 12.8))
                    {
                        _Htl_Chophep_TuyenQuang = "<= +120m";
                        _Htl_Chophep_TuyenQuang_So = 120;
                    }
                    _Htl_Chophep_ThacBa = "<= +56m";
                    _Htl_Chophep_ThacBa_So = 56;
                    if (_MNDB_HaNoi > 12.5)
                    {
                        _Htl_Chophep_ThacBa = "<= +58m";
                        _Htl_Chophep_ThacBa_So = 58;
                    }
                    if (htl_TuyenQuang > _Htl_Chophep_TuyenQuang_So)
                    {
                        _QuyTrinh_TuyenQuang = _MauDo;
                        _DoiTuongSaiQuyTrinh_TuyenQuang = _DoiTuongSaiQuyTrinh_TuyenQuang + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_TuyenQuang > _MucNuocCaoNhat_LuChinhVu_TuyenQuang)
                            _QuyTrinh_TuyenQuang = _MauVang;
                    }
                    //------------//
                    if (htl_ThacBa > _Htl_Chophep_ThacBa_So)
                    {
                        _QuyTrinh_ThacBa = _MauDo;
                        _DoiTuongSaiQuyTrinh_ThacBa = _DoiTuongSaiQuyTrinh_ThacBa + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_ThacBa > _MucNuocCaoNhat_LuChinhVu_ThacBa)
                            _QuyTrinh_ThacBa = _MauVang;
                    }
                    //------------//
                    if (htl_SonLa > _Htl_Chophep_SonLa_So)
                    {
                        _QuyTrinh_SonLa = _MauDo;
                        _DoiTuongSaiQuyTrinh_SonLa = _DoiTuongSaiQuyTrinh_SonLa + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_SonLa > _MucNuocCaoNhat_LuChinhVu_SonLa)
                            _QuyTrinh_SonLa = _MauVang;
                    }
                    //------------//
                    if (htl_HoaBinh > _Htl_Chophep_HoaBinh_So)
                    {
                        _QuyTrinh_HoaBinh = _MauDo;
                        _DoiTuongSaiQuyTrinh_HoaBinh = _DoiTuongSaiQuyTrinh_HoaBinh + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_HoaBinh > _MucNuocCaoNhat_LuChinhVu_HoaBinh)
                            _QuyTrinh_HoaBinh = _MauVang;
                    }
                    //------------//
                    string tempQT = "", tempHtl = "", tempGC = "";
                    _TruongHopDacBiet(htl_HoaBinh, 117, out tempQT, out tempHtl, out tempGC );
                    if (tempQT != "") { _QuyTrinh_HoaBinh = tempQT; tempQT = ""; }
                    if (tempHtl != "") { _Htl_Chophep_HoaBinh = tempHtl; tempHtl = ""; }
                    if (tempGC != "") { _GhiChu_HoaBinh = tempGC; tempGC = ""; }
                    _TruongHopDacBiet(htl_SonLa, 215, out tempQT, out tempHtl, out tempGC);
                    if (tempQT != "") { _QuyTrinh_SonLa = tempQT; tempQT = ""; }
                    if (tempHtl != "") { _Htl_Chophep_SonLa = tempHtl; tempHtl = ""; }
                    if (tempGC != "") { _GhiChu_SonLa = tempGC; tempGC = ""; }
                    _TruongHopDacBiet(htl_ThacBa, 58, out tempQT, out tempHtl, out tempGC);
                    if (tempQT != "") { _QuyTrinh_ThacBa = tempQT; tempQT = ""; }
                    if (tempHtl != "") { _Htl_Chophep_ThacBa = tempHtl; tempHtl = ""; }
                    if (tempGC != "") { _GhiChu_ThacBa = tempGC; tempGC = ""; }
                    _TruongHopDacBiet(htl_TuyenQuang, 120, out tempQT, out tempHtl, out tempGC);
                    if (tempQT != "") { _QuyTrinh_TuyenQuang = tempQT; tempQT = ""; }
                    if (tempHtl != "") { _Htl_Chophep_TuyenQuang = tempHtl; tempHtl = ""; }
                    if (tempGC != "") { _GhiChu_TuyenQuang = tempGC; tempGC = ""; }
                   
                    break;
                    #endregion
                case 5: // Từ 22-8 tới 15-9
                    #region Trường hợp 5
                    _Qra_Chophep_HoaBinh = "";
                    _Qra_Chophep_SonLa = "";
                    _Qra_Chophep_ThacBa = "";
                    _Qra_Chophep_TuyenQuang = "";
                    _Htl_Chophep_SonLa = "<= +209m";
                    _Htl_Chophep_SonLa_So = 209;
                    _Htl_Chophep_HoaBinh = "<= +110m";
                    _Htl_Chophep_HoaBinh_So = 110;
                    _Htl_Chophep_TuyenQuang = "<= +115m";
                    _Htl_Chophep_TuyenQuang_So = 115;
                    _Htl_Chophep_ThacBa = "<= +57m";
                    _Htl_Chophep_ThacBa_So = 57;
                    if (htl_TuyenQuang > _Htl_Chophep_TuyenQuang_So)
                    {
                        _QuyTrinh_TuyenQuang = _MauDo;
                        _DoiTuongSaiQuyTrinh_TuyenQuang = _DoiTuongSaiQuyTrinh_TuyenQuang + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_TuyenQuang > _MucNuocCaoNhat_LuMuon_TuyenQuang)
                            _QuyTrinh_TuyenQuang = _MauVang;
                    }
                    //------------//
                    if (htl_ThacBa > _Htl_Chophep_ThacBa_So)
                    {
                        _QuyTrinh_ThacBa = _MauDo;
                        _DoiTuongSaiQuyTrinh_ThacBa = _DoiTuongSaiQuyTrinh_ThacBa + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_ThacBa > _MucNuocCaoNhat_LuChinhVu_ThacBa)
                            _QuyTrinh_ThacBa = _MauVang;
                    }
                    //------------//
                    if (htl_SonLa > _Htl_Chophep_SonLa_So)
                    {
                        _QuyTrinh_SonLa = _MauDo;
                        _DoiTuongSaiQuyTrinh_SonLa = _DoiTuongSaiQuyTrinh_SonLa + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_SonLa > _MucNuocCaoNhat_LuMuon_SonLa)
                            _QuyTrinh_SonLa = _MauVang;
                    }
                    //------------//
                    if (htl_HoaBinh > _Htl_Chophep_HoaBinh_So)
                    {
                        _QuyTrinh_HoaBinh = _MauDo;
                        _DoiTuongSaiQuyTrinh_HoaBinh = _DoiTuongSaiQuyTrinh_HoaBinh + "Vượt Htlcp";
                    }
                    else
                    {
                        if (htl_HoaBinh > _MucNuocCaoNhat_LuMuon_HoaBinh)
                            _QuyTrinh_HoaBinh = _MauVang;
                    }
                    //------------//
                    break;
                    #endregion 
                case 6:
                    #region Trường hợp 6
                     if (htl_HoaBinh > 117)
                        _QuyTrinh_HoaBinh = _MauDo;
                    if (htl_SonLa > 215)
                        _QuyTrinh_SonLa = _MauDo;
                    if (htl_TuyenQuang > 120)
                        _QuyTrinh_TuyenQuang = _MauDo;
                    if (htl_ThacBa > 58)
                        _QuyTrinh_ThacBa = _MauDo;
                    _Htl_Chophep_HoaBinh = "<= +117.0m";
                    _Htl_Chophep_SonLa = "<= +215.0m";
                    _Htl_Chophep_ThacBa = "<= +58.0m";
                    _Htl_Chophep_TuyenQuang = "<= +120.0m";
                    break;
                    #endregion
            }

            // Ưu tiên cho cái ghi chú,
            if (_GhiChu_HoaBinh == "") _GhiChu_HoaBinh = _DoiTuongSaiQuyTrinh_HoaBinh;
            if (_GhiChu_SonLa == "") _GhiChu_SonLa = _DoiTuongSaiQuyTrinh_SonLa;
            if (_GhiChu_ThacBa == "") _GhiChu_ThacBa = _DoiTuongSaiQuyTrinh_ThacBa;
            if (_GhiChu_TuyenQuang == "") _GhiChu_TuyenQuang = _DoiTuongSaiQuyTrinh_TuyenQuang;
        }

        #endregion

        //================================================================================//
    }
}
