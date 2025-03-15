namespace LohikaBackend.Models
{
    public class CategorySearchViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public int Page { get; set; } = 1;
    }

    public class CategorySearchResultViewModel
    {
        public List<CategoryItemViewModel> Categories { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int Total { get; set; }
    }
    public class CategoryAddViewModel
    {
        public string? Title { get; set; }
        public int Priority { get; set; }
        public string? UrlSlug { get; set; }
        //public IFormFile? Image { get; set; }
        public string Image { get; set; }
    }

    public class CategoryEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
    }

    public class CategorySaveViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
    }
    public class CategoryItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }

    }
}
