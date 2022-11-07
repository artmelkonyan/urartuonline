using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using AmeriaPay;
using BonusMarket.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BonusMarket.Controllers
{
    public class OrderController : BaseController
    {
        private IHostingEnvironment _env;
        public OrderController(IHostingEnvironment env)
        {
            _env = env;
        }
        
    }
}