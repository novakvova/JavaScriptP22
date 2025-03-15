namespace LohikaBackend.Models
{
    public class BlogSearchViewModel
    {
        //public string? Id { get; set; }
        //public string? Name { get; set; }
        //public decimal? Price { get; set; }
        //public string? Priority { get; set; }
        public int pageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }

    public class BlogSearchResultViewModel
    {
        public List<BlogItemViewModel> Products { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int Total { get; set; }
    }
    public class BlogAddViewModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public bool IsShow { get; set; }
        public string DateTimePublish { get; set; }
    }

    public class BlogEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public bool IsShow { get; set; }
        public string DateTimePublish { get; set; }
    }

    public class BlogSaveViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public bool IsShow { get; set; }
        public string DateTimePublish { get; set; }
    }
    public class BlogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public bool IsShow { get; set; }
        public string DateTimePublish { get; set; }
    }

    public class BlogImageItemViewModel
    {
        public string Name { get; set; }
    }
}
