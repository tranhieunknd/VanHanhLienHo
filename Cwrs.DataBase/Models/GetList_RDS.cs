using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class GetList_RDS
    {
        public int wua_code { get; set; }
        public string wua_name { get; set; }
        public string wug_name { get; set; }
        public Nullable<float> area_tuoikenh { get; set; }
        public Nullable<float> area_tuoicong { get; set; }
        public Nullable<int> development_stage_ref { get; set; }
        public Nullable<int> tertiary_unit_ref { get; set; }
        public string canal_name { get; set; }
        public string culvert_name { get; set; }
        public string district_name { get; set; }
        public string commune_name { get; set; }
        public int wug_code_ref { get; set; }
    }
}
