using BonusMarket.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BonusMarket.Factories
{
    public class IdramFactory
    {
        private const string UriString = "https://web.idram.am/payment.aspx";
        private readonly string idram_id = "110000498";
        private readonly string secret_key = "E7XQBPVC9UL2YfDPkGVpgnW37D78BEpDrN4PBQ";
        public async Task<string> IdramRequest(IdramRequestPayModel model)
        {
            model.EDP_REC_ACCOUNT = idram_id;

            var parameters = new Dictionary<string, string>();
            parameters[nameof(model.EDP_AMOUNT)] = model.EDP_AMOUNT.ToString();
            parameters[nameof(model.EDP_BILL_NO)] = model.EDP_BILL_NO.ToString();
            parameters[nameof(model.EDP_DESCRIPTION)] = model.EDP_DESCRIPTION.ToString();
            parameters[nameof(model.EDP_LANGUAGE)] = model.EDP_LANGUAGE.ToString();
            parameters[nameof(model.EDP_REC_ACCOUNT)] = model.EDP_REC_ACCOUNT.ToString();
            //parameters[""] = model.EDP_AMOUNT.ToString();

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(UriString, new FormUrlEncodedContent(parameters));// PostAsJsonAsync("payment.aspx", model);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Idram error");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                return response.RequestMessage.RequestUri.ToString();
            }
        }

        public bool IsOkRequest(IdramResponsePayModel model)
        {
            return true;
            try
            {
                var idramLay = new IdramLayer();
                
                var eIdramModel = idramLay.GetByBuildId(model.EDP_BILL_NO);
                if (model.EDP_PRECHECK == "YES" && model.EDP_REC_ACCOUNT == idram_id && eIdramModel != null && Convert.ToDouble(model.EDP_AMOUNT.Replace('.',',')) == eIdramModel.Amount)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
