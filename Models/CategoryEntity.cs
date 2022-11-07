using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CategoryTranslationEntity : TranslationEntity
    {
        public int? CategoryId { get; set; } = null;
        public string DescriptionTranslation { get; set; } = null;
        public string SeoName { get; set; } = null;
        public string Name { get; set; } = null;
    }
    public class CategoryTranslationEntityViewModel : TranslationEntityViewModel
    {
        public int CategoryId { get; set; }
        public string DescriptionTranslation { get; set; }
        public string SeoName { get; set; }
        public string Name { get; set; }
    }
    public class CategoryPictureEntity
    {
        public int? PicId { get; set; } = null;
        public string PicName { get; set; } = null;
    }
    public class CategoryPictureEntityViewModel
    {
        public int PicId { get; set; }
        public string PicName { get; set; }
    }
    public class CategoryEntity : BaseEntity
    {
        public CategoryEntity()
        {
            this.SubCategories = new List<CategoryEntity>();
        }
        public int? PictureId { get; set; } = null;
        public int? OldId { get; set; } = null;
        public CategoryTranslationEntity Translation { get; set; } = null;
        public List<CategoryTranslationEntity> TranslationList { get; set; } = null;
        public int? ParentId { get; set; } = null;
        public bool? Published { get; set; } = null;
        public int? DisplayOrder { get; set; } = null;
        public bool? ShowOnHomePage { get; set; } = null;
        public bool? ShowOnLayout { get; set; } = null;

        public PictureEntity Picture { get; set; } = null;
        public List<ProductEntity> ProductList { get; set; } = null;
        public List<CategoryEntity> SubCategories { get; set; } = null;

        public CategoryPictureEntity CategoryPicture { get; set; } = null;
        public int? SubItems { get; set; } = null;

    }
    public class CategoryEntityDashboardViewModel : BaseEntityViewModel
    {
        public CategoryEntityDashboardViewModel()
        {
            this.CategoriesList = new List<CategoryEntityDashboardViewModel>();
        }
        public int PictureId { get; set; }
        public int OldId { get; set; }
        public CategoryTranslationEntityViewModel Translation { get; set; } 
        public int ParentId { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool ShowOnLayout { get; set; }
        public PictureEntityViewModel Picture { get; set; }
        public List<ProductEntity> ProductList { get; set; }
        public List<CategoryEntityDashboardViewModel> CategoriesList { get; set; }
        public List<CategoryTranslationEntityViewModel> TranslationList { get; set; }
        public string ParentCategoryNameHy { get; set; }

        public CategoryPictureEntityViewModel CategoryPicture { get; set; }

    }
}
