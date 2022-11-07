using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BonusMarket.Factories;
using BonusMarket.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Controllers
{
    public class IdramController : Controller
    {

        public IActionResult PayByIdram()
        {
            // get by user id cart whole sum == EDP_AMOUNT
            // generate row with bill_no == EDP_BILL_NO
            // EDP_REC_ACCOUNT HARD
            // EDP_DESCRIPTION
            return Ok();
        }
        
        public IActionResult Success(IdramResponsePayModel model)
        {

            var idramLay = new IdramLayer();
            var m = idramLay.GetByBuildId(model.EDP_BILL_NO);
            if (m != null)
                TempData["OrderId"] = m.OrderId.ToString();

            return RedirectToAction("PayStatus", "Payment");
        }
        
        public IActionResult Fail()
        {
            return View();
        }
        
        [HttpPost]
        [Route("/idram/Result")]
        public IActionResult Result([FromForm] IdramResponsePayModel model)
        {
            try
            {
                var idramFactory = new IdramFactory();
                // first time check if EDP_PRECHECK exists 
                if (!String.IsNullOrEmpty(model.EDP_PRECHECK) && !string.IsNullOrEmpty(model.EDP_BILL_NO))
                {
                    if (idramFactory.IsOkRequest(model))
                    {
                        var idramLay = new IdramLayer();

                        var eIdramModel = idramLay.GetByBuildId(model.EDP_BILL_NO);
                        eIdramModel.IsPay = true;
                        idramLay.Update(eIdramModel);
                        return Ok("OK");
                    }
                    else
                    {
                        var idramLay = new IdramLayer();

                        var eIdramModel = idramLay.GetByBuildId(model.EDP_BILL_NO);
                        if (eIdramModel != null)
                        {
                            var orderLay = new OrderLayer();
                            orderLay.Delete(eIdramModel.OrderId);
                        }
                        return BadRequest();
                    }
                }

                if (!String.IsNullOrEmpty(model.EDP_TRANS_ID))
                {
                    return Ok("OK");
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
