using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class GetList_TPNN
    {
        public Nullable<int> agriculture_code { get; set; }
        public Nullable<int> crop_ref { get; set; }
        public Nullable<float> crop_area { get; set; }
        public Nullable<float> crop_yeild { get; set; }
        public Nullable<float> crop_production { get; set; }
        public string district_name { get; set; }
        public string commune_name { get; set; }
        public int commune_ref { get; set; }
        public int district_ref { get; set; }
        public int province_ref { get; set; }
        public int id { get; set; }
    }
}
