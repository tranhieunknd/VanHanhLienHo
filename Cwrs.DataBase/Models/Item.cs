using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwrs.DataBase.Models
{
    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class ThucDoTLVN
    {
        public DateTime ThoiGian { get; set; }
        public int Gio { get; set; }
        public double Value { get; set; }
    }
}
