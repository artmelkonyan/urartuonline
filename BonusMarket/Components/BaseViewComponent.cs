using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BonusMarket.Components
{
    public abstract class BaseViewComponent : ViewComponent
    {
        protected string RequestLanguage { get; private set; }

        public BaseViewComponent()
        {
            RequestLanguage = Thread.CurrentThread.CurrentUICulture.Name;
        }
    }
}
