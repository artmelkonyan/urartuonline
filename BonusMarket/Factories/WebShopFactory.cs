using BusinessLayer;
using Microsoft.AspNetCore.Hosting;
using Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using WebShop;

namespace BonusMarket.Factories
{
    public class WebShopFactory
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public WebShopFactory(IHostingEnvironment _hostingEnvironment)
        {
            this._hostingEnvironment = _hostingEnvironment;
        }
        public async Task<GetItemByBarcodeResponse> GetItemBySku(string sku)
        {
            try
            {
                var client = WebShopWCFSettings.GetClient();
                await client.OpenAsync();
                var res = await client.GetItemByBarcodeAsync(new GetItemByBarcodeRequest(sku));
                await client.CloseAsync();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int> AddProduct(string sku)
        {
            var product = await GetItemBySku(sku);
            if (!product.@return)
                return 0;
            AdminLayer al = new AdminLayer();
            int res = 0;
            var p2 = al.GetProductBySku(sku);
            if (p2.Id != null)
            {
                var editProduct = al.GetProductById(p2.Id);
                editProduct.Count = (int)(product.Item.InStock ?? 0);
                if (product.Item.Price.HasValue)
                    editProduct.Price = (decimal)(product.Item.Price);
                if (product.Item.Price_Old.HasValue)
                    editProduct.OldPrice = (decimal)(product.Item.Price_Old);
                al.EditProduct(editProduct, null, null, _hostingEnvironment.WebRootPath);
                res = p2.Id ?? 0;
            }
            else
            {
                var nProduct = new ProductEntity()
                {
                    Count = p2.Count = (int)(product.Item.InStock ?? 0),
                    Price = decimal.Parse((product.Item.Price ?? 0).ToString()),
                    OldPrice = decimal.Parse((product.Item.Price_Old ?? 0).ToString()),
                    Sku = sku,
                    Translation = new ProductTranslationEntity() { Language = "hy", NameTranslation = product.Item.Name },
                    TranslationList = new List<ProductTranslationEntity>() { new ProductTranslationEntity()
                   {
                      Language="en",NameTranslation=product.Item.Name
                   },new ProductTranslationEntity(){
                       Language="hy",NameTranslation=product.Item.Name
                       },new ProductTranslationEntity(){
                       Language="ru",NameTranslation=product.Item.Name
                       }
                   },
                    CreationDate = DateTime.Now
                };

                int id = al.AddProduct(nProduct) ?? 0;
                res = id;
            }
            return res;
        }

        public async Task<bool> CheckDbByWCF()
        {
            try
            {
                AdminLayer al = new AdminLayer();
                var products = al.GetAllProducts();
                int count = 0;
                foreach (var item in products)
                {
                    var wcProduct = await GetItemBySku(item.Sku);
                    count++;
                    if (count % 100 == 0)
                    {
                        Debug.WriteLine("sss");
                    }
                    if (!wcProduct.@return)
                    {
                        if (item.Count > 0)
                        {
                            item.Count = 0;
                            al.UpdateProduct(item);
                        }
                        continue;
                    }
                    item.Price = (decimal)(wcProduct.Item.Price ?? 0);
                    item.Count = (int)(wcProduct.Item.InStock ?? 0);
                    item.OldPrice = (decimal?)wcProduct.Item.Price_Old;
                    al.UpdateProduct(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<List<GetItemByBarcodeResponse>> GetItemsBySku(IEnumerable<string> skus)
        {
            try
            {
                var client = WebShopWCFSettings.GetClient();
                var list = new List<GetItemByBarcodeResponse>();
                await client.OpenAsync();
                foreach (var sku in skus)
                {
                    var res = await client.GetItemByBarcodeAsync(new GetItemByBarcodeRequest(sku));
                    list.Add(res);
                }
                await client.CloseAsync();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
