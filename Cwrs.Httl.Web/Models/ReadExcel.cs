using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;
using System.IO;
using System.Globalization;
using Cwrs.LogDB;

namespace Cwrs.Httl.Web.Models
{
    public class ReadExcel
    {

        bool kiemtra = false;//bien kiem tra xem phuong an co gia tri hay khong.

        public DataTable MySheet(string name)
        {
            DataTable dt = new DataTable();
            OleDbConnection myConnection = new OleDbConnection();
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), name);
                // name="~/Files/somefiles/MauNhapLieu_20160808_081958.xlsx";
                if (name[name.Length - 1] == 'x')
                    //myConnection = new OleDbConnection(
                    //                "Provider=Microsoft.ACE.OLEDB.12.0; " +
                    //                 "data source='" + path + "';" +
                    //                    "Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\" ");
                    myConnection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties=Excel 8.0;", name));
                else if (name[name.Length - 1] == 's')
                    // chuỗi connection mở file 
                    myConnection = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", name)); //"provider=Microsoft.Jet.OLEDB.4.0; data source={0};HDR=Yes;IMEX=1;ReadOnly:=false;UpdateLinks:=0\" "

                else return dt;


                myConnection.Open();
                DataTable mySheets = myConnection.GetOleDbSchemaTable(
                                OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" }
                                );
                //mySheets.Columns.Remove()

                //dt = mySheets.Columns["TABLE_NAME"].Table;

