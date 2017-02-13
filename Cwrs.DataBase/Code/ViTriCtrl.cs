using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwrs.DataBase.VanHanhLienHo;
using Cwrs.DataBase.Models;

namespace Cwrs.DataBase.Code
{
    public class ViTriCtrl : Common
    {
        public List<vi_tri_tb> result = new List<vi_tri_tb>();
        public List<vi_tri_tb> GetViTri()
        {
            var x = from u in _database._databaseContext.vi_tri_tb
                    where u.vi_tri_type == 3
                    select u;
            result = x.ToList();
            return result;
        }
    }
    
}