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
    public class VoucherDTLController : ApiController
    {
        public IHttpActionResult GetAllVouchersDTL()
        {
            IList<VoucherDTLViewModel> voucherDTL = null;

            using (var ctx = new dbInventoryEntities())
            {
                voucherDTL = ctx.tblVoucherDTLs
                            .Select(s => new VoucherDTLViewModel()
                            {
                                intVNo = s.intVNo,
                                intId = s.intId,
                                intCategory = s.intCategory,
                                intItemCode = s.intItemCode,
                                intPieces = s.intPieces,
                                floatRate = s.floatRate,
                                floatCommission = s.floatCommission,
                                floatAmount = s.floatAmount
                            }).ToList<VoucherDTLViewModel>();
            }

            if (voucherDTL.Count == 0)
            {
                return NotFound();
            }

            return Ok(voucherDTL);
        }

        public IHttpActionResult GetVoucherDTLById(int id)
        {
            VoucherDTLViewModel voucherDTL = null;

            using (var ctx = new dbInventoryEntities())
            {
                voucherDTL = ctx.tblVoucherDTLs
                    .Where(s => s.intId == id)
                    .Select(s => new VoucherDTLViewModel()
                    {
                        intVNo = s.intVNo,
                        intId = s.intId,
                        intCategory = s.intCategory,
                        intItemCode = s.intItemCode,
                        intPieces = s.intPieces,
                        floatRate = s.floatRate,
                        floatCommission = s.floatCommission,
                        floatAmount = s.floatAmount
                    }).FirstOrDefault<VoucherDTLViewModel>();
            }

            if (voucherDTL == null)
            {
                return NotFound();
            }

            return Ok(voucherDTL);
        }

        public IHttpActionResult PostNewVoucherDTL(VoucherDTLViewModel voucherDTL)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Invalid data.");

            using (var ctx = new dbInventoryEntities())
            {
                int _intId = 1;
                var obj = ctx.tblVoucherDTLs.OrderByDescending(t => t.intId).FirstOrDefault();
                if (obj != null)
                {
                    _intId = obj.intId + 1;
                }

                ctx.tblVoucherDTLs.Add(new tblVoucherDTL()
                {
                    intVNo = voucherDTL.intVNo,
                    intId = _intId,
                    intCategory = voucherDTL.intCategory,
                    intItemCode = voucherDTL.intItemCode,
                    intPieces = voucherDTL.intPieces,
                    floatRate = voucherDTL.floatRate,
                    floatCommission = voucherDTL.floatCommission,
                    floatAmount = voucherDTL.floatCommission
                });
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

        public IHttpActionResult Put(VoucherDTLViewModel voucherDTL)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new dbInventoryEntities())
            {
                try
                {
                    var existingVoucherDTL = ctx.tblVoucherDTLs.Where(s => s.intId == voucherDTL.intId)
                                                        .FirstOrDefault<tblVoucherDTL>();

                    if (existingVoucherDTL != null)
                    {
                        existingVoucherDTL.intCategory = voucherDTL.intCategory;
                        existingVoucherDTL.intItemCode = voucherDTL.intItemCode;
                        existingVoucherDTL.intPieces = voucherDTL.intPieces;
                        existingVoucherDTL.floatRate = voucherDTL.floatRate;
                        existingVoucherDTL.floatCommission = voucherDTL.floatCommission;
                        existingVoucherDTL.floatAmount = voucherDTL.floatAmount;


                        ctx.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
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

        public IHttpActionResult DeleteVoucherDTL(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid VoucherDTL ID");

            using (var ctx = new dbInventoryEntities())
            {
                var voucherDTL = ctx.tblVoucherDTLs
                    .Where(s => s.intId == id)
                    .FirstOrDefault();
                ctx.Entry(voucherDTL).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        [Route("api/voucherdtl/del/{id}")]
        public IHttpActionResult DeleteVoucherDTLArr(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Voucher ID");

            using (var ctx = new dbInventoryEntities())
            {
                var voucherDTL = ctx.tblVoucherDTLs
                    .Where(s => s.intVNo == id)
                    .ToList();
                foreach (var item in voucherDTL)
                {
                    ctx.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }
                
            }

            return Ok();
        }


    }
}
