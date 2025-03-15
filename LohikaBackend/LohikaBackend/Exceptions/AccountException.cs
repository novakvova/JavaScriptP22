using LohikaBackend.Models;

namespace LohikaBackend.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException(AccountError accountError)
        {
            AccountError = accountError;
        }
        public AccountError AccountError { get; private set; }
    }
}
