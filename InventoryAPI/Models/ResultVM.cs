using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    public class ResultVM
    {
        public int intId { get; set; }
        public int intVoucherNo { get; set; }
        public Nullable<System.DateTime> dtDate { get; set; }
        public Nullable<int> intZoneName { get; set; }
        public Nullable<int> intSOName { get; set; }
        public Nullable<int> intCategory { get; set; }
        public Nullable<int> intItemCode { get; set; }
        public Nullable<int> intPieces { get; set; }
        public Nullable<double> floatRate { get; set; }
        public Nullable<double> floatCommission { get; set; }
        public Nullable<double> floatAmount { get; set; }
    }
}