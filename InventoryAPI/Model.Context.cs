//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbInventoryEntities : DbContext
    {
        public dbInventoryEntities()
            : base("name=dbInventoryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblItem> tblItems { get; set; }
        public virtual DbSet<tblVoucher> tblVouchers { get; set; }
        public virtual DbSet<tblVoucherDTL> tblVoucherDTLs { get; set; }
    }
}