                dt.Columns.Add("Sheet", typeof(string)); //đọc trong file có bao nhiêu sheet cho vào datatable
                foreach (DataRow cot in mySheets.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["Sheet"] = cot["TABLE_NAME"];
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                LogDb.WriteLogError("HieuTM", "MySheet", "TuVanController", ex);
            }
            return dt;
        }

        public List<DataTable> makeDataTableFromSheetName(string filename, DataTable mysheet)
        {
            DataTable dtImport;
            List<DataTable> dsImport = new List<DataTable>();
            string connectionString;
            string sheetName;
            if (filename[filename.Length - 1] == 's') connectionString = string.Format("provider=Microsoft.Jet.OLEDB.4.0; data source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;ReadOnly:=false;UpdateLinks:=0\"", filename);
            else if (filename[filename.Length - 1] == 'x') connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";" + "Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\" ";
            else return dsImport;
            try
            {
                System.Data.OleDb.OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString);
                DataSet ds;
                foreach (DataRow sheetName1 in mysheet.Rows)
                {
                    sheetName = sheetName1[0].ToString();
                    OleDbDataAdapter myImportCommand = new OleDbDataAdapter("select * from [" + sheetName + "]", myConnection);
                    ds = new DataSet();
                    myImportCommand.Fill(ds);
                    dtImport = new DataTable();
                    dtImport = ds.Tables[0];
                    dtImport.TableName = sheetName;
                    dsImport.Add(dtImport);
                }
            }
            catch(Exception ex)
            {
                LogDb.WriteLogError("HieuTM", "makeDataTableFromSheetName", "TuVanController", ex);
            }
            return dsImport;
        }

        //loc du lieu bang phuong phuong an
        /// <summary>
        /// ThamNT
        /// </summary>
        /// <param name="fileExcel">Đọc dữ liệu bảng bảng phương án</param>
        /// <param name="x">hoành độ bắt đầu bảng phương án</param>
        /// <param name="y">tung độ bắt đầu bảng phương án</param>
        /// <returns>Trả về dữ liệu bảng phương án</returns>
        public List<PhuongAnDetail_Model> PhuongAn_Detail(DataTable fileExcel, int x, int y)
        {
            List<PhuongAnDetail_Model> lstphuongan = new List<PhuongAnDetail_Model>();
            int vitri = -1;
            int loaidulieu = -1;
            string date = "";
            string hour = "";
            string namecolumn = "";
            kiemtra = false;
            char[] hr = { 'h', 'H', ' ' };
            //DateTime datetime;
            PhuongAnDetail_Model phuongandetail;
            for (int i = y; i <= y + 7; i++)//di theo truc oy
            {
                for (int j = x + 1; j <= x + 17; j++)//di theo truc ox
                {
                    // xet vi tri
                    if (j == (x + 3))
                    {
                        vitri = 1;

                    }
                    if (j == (x + 7))
                    {
                        vitri = 2;

                    }
                    if (j == (x + 11))
                    {
                        vitri = 3;

                    }
                    // xet loai dulieu
                    if (j == (x + 3) || j == (x + 7) || j == (x + 11))
                    {
                        loaidulieu = 3;

                    }
                    if (j == (x + 4) || j == (x + 8) || j == (x + 12))
                    {

                        loaidulieu = 4;
                    }
                    if (j == (x + 5) || j == (x + 9) || j == (x + 13))
                    {
                        loaidulieu = 1;

                    }
                    if (j == (x + 6) || j == (x + 10) || j == (x + 14))
                    {
                        loaidulieu = 2;

                    }
                    if (j == (x + 15))
                    {
                        vitri = 6;
                        loaidulieu = 2;

                    }
                    if (j == (x + 16))
                    {

                        vitri = 7;
                        loaidulieu = 2;

                    }
                    if (j == (x + 17))
                    {
                        vitri = 8;
                        loaidulieu = 2;

                    }
                    // Lay Ngay, Gio cua Phuong An
                    namecolumn = "F" + j.ToString();
                    if ((j == (x + 1)) && (i == y || i == (y + 2) || i == (y + 6))) date = fileExcel.Rows[i][namecolumn].ToString();
                    if (j == (x + 2)) hour = fileExcel.Rows[i][namecolumn].ToString();
                    if (j == (x + 2))
                    {
                        // tach "h" hoi hour
                        hour = hour.Substring(0, hour.IndexOf("h"));
                    }

                    if (j > (x + 2))
                    {
                        phuongandetail = new PhuongAnDetail_Model();
                        if (string.IsNullOrWhiteSpace(fileExcel.Rows[i][namecolumn].ToString()))
                            phuongandetail.gia_tri = "";
                        else
                        {
                            phuongandetail.gia_tri = fileExcel.Rows[i][namecolumn].ToString();
                            kiemtra = true;
                        }
                        phuongandetail.vi_tri_ref = vitri;
                        phuongandetail.thoi_gian = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture); //DateTime.Parse(date);
                        phuongandetail.thoi_gian = phuongandetail.thoi_gian.Value.AddHours(double.Parse(hour));
                        phuongandetail.loai_du_lieu_ref = loaidulieu;
                        lstphuongan.Add(phuongandetail);
                    }
                }
            }
            return lstphuongan;
        }
        public PhuongAnExcel Phuongan_Dettail_excel(DataTable fileExcel, int x, int y)
        {
            PhuongAnExcel phuongan = new PhuongAnExcel();
            phuongan.PhuongAn_Detail = PhuongAn_Detail(fileExcel, x, y);
            string namecolumn = "F" + x.ToString();
            phuongan.KienNghiPA = fileExcel.Rows[y + 14][namecolumn].ToString();
            phuongan.PhanTichNhanXet = fileExcel.Rows[y + 11][namecolumn].ToString();
            return phuongan;
        }
        /// <summary>
        /// ThamNT
        /// </summary>
        /// <param name="fileExcel">Đọc dữ liệu bảng diễn biến</param>
        /// <param name="x">hoành độ bắt đầu của bảng diễn biến</param>
        /// <param name="y">tung độ điểm bắt đầu bảng diễn biến</param>
        /// <returns>Trả về các số liệu liên quan chỉ số diễn biến</returns>
        public List<DienBien_Models> DienBien_Excel(DataTable fileExcel, int x, int y)
        {
            List<DienBien_Models> lstdienbien = new List<DienBien_Models>();
            DienBien_Models dienbien;
            string date = "";
            kiemtra = false;
            string hour = "";
            int vitri = -1;
            int loaidulieu = -1;
            string namecolumn = "";
            fileExcel.Columns[0].ColumnName = "F1";
            fileExcel.Columns[4].ColumnName = "F5";
            int isThucDo = -1;
            for (int i = y; i <= y + 6; i++)//di theo truc oy
            {
                for (int j = x; j <= x + 9; j++)//di theo truc ox
                {
                    namecolumn = "F" + j.ToString();
                    //lay thoi gian
                    if (j == (x + 2) || j == (x + 4) || j == (x + 8))
                    {
                        date = fileExcel.Rows[y - 2][namecolumn].ToString();

                    }

                    //lay gia tri is thuc do
                    if (j == (x + 2) || j == (x + 3) || j == (x + 4) || j == (x + 5))
                    {
                        isThucDo = 1;

                    }
                    if (j == (x + 6) || j == (x + 7) || j == (x + 8) || j == (x + 9))
                    {
                        isThucDo = 0;
                    }

                    if (j == x)
                    {

                        if (i == (y))
                        {
                            vitri = 1;
                        }
                        if (i == (y + 2))
                        {
                            vitri = 2;

                        }
                        if (i == (y + 4))
                        {
                            vitri = 3;
                        }
                        if (i == (y + 6))
                        {
                            vitri = 5;

                        }
                    }
                    if (j >= x + 2)
                        hour = fileExcel.Rows[y - 1][namecolumn].ToString();
                    if (j == x + 1 && (i == y || i == y + 2 || i == y + 4 || i == y + 6))
                        loaidulieu = 2;
                    if (j == x + 1 && (i == y + 1 || i == y + 3 || i == y + 5))
                        loaidulieu = 1;
                    if (j > x + 1)
                    {
                        hour = hour.Substring(0, hour.IndexOf("h"));
                    }
                    if ((j >= x + 2) && (i >= y))
                    {
                        dienbien = new DienBien_Models();
                        dienbien.vi_tri_ref = vitri;
                        dienbien.thoi_gian = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dienbien.thoi_gian = dienbien.thoi_gian.Value.AddHours(double.Parse(hour));
                        dienbien.loai_du_lieu_ref = loaidulieu;
                        if (string.IsNullOrWhiteSpace(fileExcel.Rows[i][namecolumn].ToString()))
                            dienbien.gia_tri = "";
                        else
                        {
                            dienbien.gia_tri = fileExcel.Rows[i][namecolumn].ToString();
                            kiemtra = true;
                        }
                        dienbien.is_thuc_do = isThucDo;
                        lstdienbien.Add(dienbien); //thêm 1 đối tượng diễn biến
                    }

                }
            }
            //if (kiemtra == false)
            //    lstdienbien.Clear();
            return lstdienbien;
        }
        public BanTinTuVan BanTinTuVanExcel(List<DataTable> dsImport)
        {

            BanTinTuVan bantintuvan = new BanTinTuVan();
            List<PhuongAnExcel> lstphuonganexcel = new List<PhuongAnExcel>();
            PhuongAnExcel phuonganexcel = new PhuongAnExcel(); ;
            List<DienBien_Models> dienbienexcel = new List<DienBien_Models>();
            LogDb.WriteLogInfo("HieuTM", "BanTinTuVanExcel", "ReadExcel", "Vao ban tin tu van");
            try
            {
                foreach (DataTable table in dsImport)
                {
                    if (table.TableName == "Chung$")
                    {
                        table.Columns[0].ColumnName = "F1";
                        table.Columns[4].ColumnName = "F5";
                        string thoigian = "";
                        thoigian = table.Rows[3]["F5"].ToString();
                        string[] ngay = { "ngày", "tháng", "năm", ",", " ", "/" };
                        int b = -1;
                        string date = "";
                        foreach (string a in thoigian.Split(ngay, StringSplitOptions.RemoveEmptyEntries))
                        {
                            try
                            {
                                b = int.Parse(a);
                                date = date + a + "/";
                            }
                            catch { }
                        }

                        b = date.LastIndexOf("/");
                        date = date.Substring(0, b);
                        bantintuvan.ThoiGian = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //bantintuvan.NhanXetTinhHinh = table.Rows[11]["F1"].ToString();
                        //bantintuvan.DuKien = table.Rows[14]["F1"].ToString();
                        List<string> lstDuKien = DuKienDienBien(table, 1, 11);
                        bantintuvan.NhanXetTinhHinh = lstDuKien[0];
                        bantintuvan.DuKien = lstDuKien[1];
                        dienbienexcel = DienBien_Excel(table, 1, 21);
                        if (kiemtra == true)
                        {
                            bantintuvan.DienBienExcel = dienbienexcel;
                            bantintuvan.HienTrangExcel = HienTrangExcel(table, 1, 30);
                        }
                    }
                    else
                    {
                        table.Columns[0].ColumnName = "F1";
                        phuonganexcel = Phuongan_Dettail_excel(table, 1, 3);
                        //kiem tra neu kiemtra = true bang phuong an co gia tri them vao lst phuong an excel
                        if (kiemtra == true)
                        {
                            lstphuonganexcel.Add(phuonganexcel);
                        }
                    }
                }
                bantintuvan.PhuongAnExcel = lstphuonganexcel;
                LogDb.WriteLogInfo("HieuTM", "BanTinTuVanExcel", "ReadExcel", "Doc xong file excel");
            }
            catch (Exception ex)
            {
                bantintuvan = null;
                LogDb.WriteLogInfo("HieuTM", "BanTinTuVanExcel", "ReadExcel", "Error: "  + ex.Message);
                LogDb.WriteLogError("HieuTM", "BanTinTuVanExcel", "ReadExcel", ex);
            }
            return bantintuvan;
        }
        /// <summary>
        /// ThamNT
        /// </summary>
        /// <param name="fileExcel">Đọc dữ liệu mục hiện trạng công trình</param>
        /// <param name="x">hoành độ bắt đầu từ hồ đầu tiên</param>
        /// <param name="y">tung độ bắt đầu hồ đầu tiên</param>
        /// <returns>trả về số liệu các hồ của mục hiện trạng</returns>
        public List<Hientrang_Model> HienTrangExcel(DataTable fileExcel, int x, int y)
        {
            List<Hientrang_Model> lstHienTrang = new List<Hientrang_Model>();
            Hientrang_Model hientrang;
            int vitri = -1;
            int loaidulieu = -1;
            string namecolumn = "";
            for (int i = y; i <= y + 20; i++)//di theo truc oy
            {
                for (int j = x; j <= x + 2; j++)//di theo truc ox
                {
                    namecolumn = "F" + j.ToString();
                    //tim vi tri
                    if (j == x)
                    {
                        if (i == y)
                        {
                            vitri = 1;
                        }
                        if (i == (y + 6))
                        {
                            vitri = 2;

                        }
                        if (i == (y + 12))
                        {
                            vitri = 3;

                        }
                        if (i == (y + 18))
                        {
                            vitri = 5;

                        }
                        //tim loai du lieu
                        if (i == (y + 1) || i == (y + 7) || i == (y + 13) || i == (y + 19))
                        {
                            loaidulieu = 2;

                        }
                        if (i == (y + 2) || i == (y + 8) || x == (y + 14))
                        {
                            loaidulieu = 1;

                        }
                        if (i == (y + 3) || i == (y + 9) || i == (y + 15))
                        {
                            loaidulieu = 3;

                        }
                        if (i == (y + 4) || i == (y + 10) || i == (y + 16) || i == (y + 20))
                        {
                            loaidulieu = 4;

                        }
                    }
                    if ((j == x + 2) && (i != (y + 5)) && (i != (y + 11)) && (i != (y + 17)) && (i != y) && (i != (y + 6)) && (i != (y + 12)) && (i != (y + 18)))
                    {
                        hientrang = new Hientrang_Model();
                        hientrang.vi_tri_ref = vitri;
                        hientrang.loai_du_lieu_ref = loaidulieu;
                        if (string.IsNullOrEmpty(fileExcel.Rows[i][namecolumn].ToString()))
                            hientrang.gia_tri = "";
                        else
                        {
                            hientrang.gia_tri = fileExcel.Rows[i][namecolumn].ToString();
                        }
                        lstHienTrang.Add(hientrang);
                    }
                }
            }
            //if (kiemtra == false)
            //    lstHienTrang.Clear();
            return lstHienTrang;
        }
        /// <summary>
        /// ThamNT
        /// </summary>
        /// <param name="fileExcel">Đọc dữ liệu dự kiến và nhận xét tình hình</param>
        /// <param name="x">hoành độ bắt đầu từ ô nhận xét</param>
        /// <param name="y">tung độ lấy ô nhận xét bắt đầu</param>
        /// <returns>1 list gồm 2 phần tử nhận xét, dữ kiến trong 24h</returns>
        public List<string> DuKienDienBien(DataTable fileExcel, int x, int y)
        {
            List<string> lstDukien = new List<string>();
            string nhanxet = fileExcel.Rows[y]["F" + x.ToString()].ToString();
            lstDukien.Add(nhanxet);
            nhanxet = fileExcel.Rows[y + 3]["F" + x.ToString()].ToString();
            lstDukien.Add(nhanxet);
            return lstDukien;
        }
    }
}