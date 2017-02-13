using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class UserDetail
    {
        public int user_code { get; set; }

        [Required(AllowEmptyStrings =true,ErrorMessage ="Bạn phải nhập tên tài khoản")]
        public string username { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Bạn phải nhập mật khẩu")]
        [MinLength(6, ErrorMessage ="Mật khẩu phải có tối thiểu 6 kí tự")]
        public string password { get; set; }

        [NotMapped]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Bạn phải xác nhận mât khẩu")]
        [CompareAttribute("password", ErrorMessage = "Xác nhận mật khẩu không đúng")]  
        public string confirmPW { set; get; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email không đúng định dạng")]
        public string email { get; set; }
        public string mo_ta { get; set; }
        public Nullable<int> deleted_status { get; set; }
        public string row_id { get; set; }
        public Nullable<int> account_status_ref { get; set; }
        public string account_status_name { get; set; }

        public string dia_chi { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string dien_thoai { get; set; }
        public string ghi_chu { get; set; }
        public Nullable<int> don_vi_ref { get; set; }
        public string don_vi_name { get; set; }
        public string user_guid { get; set; }
    }
}
