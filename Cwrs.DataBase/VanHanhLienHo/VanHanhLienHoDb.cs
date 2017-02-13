using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.Core.Data;
using Cwrs.Data;
using System.Reflection;
namespace Cwrs.DataBase.VanHanhLienHo
{
    public class VanHanhLienHoDb
    {
        public VanHanhLienHoDb() { }

        internal VanHanhLienHoEntities1 _databaseContext = new VanHanhLienHoEntities1();
        
        ///<summary>
        /// IRepository for user_tb.
        ///</summary>
        public IRepository<user_tb> user_tbs
        {
            get { return new EfRepository<user_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for dien_bien_tb.
        ///</summary>
        public IRepository<dien_bien_tb> dien_bien_tbs
        {
            get { return new EfRepository<dien_bien_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for don_vi_tb.
        ///</summary>
        public IRepository<don_vi_tb> don_vi_tbs
        {
            get { return new EfRepository<don_vi_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for vi_tri_tb.
        ///</summary>
        public IRepository<vi_tri_tb> vi_tri_tbs
        {
            get { return new EfRepository<vi_tri_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for hien_trang_tb.
        ///</summary>
        public IRepository<hien_trang_tb> hien_trang_tbs
        {
            get { return new EfRepository<hien_trang_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for loai_du_lieu_tb.
        ///</summary>
        public IRepository<loai_du_lieu_tb> loai_du_lieu_tbs
        {
            get { return new EfRepository<loai_du_lieu_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for phuong_an_tb.
        ///</summary>
        public IRepository<phuong_an_tb> phuong_an_tbs
        {
            get { return new EfRepository<phuong_an_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for phuong_an_detail_tb.
        ///</summary>
        public IRepository<phuong_an_detail_tb> phuong_an_detail_tbs
        {
            get { return new EfRepository<phuong_an_detail_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for tu_van_tb.
        ///</summary>
        public IRepository<tu_van_tb> tu_van_tbs
        {
            get { return new EfRepository<tu_van_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for danh_muc_tb.
        ///</summary>
        public IRepository<danh_muc_tb> danh_muc_tbs
        {
            get { return new EfRepository<danh_muc_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for loai_danh_muc_tb.
        ///</summary>
        public IRepository<loai_danh_muc_tb> loai_danh_muc_tbs
        {
            get { return new EfRepository<loai_danh_muc_tb>(_databaseContext); }
        }

        ///<summary>
        /// IRepository for upload_file_tb.
        ///</summary>
        public IRepository<upload_file_tb> upload_file_tbs
        {
            get { return new EfRepository<upload_file_tb>(_databaseContext); }
        }
    }
}
