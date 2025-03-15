namespace LohikaBackend.Models
{
    public class UserItemViewModel
    {
        public long Id { get; set; }

        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Photo { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    public class UserSearchViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int Page { get; set; } = 1;
    }

    public class UserSearchResultViewModel
    {
        public List<UserItemViewModel> Users { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
    }

    public class UserEditViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class UserSaveViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
