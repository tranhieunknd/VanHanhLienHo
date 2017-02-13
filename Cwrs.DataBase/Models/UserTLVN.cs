using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cwrs.DataBase.Models
{
    public class UserTLVN
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string RoleRef { get; set; }
        public Nullable<bool> IsActiveBitType { get; set; }
        public string DiaChi { get; set; }
        public string GroupRef { get; set; }
        public Nullable<bool> IsActiveEmailBitType { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> IsAdminBitType { get; set; }
        public Nullable<bool> IsActiveCameraBitType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<int> DeletedStatus { get; set; }
        public string MatKhauSms { get; set; }
        public Nullable<int> NumOfDayDisplay { get; set; }
        public Nullable<int> NumOfDayEdit { get; set; }
        public string HourDisplay { get; set; }
        public Nullable<int> CapNhatDuLieuType { get; set; }
        public Nullable<int> QuyenCapNhatCongTrinh { get; set; }
        public Nullable<int> QuyenCapNhatDiemQuanTrac { get; set; }
        public string UserGuid { get; set; }

    }
}