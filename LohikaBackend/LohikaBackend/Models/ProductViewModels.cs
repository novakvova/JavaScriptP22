namespace LohikaBackend.Models
{
    public class ProductSearchViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal ? Price { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string CategorySlug { get; set; }
        public int Page { get; set; } = 1;
    }

    public class ProductSearchResultViewModel
    {
        public List<ProductItemViewModel> Products { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int Total { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductAddViewModel
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public int ? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int [] ids { get; set; }
    }

    public class ProductImageAddViewModel
    {
        public string Image { get; set; }
    }
    public class ProductImageItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<ProductImageItemViewModel> Images { get; set; }
    }

    public class ProductSaveViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Priority { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int[] ids { get; set; }
        //public IFormFile? Image { get; set; }
    }
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public decimal Price { get; set; }

    }
}
