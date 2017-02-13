using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class GetList_KTXH
    {
        public int socioeconomic_resettlement_code { get; set; }
        public string district_name { get; set; }
        public string commune_name { get; set; }
        public Nullable<float> natural_area { get; set; }
        public Nullable<int> male_number { get; set; }
        public Nullable<int> female_number { get; set; }
        public Nullable<int> household_number { get; set; }
        public Nullable<int> village_number { get; set; }
        public Nullable<int> agriculture_household_number { get; set; }
        public Nullable<int> poor_household_number { get; set; }
        public Nullable<int> pro_poor_household_number { get; set; }
        public Nullable<double> poverty_percent { get; set; }
        public Nullable<float> cultivated_area { get; set; }
        public Nullable<float> irrigated_area_rice { get; set; }
        public Nullable<float> irrigated_area_upland_crop { get; set; }
        public Nullable<float> irrigated_area_rice_project { get; set; }
        public Nullable<float> irrigated_area_upland_crop_project { get; set; }
        public Nullable<float> minority_household_number { get; set; }
        public Nullable<float> permanently_acquired_house_area { get; set; }
        public Nullable<float> permanently_acquired_aquaculture_area { get; set; }
        public Nullable<float> permanently_acquired_forestry_area { get; set; }
        public Nullable<float> permanently_acquired_other_area { get; set; }
        public Nullable<int> house_effected_number { get; set; }
        public Nullable<int> other_work_effected_number { get; set; }
        public Nullable<int> crop_effected_number { get; set; }
        public Nullable<int> tomb_effected_number { get; set; }
        public Nullable<int> househole_slight_effected_number { get; set; }
        public Nullable<int> household_seriously_effected_number { get; set; }
        public Nullable<int> household_resettled_effected_number { get; set; }
        public Nullable<int> household_business_effected_number { get; set; }
        public Nullable<int> household_poor_effected_number { get; set; }
        public Nullable<int> household_other_effected { get; set; }
        public string remark { get; set; }
    }
}
