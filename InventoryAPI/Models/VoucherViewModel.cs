using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    public class VoucherViewModel
    {
        public int intVoucherNo { get; set; }
        public Nullable<System.DateTime> dtDate { get; set; }
        public Nullable<int> intZoneName { get; set; }
        public Nullable<int> intSOName { get; set; }
        public virtual List<tblVoucherDTL> voucherDTL { get; set; }
        public virtual List<tblVoucherDTL> voucherDTL1 { get; set; }

    }
}