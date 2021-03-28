using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    public class ItemViewModel
    {
        public int intCode { get; set; }
        public System.Guid barCode { get; set; }
        public string varName { get; set; }
        public string varShortName { get; set; }
        public Nullable<int> intClass { get; set; }
        public Nullable<int> intCostMethod { get; set; }
        public Nullable<int> intSupplier { get; set; }
        public Nullable<int> intPacking { get; set; }
        public Nullable<int> intPackingQuantity { get; set; }
        public Nullable<int> intPackingType { get; set; }
        public string varMinOrderLevel { get; set; }
        public string varMaxOrderLevel { get; set; }
        public string varDescription { get; set; }
        public string varOrderLevel { get; set; }
        public Nullable<int> intOrderLevelQuantity { get; set; }
        public string varLocation { get; set; }
        public Nullable<int> intWeight { get; set; }
        public Nullable<int> intWeightUnit { get; set; }
        public Nullable<int> intCategory { get; set; }
        public Nullable<int> intRegNum { get; set; }
        public string varManuBy { get; set; }
        public string varPacking { get; set; }
        public byte[] Image { get; set; }
        public Nullable<bool> bitPV { get; set; }
        public Nullable<bool> bitSV { get; set; }
        public Nullable<bool> bitDC { get; set; }
        public Nullable<bool> bitGRN { get; set; }
        public Nullable<bool> bitStockable { get; set; }
    }
}