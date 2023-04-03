using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using AmeriaPay;
using BonusMarket.Factories;
using BonusMarket.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BonusMarket.Controllers
{
    public class PaymentController : BaseController
    {

        private IHostingEnvironment _env;
        public PaymentController(IHostingEnvironment env)
        {
            _env = env;
        }
        [Route("test/pay/online")]
        [HttpPost]
        public IActionResult OnlinePay(OrderEntityViewModel model)
        {
            if (model.OrderedProducts == null || !model.OrderedProducts.Any())
            {
                return Json(new { error = "Խնդրում ենք լրացնել պարտադիր դաշտերը" });
            }
            if (!User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(model.Address) || string.IsNullOrWhiteSpace(model.FirstName))
                {
                    return Json(new { error = "Խնդրում ենք լրացնել պարտադիր դաշտերը" });
                }
                else
                if (string.IsNullOrEmpty(model.LastName) || string.IsNullOrWhiteSpace(model.Email))
                {
                    return Json(new { error = "Խնդրում ենք լրացնել պարտադիր դաշտերը" });
                }
                else
                if (string.IsNullOrEmpty(model.Phone))
                {
                    return Json(new { error = "Խնդրում ենք լրացնել պարտադիր դաշտերը" });
                }
                else
                {
                    HttpContext.Session.SetString("buyerr", "");
                }
            }
            else
            {
                List<string> slist = new List<string>();
                slist.Add(model.FirstName);
                slist.Add(model.LastName);
                slist.Add(model.Address);
                slist.Add(model.Email);
                slist.Add(model.Phone);
                bool flag1 = false;
                bool flag2 = false;
                foreach (var item in slist)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        flag1 = true;
                    }
                    if (!string.IsNullOrEmpty(item))
                    {
                        flag2 = true;
                    }
                }

                if (flag1 && flag2)
                {
                    return Json(new { error = "Խնդրում ենք լրացնել պարտադիր դաշտերը" });
                }
                if (!string.IsNullOrEmpty(model.LastName) && !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Phone)
                    && !string.IsNullOrEmpty(model.Address) && !string.IsNullOrEmpty(model.FirstName))
                {
                    HttpContext.Session.SetString("buyerr", "");
                }
            }

            ProductLayer ly = new ProductLayer();

            List<ProductEditViewModel> plist = new List<ProductEditViewModel>();
            List<int?> idlist = model.OrderedProducts.Select(x => x.Id).ToList();
            UserEntity us = new UserEntity();
            model.UserName = null;
            if (User.Identity.IsAuthenticated)
            {
                model.UserName = User.Identity.Name;
            }
            plist = ly.GetProductPrices(idlist);

            if (plist.Any(n => n.Count < model.OrderedProducts.Find(k => k.Id == n.Id).Count || n.Price == 0))
            {
                return Json(new { error = ("buyerr", plist.Find(n => n.Count < model.OrderedProducts.Find(k => k.Id == n.Id).Count).Sku + " :Հետևյալ Կոդ ունեցող ապրանքի քանակը քիչ է ձեր ցանկացածից") });
            }
            else
            {
                HttpContext.Session.SetString("buyerr", "");
            }
            decimal? validTotalMoney = 0;
            foreach (var item in plist)
            {
                validTotalMoney += item.Price * model.OrderedProducts.Find(m => m.Id == item.Id).Count;
            }
            if (validTotalMoney < 5000)
            {
                validTotalMoney = validTotalMoney + 1000;
            }
            model.TotalMoney = (int)validTotalMoney;

            foreach (var item in model.OrderedProducts)
            {
                var exists = plist.Where(r => r.Id == item.Id).Any();
                if (exists)
                {
                    item.Price = plist.Where(r => r.Id == item.Id).ToList()[0].Price;
                }
            }
            //validation
            var orderId = ly.InsertOrder(model, model.UserName);
            var idramL = new IdramLayer();
            var idramModer = new IdramPay()
            {
                Amount = model.TotalMoney,
                CreationDate = DateTime.Now,
                IsPay = false,
                BillId = Guid.NewGuid().ToString(),
                OrderId = orderId.Value
            };
            idramL.Create(idramModer);

            return Json(idramModer);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(OrderEntityViewModel model)
        {
            if (model.OrderedProducts == null || !model.OrderedProducts.Any())
            {
                return RedirectToAction("Basket", "Home", new { haveError = true });
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            if (!User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(model.Address) || string.IsNullOrWhiteSpace(model.FirstName))
                {
                    HttpContext.Session.SetString("buyerr", "Խնդրում ենք լրացնել պարտադիր դաշտերը");
                    return RedirectToAction("Basket", "Home");
                }
                else
                if (string.IsNullOrEmpty(model.LastName) || string.IsNullOrWhiteSpace(model.Email))
                {
                    HttpContext.Session.SetString("buyerr", "Խնդրում ենք լրացնել պարտադիր դաշտերը");
                    return RedirectToAction("Basket", "Home");
                }
                else
                if (string.IsNullOrEmpty(model.Phone))
                {
                    HttpContext.Session.SetString("buyerr", "Խնդրում ենք լրացնել պարտադիր դաշտերը");
                    return RedirectToAction("Basket", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("buyerr", "");
                }
            }
            else
            {
                List<string> slist = new List<string>();
                slist.Add(model.FirstName);
                slist.Add(model.LastName);
                slist.Add(model.Address);
                slist.Add(model.Email);
                slist.Add(model.Phone);
                bool flag1 = false;
                bool flag2 = false;
                foreach (var item in slist)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        flag1 = true;
                    }
                    if (!string.IsNullOrEmpty(item))
                    {
                        flag2 = true;
                    }
                }

                if (flag1 && flag2)
                {
                    HttpContext.Session.SetString("buyerr", "Եթե ցանկանում եք առաքել այլ հասցեով խնդրում ենք լրանել պարտադիր դաշտերը");
                    return RedirectToAction("Basket", "Home");
                }
                if (!string.IsNullOrEmpty(model.LastName) && !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Phone)
                    && !string.IsNullOrEmpty(model.Address) && !string.IsNullOrEmpty(model.FirstName))
                {
                    HttpContext.Session.SetString("buyerr", "");
                }
            }

            string url = "";
            ProductLayer ly = new ProductLayer();

            List<ProductEditViewModel> plist = new List<ProductEditViewModel>();
            List<int?> idlist = model.OrderedProducts.Select(x => x.Id).ToList();
            UserEntity us = new UserEntity();
            model.UserName = null;
            if (User.Identity.IsAuthenticated)
            {
                model.UserName = User.Identity.Name;
            }
            plist = ly.GetProductPrices(idlist);

            if (plist.Any(n => n.Count < model.OrderedProducts.Find(k => k.Id == n.Id).Count || n.Price == 0))
            {
                HttpContext.Session.SetString("buyerr", plist.Find(n => n.Count < model.OrderedProducts.Find(k => k.Id == n.Id).Count).Sku + " :Հետևյալ Կոդ ունեցող ապրանքի քանակը քիչ է ձեր ցանկացածից");
                return RedirectToAction("Basket", "Home");
            }
            else
            {
                HttpContext.Session.SetString("buyerr", "");
            }
            decimal? validTotalMoney = 0;
            foreach (var item in plist)
            {
                validTotalMoney += item.Price * model.OrderedProducts.Find(m => m.Id == item.Id).Count;
            }
            if (validTotalMoney < 5000)
            {
                validTotalMoney = validTotalMoney + 1000;
            }
            model.TotalMoney = (int)validTotalMoney;


            //validation
            if (model.PaymentMethod)
            {


                //create new bill
                //make api call to idram with new bill id
                ////catch response in  Idram/Result
                #region Ameria

                //AmeriaClient cl = new AmeriaClient();
                //try
                //{

                //    cl.OpenAsync();
                //    PaymentClientClass paymentIdfields = new PaymentClientClass();
                //    paymentIdfields.ClientID = "1c5bccc8-8638-44d8-8428-f6528c8aee06";
                //    paymentIdfields.Username = "19533087_api";
                //    paymentIdfields.Password = "a7KWUMqC5v3l9jW";
                //    paymentIdfields.Description = "Bonus Market";
                //    paymentIdfields.OrderID = int.Parse(DateTime.Now.Year.ToString()[2] + DateTime.Now.Year.ToString()[3] + DateTime.Now.Hour.ToString() + DateTime.Now.Minute + DateTime.Now.Millisecond.ToString());
                //    paymentIdfields.Opaque = "Opaque" + paymentIdfields.OrderID;
                //    paymentIdfields.PaymentAmount = model.TotalMoney;
                //    paymentIdfields.backURL = "https://urartuonline.am/Payment/PayStatus";
                //    model.BankOrderId = paymentIdfields.OrderID;
                //    XDocument xdoc = new XDocument(new XElement("GetPaymentIDData",
                //        new XElement("ClientID", paymentIdfields.ClientID),
                //        new XElement("Username", paymentIdfields.Username),
                //        new XElement("Password", paymentIdfields.Password),
                //        new XElement("Description", paymentIdfields.Description),
                //        new XElement("OrderID", paymentIdfields.OrderID),
                //        new XElement("Opaque", paymentIdfields.Opaque),
                //        new XElement("PaymentAmount", paymentIdfields.PaymentAmount),
                //        new XElement("backURL", paymentIdfields.backURL)
                //        ));
                //    string tempxl = ToXML(model);
                //    System.IO.File.WriteAllText(_env.WebRootPath + "\\tmpxml\\" + paymentIdfields.OrderID + ".txt", tempxl);

                //    string path = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString();

                //    var webRoot = _env.WebRootPath;
                //    if (!System.IO.Directory.Exists(webRoot + "\\" + DateTime.Now.Year.ToString()))
                //    {
                //        System.IO.Directory.CreateDirectory(webRoot + "\\" + DateTime.Now.Year.ToString());
                //    }
                //    if (!System.IO.Directory.Exists(webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                //    {
                //        System.IO.Directory.CreateDirectory(webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());
                //    }
                //    if (!System.IO.Directory.Exists(webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString()))
                //    {
                //        System.IO.Directory.CreateDirectory(webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString());
                //    }
                //    if (System.IO.Directory.Exists(webRoot + "\\" + path))
                //    {
                //        xdoc.Save(webRoot + "\\" + path + "\\" + paymentIdfields.OrderID.ToString() + ".xml");
                //    }

                //    var result = cl.GetPaymentIDAsync(paymentIdfields).Result;
                //    url = "https://payments.ameriabank.am/forms/frm_paymentstype.aspx?clientid=" + paymentIdfields.ClientID +
                //              "&clienturl=https://urartuonline.am/ameriarequestframe.aspx&lang=am&paymentid=" + result.PaymentID;
                //    cl.CloseAsync();
                //}
                //catch (Exception)
                //{

                //    throw new Exception("Ծրագրային սխալ");
                //}
                //finally
                //{
                //    if (cl != null)
                //    {

                //        cl.CloseAsync();


                //    }
                //}
                #endregion
                var orderId = ly.InsertOrder(model, model.UserName);
                var idramL = new IdramLayer();
                var idramModer = new IdramPay()
                {
                    Amount = model.TotalMoney,
                    CreationDate = DateTime.Now,
                    IsPay = false,
                    BillId = Guid.NewGuid().ToString(),
                    OrderId = orderId.Value
                };
                idramL.Create(idramModer);
                IdramFactory ifactory = new IdramFactory();
                url = await ifactory.IdramRequest(new IdramRequestPayModel()
                {
                    EDP_AMOUNT = idramModer.Amount,
                    EDP_BILL_NO = idramModer.BillId,
                    EDP_DESCRIPTION = "",
                    EDP_LANGUAGE = "AM"
                });
            }
            else
            {
                var ressult = ly.InsertOrder(model, model.UserName);
                TempData["OrderId"] = ressult.ToString();
                return RedirectToAction("PayStatus", "Payment");
            }
            return Redirect(url);
        }

        [Route("/Payment/PayStatus/")]
        public IActionResult PayStatus(string orderid, string respcode, string paymentid, string opaque)
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            if (TempData["OrderId"] != null && string.IsNullOrEmpty(orderid))
            {
                orderid = TempData["OrderId"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(respcode))
            {
                var webRoot = _env.WebRootPath;
                string path = webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\" + orderid.ToString() + ".xml";
                string pathConfirm = webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\" + "confirm" + orderid.ToString() + ".xml";
                XDocument xdoc = XDocument.Load(path);
                AmeriaClient cl = new AmeriaClient();
                try
                {
                    cl.OpenAsync();
                    if (orderid != xdoc.Root.Element("OrderID").Value && opaque != xdoc.Root.Element("Opaque").Value)
                    {
                        ViewBag.resp = "Ծրագրային սխալ";
                        return View();
                    }
                    PaymentClientClass conf = new PaymentClientClass();
                    conf.ClientID = xdoc.Root.Element("ClientID").Value.ToString();
                    conf.Username = "19533087_api";
                    conf.Password = xdoc.Root.Element("Password").Value.ToString();
                    conf.Description = xdoc.Root.Element("Description").Value;
                    conf.OrderID = int.Parse(xdoc.Root.Element("OrderID").Value);
                    conf.Opaque = xdoc.Root.Element("Opaque").Value;
                    conf.PaymentAmount = decimal.Parse(xdoc.Root.Element("PaymentAmount").Value);
                    RespMessage result = cl.ConfirmationAsync(conf).Result;
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlSerializer xmlSerializer = new XmlSerializer(result.GetType());
                    using (MemoryStream xmlStream = new MemoryStream())
                    {
                        xmlSerializer.Serialize(xmlStream, result);
                        xmlStream.Position = 0;
                        xmlDoc.Load(xmlStream);
                        xmlDoc.Save(pathConfirm);
                    }
                    var gf = new PaymentClientClass();
                    gf.ClientID = "1c5bccc8-8638-44d8-8428-f6528c8aee06";
                    gf.Username = "19533087_api";
                    gf.Password = "a7KWUMqC5v3l9jW";
                    gf.OrderID = int.Parse(xdoc.Root.Element("OrderID").Value);
                    gf.PaymentAmount = decimal.Parse(xdoc.Root.Element("PaymentAmount").Value);

                    PaymentFields resf = cl.GetPaymentFieldsAsync(gf).Result;
                    XmlDocument xmlGetPaymentFields = new XmlDocument();
                    XmlSerializer xmlSerializerGetPaymentFields = new XmlSerializer(resf.GetType());
                    using (MemoryStream xmlStreamGetPaymentFields = new MemoryStream())
                    {
                        xmlSerializerGetPaymentFields.Serialize(xmlStreamGetPaymentFields, resf);
                        xmlStreamGetPaymentFields.Position = 0;
                        xmlGetPaymentFields.Load(xmlStreamGetPaymentFields);
                        string pathxmlStreamGetPaymentFields = webRoot + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\" + "PaymentFields" + orderid.ToString() + ".xml";
                        xmlGetPaymentFields.Save(pathxmlStreamGetPaymentFields);
                    }
                    ProductLayer pl = new ProductLayer();
                    if (resf.respcode == "00")
                    {
                        string ordr = System.IO.File.ReadAllText(_env.WebRootPath + "\\tmpxml\\" + orderid + ".txt");

                        ProductLayer ly = new ProductLayer();
                        System.IO.File.Delete(_env.WebRootPath + "\\tmpxml\\" + orderid + ".txt");
                        var ordrmodel = LoadFromXMLString(ordr);
                        var ressult = ly.InsertOrder(ordrmodel, ordrmodel.UserName);
                        ViewBag.resp = resf.descr + "ORDER ID: " + ressult.ToString();
                    }

                    cl.CloseAsync();

                }
                catch (Exception)
                {
                    ViewBag.resp = "Ծրագրային սխալ";
                    return View();
                }
                finally
                {
                    if (cl != null)
                    {
                        cl.CloseAsync();
                    }
                }
            }
            else
            {
                ViewBag.resp = "Պատվերն ընդունվել է , շնորհակալություն գնումների համար: Order Id:" + orderid;
            }
            return View();
        }
        private string ToXML(OrderEntityViewModel model)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(model.GetType());
            serializer.Serialize(stringwriter, model);
            return stringwriter.ToString();
        }

        private OrderEntityViewModel LoadFromXMLString(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(OrderEntityViewModel));
            return serializer.Deserialize(stringReader) as OrderEntityViewModel;
        }
    }
}