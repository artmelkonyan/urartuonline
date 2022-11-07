using System;
using System.Xml.Linq;
using DataLayer;
using System.Collections.Generic;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Http.Internal;

namespace BusinessLayer
{
    public class CategoryLayer
    {
        public CategoryEntity GetCategoryById(int id, string lang)
        {
            return new CategoryDbProxy().GetCategoryById(id, lang);
        }
        public List<CategoryEntity> GetCategoryTree()
        {
            List<CategoryEntity> categories = null;
            CategoryDbProxy db = new CategoryDbProxy();
            categories = db.GetCategoryTree();
            return categories;
        }
        public List<CategoryEntity> GetHomePageCategories(string lang)
        {
            List<CategoryEntity> homePageCategories = new List<CategoryEntity>();
            CategoryDbProxy db = new CategoryDbProxy();
            homePageCategories = db.GetHompageCategories(lang);
            return homePageCategories;
        }
        public  List<CategoryEntity> GetCategoriesByBrandId(int brandId,string lang)
        {
            Tuple<List<CategoryEntity>, List<CategoryEntity>> tuple;
            CategoryDbProxy db = new CategoryDbProxy();
            List<CategoryEntity> categories = db.GetCategoriesByBrandId(brandId,lang);
            foreach (CategoryEntity item in categories)
            {
                item.SubCategories = this.ClassifyOrgUnits(categories, item.Id.Value);
            }

            return categories;
        }
        public Tuple<List<CategoryEntity>, List<CategoryEntity>> GetLayoutCategories(string lang)
        {
            Tuple<List<CategoryEntity>, List<CategoryEntity>> tuple;
            CategoryDbProxy db = new CategoryDbProxy();
            List<CategoryEntity> categories = db.GetLayoutCategories(lang);

            List<CategoryEntity> firstList = categories.FindAll(x => x.ShowOnLayout.HasValue && x.ShowOnLayout.Value);
            firstList = firstList.Distinct().ToList();
            foreach (CategoryEntity item in firstList)
            {
                item.SubCategories = this.ClassifyOrgUnits(categories, item.Id.Value);
            }

            tuple = new Tuple<List<CategoryEntity>, List<CategoryEntity>>(firstList, categories);

            return tuple;
        }
        public bool GetCategoryByBrandCheck(int id)
        {
            var s = GetCategoryById(id, "hy");
            s.ParentId = s.ParentId ?? 0;
            s.Status = s.Status ?? false;
            if (s.ParentId == 0 && s.Status.HasValue && s.Status.Value)
                return true;
            if (s.Status.HasValue && !s.Status.Value)
                return false;
            
            return GetCategoryByBrandCheck(s.ParentId.Value);

        }
        public CategoryEntityDashboardViewModel GetCategoryDefultModel()
        {
            CategoryEntityDashboardViewModel category = new CategoryEntityDashboardViewModel();
            List<CategoryEntityDashboardViewModel> catLsit = new List<CategoryEntityDashboardViewModel>() { new CategoryEntityDashboardViewModel() {
            Id=0,
            Translation=new CategoryTranslationEntityViewModel()
                            {
                                Name="Անծնող կատեգորիյա"
                            }
                }
            };
            AdminDbProxy a_dbproxy = new AdminDbProxy();
            catLsit.AddRange(a_dbproxy.GetProductCatList());
            category.TranslationList = GetDefultTranslationList();
            category.CategoriesList = catLsit;
            return category;
        }
        public List<CategoryTranslationEntityViewModel> GetDefultTranslationList()
        {
            var model = new List<CategoryTranslationEntityViewModel>()
            {
                new CategoryTranslationEntityViewModel()
                {
                    Language="hy",
                    Status=true
                },
                new CategoryTranslationEntityViewModel()
                {
                    Language="en",
                    Status=true
                },
                new CategoryTranslationEntityViewModel()
                {
                    Language="ru",
                    Status=true
                }
            };
            return model;
        }
        public List<CategoryEntity> GetCategoriesByParentId(int parentId)
        {
            CategoryDbProxy db = new CategoryDbProxy();
            var categories = db.GetCategoriesByParentId(parentId);
            return categories;
        }
        public Tuple<List<CategoryEntity>, List<CategoryEntity>> GetCategoriesTreeByParentId(int parentId, string lang)
        {
            Tuple<List<CategoryEntity>, List<CategoryEntity>> tuple;
            CategoryDbProxy db = new CategoryDbProxy();
            List<CategoryEntity> categories = db.GetCategoriesTreeByParentId(parentId, lang);

            List<CategoryEntity> firstList = categories.FindAll(r => r.ParentId == parentId);

            foreach (CategoryEntity item in firstList)
            {
                item.SubCategories = this.ClassifyOrgUnits(categories, item.Id.Value);
            }

            tuple = new Tuple<List<CategoryEntity>, List<CategoryEntity>>(firstList, categories);

            return tuple;
        }

        private List<CategoryEntity> ClassifyOrgUnits(List<CategoryEntity> OrgUnitList, int parentID)
        {
            List<CategoryEntity> list = new List<CategoryEntity>();
            //List<OrgUnitEntity> currentLibsList = OrgUnitList.FindAll(r => (r.ParentID == parentID) && (r.TypeID == 4));
            List<CategoryEntity> currentLibsList = OrgUnitList.FindAll(r => (r.ParentId == parentID));

            if (currentLibsList.Count > 0)
            {
                foreach (CategoryEntity currentLib in currentLibsList)
                {
                    List<CategoryEntity> tempLibsList = OrgUnitList.FindAll(r => r.ParentId == currentLib.Id);
                    if (tempLibsList.Count > 0)
                    {
                        currentLib.SubCategories = this.ClassifyOrgUnits(OrgUnitList, currentLib.Id.Value);
                    }
                    list.Add(currentLib);
                }
            }

            //currentLibsList = OrgUnitList.FindAll(r => (r.ParentID == parentID) && (r.TypeID != 4));
            //currentLibsList.OrderBy(x => x.SortLevel);
            //if (currentLibsList.Any())
            //{
            //    foreach (OrgUnitEntity currentLib in currentLibsList)
            //    {
            //        List<OrgUnitEntity> tempLibsList = OrgUnitList.FindAll(r => r.ParentID == currentLib.ID);
            //        if (tempLibsList.Any())
            //        {
            //            currentLib.subItems = this.ClassifyOrgUnits(OrgUnitList, currentLib.ID);
            //        }
            //        list.Add(currentLib);
            //    }
            //}
            return list;
        }
    }
}
