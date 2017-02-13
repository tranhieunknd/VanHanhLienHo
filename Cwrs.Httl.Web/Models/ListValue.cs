using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cwrs.Httl.Web.Models
{
    public class ListValue
    {
        public int socioeconomic_resettlement_code { get; set; }
        public int agriculture_code { get; set; }
        public int wua_code { get; set; }
        public int wug_code_ref { get; set; }
        public int crop_ref { get; set; }
        public string value_name { get; set; }
        public int value_number { get; set; }
        public string value_text { get; set; }
        public string huyen { get; set; }
        public string xa { get; set; }
        public int dvql { get; set; }
        public string thoigian { get; set; }
        public string tendanhmuc { get; set; }
        public string tendanhmuc1 { get; set; }
        public string tencong { get; set; }
        public string tenkenh { get; set; }
    }
}