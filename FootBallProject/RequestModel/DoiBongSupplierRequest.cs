using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.RequestModel
{
    public class DoiBongSupplierRequest
    {
        public string idDoiBong { get; set; }
        public int idSupplier { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<int> duration { get; set; }
        public Nullable<int> status { get; set; }
      
        public virtual DOIBONG DOIBONG { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
    }
}
