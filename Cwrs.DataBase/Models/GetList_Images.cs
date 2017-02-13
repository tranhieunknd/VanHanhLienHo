using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class GetList_Images
    {
        public Nullable<int> structure_file_code { get; set; }
        public string files_name { get; set; }
        public string link { get; set; }
        public Nullable<int> album_code { get; set; }
        public string album_name { get; set; }
        public Nullable<int> structure_code_ref { get; set; }
        public string structure_name { get; set; }
        public string description { get; set; }
        public string epitome { get; set; }
        public Nullable<System.DateTime> getimages_day { get; set; }
    }
}