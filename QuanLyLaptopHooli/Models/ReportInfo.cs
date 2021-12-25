using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyLaptopHooli.Models
{
    public class ReportInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Sum { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> Avg { get; set; }
        public Nullable<int> Max { get; set; }
        public Nullable<int> Min { get; set; }
    }
}