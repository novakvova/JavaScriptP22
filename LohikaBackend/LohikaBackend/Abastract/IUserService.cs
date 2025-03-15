using LohikaBackend.Models;

namespace LohikaBackend.Abastract
{
    public interface IUserService
    {
        public Task<string> CreateUser(RegisterViewModel model);
        public void UpdateUser(UserSaveViewModel model);
    }
}
