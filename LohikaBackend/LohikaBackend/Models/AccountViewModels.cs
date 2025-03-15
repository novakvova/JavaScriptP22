namespace LohikaBackend.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        //public string RecaptchaToken { get; set; }
    }

    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Photo { get; set; }
        public string? Phone { get; set; }
    }

    public class LoginViewModel
    {

        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class AccountError
    {
        public AccountError()
        {
            Errors = new AccountErrorItem();
        }
        public AccountError(string message)
        {
            Errors = new AccountErrorItem();
            Errors.Invalid.Add(message);
        }
        public AccountErrorItem Errors { get; set; }
    }

    public class AccountErrorItem
    {
        public AccountErrorItem()
        {
            Invalid = new List<string>();
        }
        public List<string> Invalid { get; set; }
    }
    public class ExternalLoginRequest
    {
        public string Provider { get; set; }
        public string Token { get; set; }
    }
}
