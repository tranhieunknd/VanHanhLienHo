﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cwrs.DataBase.VanHanhLienHo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VanHanhLienHoEntities1 : DbContext
    {
        public VanHanhLienHoEntities1()
            : base("name=VanHanhLienHoEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<danh_muc_tb> danh_muc_tb { get; set; }
        public virtual DbSet<dien_bien_tb> dien_bien_tb { get; set; }
        public virtual DbSet<don_vi_tb> don_vi_tb { get; set; }
        public virtual DbSet<hien_trang_tb> hien_trang_tb { get; set; }
        public virtual DbSet<loai_danh_muc_tb> loai_danh_muc_tb { get; set; }
        public virtual DbSet<loai_du_lieu_tb> loai_du_lieu_tb { get; set; }
        public virtual DbSet<phuong_an_detail_tb> phuong_an_detail_tb { get; set; }
        public virtual DbSet<phuong_an_tb> phuong_an_tb { get; set; }
        public virtual DbSet<thiet_lap_kich_ban_detail_tb> thiet_lap_kich_ban_detail_tb { get; set; }
        public virtual DbSet<thiet_lap_kich_ban_tb> thiet_lap_kich_ban_tb { get; set; }
        public virtual DbSet<tu_van_tb> tu_van_tb { get; set; }
        public virtual DbSet<upload_file_tb> upload_file_tb { get; set; }
        public virtual DbSet<user_tb> user_tb { get; set; }
        public virtual DbSet<vi_tri_tb> vi_tri_tb { get; set; }
        public virtual DbSet<tong_hop_tb> tong_hop_tb { get; set; }
    }
}
