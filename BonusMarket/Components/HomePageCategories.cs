using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Components
{
    public class HomePageCategoriesViewComponent : BaseViewComponent
    {
        CategoryLayer c_layer;

        public HomePageCategoriesViewComponent()
        {
            c_layer = new CategoryLayer();
        }

        public IViewComponentResult Invoke()
        {
            var categories = c_layer.GetHomePageCategories(RequestLanguage);
            return View(categories);
        }
    }
}
