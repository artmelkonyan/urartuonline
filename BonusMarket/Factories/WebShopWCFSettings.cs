using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using WebShop;

namespace BonusMarket.Factories
{
    public static class WebShopWCFSettings
    {
        public static string Name = "WebShop";
        public readonly static string Password = "web1598";
        public readonly static string Url = "http://212.42.214.82:33335/URARTU/ws/WebShop/";
        public static WebShopPortTypeClient GetClient()
        {
            BasicHttpBinding myBinding = new BasicHttpBinding();
            myBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            EndpointAddress ea = new EndpointAddress(Url);
            WebShopPortTypeClient Service = new WebShopPortTypeClient(myBinding, ea);
            Service.ClientCredentials.UserName.UserName = Name;
            Service.ClientCredentials.UserName.Password = Password;
            return Service;
        }
    }
}
