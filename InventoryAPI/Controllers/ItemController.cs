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
    public class ItemController : ApiController
    {
        
        // GET api/<controller>
        public IHttpActionResult GetAllItems()
        {
            IList<ItemViewModel> item = null;

            using (var ctx = new dbInventoryEntities())
            {
                item = ctx.tblItems
                            .Select(s => new ItemViewModel()
                            {
                                intCode = s.intCode,
                                barCode = s.barCode,
                                varName = s.varName,
                                varShortName = s.varShortName,
                                intClass = s.intCategory,
                                intCostMethod = s.intCostMethod,
                                intSupplier = s.intSupplier,
                                intPacking = s.intPacking,
                                intPackingQuantity = s.intPackingQuantity,
                                intPackingType = s.intPackingType,
                                varMinOrderLevel = s.varMinOrderLevel,
                                varMaxOrderLevel = s.varMaxOrderLevel,
                                varDescription = s.varDescription,
                                varOrderLevel = s.varOrderLevel,
                                intOrderLevelQuantity = s.intOrderLevelQuantity,
                                varLocation = s.varLocation,
                                intWeight = s.intWeight,
                                intWeightUnit = s.intWeightUnit,
                                intCategory = s.intCategory,
                                intRegNum = s.intRegNum,
                                varManuBy = s.varManuBy,
                                varPacking = s.varPacking,
                                Image = s.Image,
                                bitPV = s.bitPV,
                                bitSV = s.bitSV,
                                bitDC = s.bitDC,
                                bitGRN = s.bitGRN,
                                bitStockable = s.bitStockable
                            }).ToList<ItemViewModel>();
            }

            if (item.Count == 0)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // GET api/<controller>/5
        public IHttpActionResult GetItemById(int id)
        {
            ItemViewModel item = null;

            using (var ctx = new dbInventoryEntities())
            {
                item = ctx.tblItems
                    .Where(s => s.intCode == id)
                    .Select(s => new ItemViewModel()
                    {
                        intCode = s.intCode,
                        barCode = s.barCode,
                        varName = s.varName,
                        varShortName = s.varShortName,
                        intClass = s.intCategory,
                        intCostMethod = s.intCostMethod,
                        intSupplier = s.intSupplier,
                        intPacking = s.intPacking,
                        intPackingQuantity = s.intPackingQuantity,
                        intPackingType = s.intPackingType,
                        varMinOrderLevel = s.varMinOrderLevel,
                        varMaxOrderLevel = s.varMaxOrderLevel,
                        varDescription = s.varDescription,
                        varOrderLevel = s.varOrderLevel,
                        intOrderLevelQuantity = s.intOrderLevelQuantity,
                        varLocation = s.varLocation,
                        intWeight = s.intWeight,
                        intWeightUnit = s.intWeightUnit,
                        intCategory = s.intCategory,
                        intRegNum = s.intRegNum,
                        varManuBy = s.varManuBy,
                        varPacking = s.varPacking,
                        Image = s.Image,
                        bitPV = s.bitPV,
                        bitSV = s.bitSV,
                        bitDC = s.bitDC,
                        bitGRN = s.bitGRN,
                        bitStockable = s.bitStockable
                    }).FirstOrDefault<ItemViewModel>();
            }

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<controller>
        public IHttpActionResult PostNewItem(ItemViewModel item)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Invalid data.");

            using (var ctx = new dbInventoryEntities())
            {
                int _intCode = 1;
                var obj = ctx.tblItems.OrderByDescending(t => t.intCode).FirstOrDefault();
                if (obj != null)
                {
                    _intCode = obj.intCode + 1;
                }
                
                ctx.tblItems.Add(new tblItem()
                {
                    intCode = item.intCode,
                    barCode = item.barCode,
                    varName = item.varName,
                    varShortName = item.varShortName,
                    intClass = item.intCategory,
                    intCostMethod = item.intCostMethod,
                    intSupplier = item.intSupplier,
                    intPacking = item.intPacking,
                    intPackingQuantity = item.intPackingQuantity,
                    intPackingType = item.intPackingType,
                    varMinOrderLevel = item.varMinOrderLevel,
                    varMaxOrderLevel = item.varMaxOrderLevel,
                    varDescription = item.varDescription,
                    varOrderLevel = item.varOrderLevel,
                    intOrderLevelQuantity = item.intOrderLevelQuantity,
                    varLocation = item.varLocation,
                    intWeight = item.intWeight,
                    intWeightUnit = item.intWeightUnit,
                    intCategory = item.intCategory,
                    intRegNum = item.intRegNum,
                    varManuBy = item.varManuBy,
                    varPacking = item.varPacking,
                    Image = item.Image,
                    bitPV = item.bitPV,
                    bitSV = item.bitSV,
                    bitDC = item.bitDC,
                    bitGRN = item.bitGRN,
                    bitStockable = item.bitStockable
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

        // PUT api/<controller>/5
        public IHttpActionResult Put(ItemViewModel item)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new dbInventoryEntities())
            {
                try
                {
                    var existingItem = ctx.tblItems.Where(s => s.intCode == item.intCode)
                                                        .FirstOrDefault<tblItem>();

                    if (existingItem != null)
                    {
                        existingItem.intCode = item.intCode;
                        existingItem.barCode = item.barCode;
                        existingItem.varName = item.varName;
                        existingItem.varShortName = item.varShortName;
                        existingItem.intClass = item.intCategory;
                        existingItem.intCostMethod = item.intCostMethod;
                        existingItem.intSupplier = item.intSupplier;
                        existingItem.intPacking = item.intPacking;
                        existingItem.intPackingQuantity = item.intPackingQuantity;
                        existingItem.intPackingType = item.intPackingType;
                        existingItem.varMinOrderLevel = item.varMinOrderLevel;
                        existingItem.varMaxOrderLevel = item.varMaxOrderLevel;
                        existingItem.varDescription = item.varDescription;
                        existingItem.varOrderLevel = item.varOrderLevel;
                        existingItem.intOrderLevelQuantity = item.intOrderLevelQuantity;
                        existingItem.varLocation = item.varLocation;
                        existingItem.intWeight = item.intWeight;
                        existingItem.intWeightUnit = item.intWeightUnit;
                        existingItem.intCategory = item.intCategory;
                        existingItem.intRegNum = item.intRegNum;
                        existingItem.varManuBy = item.varManuBy;
                        existingItem.varPacking = item.varPacking;
                        existingItem.Image = item.Image;
                        existingItem.bitPV = item.bitPV;
                        existingItem.bitSV = item.bitSV;
                        existingItem.bitDC = item.bitDC;
                        existingItem.bitGRN = item.bitGRN;
                        existingItem.bitStockable = item.bitStockable;

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

        // DELETE api/<controller>/5
        public IHttpActionResult DeleteItem(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Item ID");

            using (var ctx = new dbInventoryEntities())
            {
                var item = ctx.tblItems
                    .Where(s => s.intCode == id)
                    .FirstOrDefault();
                ctx.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/item/itemid")]
        public int ItemId()
        {
            //var obj;
            using (var ctx = new dbInventoryEntities())
            {
                var obj = ctx.tblItems.Max(t => t.intCode);
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