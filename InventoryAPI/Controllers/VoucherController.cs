using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryAPI.Controllers
{
    public class VoucherController : ApiController
    {
        public IHttpActionResult GetAllVouchers()
        {
            IList<VoucherViewModel> voucher = null;

            using (var ctx = new dbInventoryEntities())
            {
                voucher = ctx.tblVouchers
                            .Select(s => new VoucherViewModel()
                            {
                                intVoucherNo = s.intVoucherNo,
                                dtDate = s.dtDate,
                                intZoneName = s.intZoneName,
                                intSOName = s.intSOName                                
                            }).ToList<VoucherViewModel>();
            }

            if (voucher.Count == 0)
            {
                return NotFound();
            }

            return Ok(voucher);
        }

        public IHttpActionResult GetVoucherById(int id)
        {
           using (var ctx = new dbInventoryEntities())
            {
                List<ResultVM> resList = new List<ResultVM>();

                resList = (from vc in ctx.tblVouchers
                           join vcd in ctx.tblVoucherDTLs on vc.intVoucherNo equals vcd.intVNo                          
                           where vc.intVoucherNo == id
                           select new ResultVM
                           {
                               intId = vcd.intId,
                               intVoucherNo = vc.intVoucherNo,
                               dtDate = vc.dtDate,
                               intZoneName = vc.intZoneName,
                               intSOName = vc.intSOName,
                               intCategory = vcd.intCategory,
                               intItemCode = vcd.intItemCode,
                               intPieces = vcd.intPieces,
                               floatRate = vcd.floatRate,
                               floatCommission = vcd.floatCommission,
                               floatAmount = vcd.floatAmount
                           }).OrderByDescending(x => x.intVoucherNo).ToList();

                return Ok(resList);                
            }           
        }

        public IHttpActionResult PostNewVoucher(VoucherViewModel voucher)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Invalid data.");

            using (var ctx = new dbInventoryEntities())
            {
                int _intVoucherNo = 1;
                var obj = ctx.tblVouchers.OrderByDescending(t => t.intVoucherNo).FirstOrDefault();
                if (obj != null)
                {
                    _intVoucherNo = obj.intVoucherNo + 1;
                }

                ctx.tblVouchers.Add(new tblVoucher()
                {
                    intVoucherNo = _intVoucherNo,
                    dtDate = voucher.dtDate,
                    intZoneName = voucher.intZoneName,
                    intSOName = voucher.intSOName
                });
                int _intId = 1;
                var obj1 = ctx.tblVoucherDTLs.OrderByDescending(t => t.intId).FirstOrDefault();
                if (obj1 != null)
                {
                    _intId = obj1.intId + 1;
                }
                foreach (var detail in voucher.voucherDTL)
                {
                    var detailVoucher = new tblVoucherDTL
                    {
                        intId = _intId,
                        intCategory = detail.intCategory,
                        intVNo = _intVoucherNo,
                        intItemCode = detail.intItemCode,
                        intPieces = detail.intPieces,
                        floatRate = detail.floatRate,
                        floatCommission = detail.floatCommission,
                        floatAmount = detail.floatAmount
                    };
                    ctx.tblVoucherDTLs.Add(detailVoucher);
                    _intId++;
                }
                try
                {
                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }

            return Ok();
        }

        public IHttpActionResult Put(VoucherViewModel voucher)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Not a valid model");

            using (var ctx = new dbInventoryEntities())
            {
                try
                {
                    var existingVoucher = ctx.tblVouchers.Where(s => s.intVoucherNo == voucher.intVoucherNo)
                                                        .FirstOrDefault<tblVoucher>();

                    if (existingVoucher != null)
                    {
                        //existingVoucher.dtDate = voucher.dtDate;
                        existingVoucher.intZoneName = voucher.intZoneName;
                        existingVoucher.intSOName = voucher.intSOName;

                        ctx.SaveChanges();
                    }

                    int id = 0;
                    foreach (var detail in voucher.voucherDTL)
                    {                        
                        int id1 = voucher.voucherDTL[id].intId;
                        var existingVoucherDTL = ctx.tblVoucherDTLs.Where(s => s.intId == id1)
                                                    .FirstOrDefault<tblVoucherDTL>();
                        if (existingVoucherDTL != null)
                        {
                            existingVoucherDTL.intCategory = detail.intCategory;
                            existingVoucherDTL.intItemCode = detail.intItemCode;
                            existingVoucherDTL.intPieces = detail.intPieces;
                            existingVoucherDTL.floatRate = detail.floatRate;
                            existingVoucherDTL.floatCommission = detail.floatCommission;
                            existingVoucherDTL.floatAmount = detail.floatAmount;

                            ctx.SaveChanges();
                        }
                        id++;
                    }

                    int _intId = 1;
                        var obj1 = ctx.tblVoucherDTLs.OrderByDescending(t => t.intId).FirstOrDefault();
                        if (obj1 != null)
                        {
                            _intId = obj1.intId + 1;
                        }
                        foreach (var detail1 in voucher.voucherDTL1)
                        {
                            var detailVoucher1 = new tblVoucherDTL
                            {
                                intId = _intId,
                                intCategory = detail1.intCategory,
                                intVNo = voucher.intVoucherNo,
                                intItemCode = detail1.intItemCode,
                                intPieces = detail1.intPieces,
                                floatRate = detail1.floatRate,
                                floatCommission = detail1.floatCommission,
                                floatAmount = detail1.floatAmount
                            };
                            ctx.tblVoucherDTLs.Add(detailVoucher1);
                            _intId++;                           

                            ctx.SaveChanges();                            
                        }
                    //else
                    //{
                    //    return NotFound();
                    //}
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

            return Ok();
        }

        public IHttpActionResult DeleteVoucher(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Voucher ID");

            using (var ctx = new dbInventoryEntities())
            {
                var voucher = ctx.tblVouchers
                    .Where(s => s.intVoucherNo == id)
                    .FirstOrDefault();
                ctx.Entry(voucher).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();

                var voucherDTL = ctx.tblVoucherDTLs
                    .Where(s => s.intVNo == id)
                    .ToList();
                ctx.tblVoucherDTLs.RemoveRange(voucherDTL);
                //foreach (var item in voucherDTL)
                //{
                //    ctx.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                //    ctx.SaveChanges();
                //}
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/voucher/voucherid")]
        public int VoucherId()
        {
            //var obj;
            using (var ctx = new dbInventoryEntities())
            {
                var obj = ctx.tblVouchers.Max(t => t.intVoucherNo);
                if (obj == 0)
                {
                    return obj;
                }
                //int id = Convert.ToInt32(obj);
                //return Ok(obj);
                else
                {
                    return obj + 1;
                }
            }

        }


    }
}
