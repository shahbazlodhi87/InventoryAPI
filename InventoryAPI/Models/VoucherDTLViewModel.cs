using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    public class VoucherDTLViewModel
    {
        public Nullable<int> intVNo { get; set; }
        public int intId { get; set; }
        public Nullable<int> intCategory { get; set; }
        public Nullable<int> intItemCode { get; set; }
        public Nullable<int> intPieces { get; set; }
        public Nullable<double> floatRate { get; set; }
        public Nullable<double> floatCommission { get; set; }
        public Nullable<double> floatAmount { get; set; }
        public virtual tblVoucher voucher { get; set; }
    }
}