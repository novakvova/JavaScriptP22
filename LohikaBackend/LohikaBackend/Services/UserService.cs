using AutoMapper;
using Data.LohikaBackend;
using Data.LohikaBackend.Entities.Identity;
using LohikaBackend.Abastract;
using LohikaBackend.Constants;
using LohikaBackend.Exceptions;
using LohikaBackend.Helpers;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace LohikaBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly AppEFContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IImageService _imageService;
        public UserService(
            UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService,
            AppEFContext context,
            IMapper mapper,
            IWebHostEnvironment env,
            IConfiguration configuration,
            IImageService imageService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _env = env;
            _configuration = configuration;
            _imageService = imageService;
        }
        public async Task<string> CreateUser(RegisterViewModel model)
        {

            string fileName = String.Empty;
            var user = _mapper.Map<AppUser>(model);
            try
            {
                //throw new Exception("asdfa");
                if (model.Photo != null)
                {
                    if (model.Photo != null)
                    {
                        fileName = await _imageService.SaveImageAsync(model.Photo);

                        user.Photo = fileName;
                    }
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(fileName))
                        File.Delete(fileName);
                    AccountError accountError = new AccountError();
                    foreach (var item in result.Errors)
                    {
                        accountError.Errors.Invalid.Add(item.Description);
                    }

                    throw new AccountException(accountError);
                }

                result = await _userManager.AddToRoleAsync(user, Roles.User);
                if (!result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(fileName))
                        File.Delete(fileName);
                    result = await _userManager.DeleteAsync(user);
                    AccountError accountError = new AccountError();
                    foreach (var item in result.Errors)
                    {
                        accountError.Errors.Invalid.Add(item.Description);
                    }
                    throw new AccountException(accountError);
                }
                return _jwtTokenService.CreateToken(user);
            }
            catch (AccountException aex)
            {
                throw new AccountException(aex.AccountError);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(fileName))
                    File.Delete(fileName);
                if (user.Id != 0)
                {
                    var result = await _userManager.DeleteAsync(user);
                }
                AccountError accountError = new AccountError();
                accountError.Errors.Invalid.Add(ex.Message);
                throw new AccountException(accountError);
            }
        }

        public void UpdateUser(UserSaveViewModel model)
        {
            try
            {
                var user = _context.Users
                    .SingleOrDefault(x => x.Id == model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.SecondName = model.SecondName;
                    user.Phone = model.Phone;
                    string fileName = String.Empty;
                    if (model.Photo != null)
                    {
                        if (string.IsNullOrEmpty(user.Photo))
                            user.Photo = Path.GetRandomFileName() + ".jpeg";
                        string randomFilename = user.Photo;

                        string pathSaveImages = InitStaticFiles
                            .CreateImageByFileName(_env, _configuration,
                                new string[] { "Folder" },
                                randomFilename, model.Photo, false, false);
                    }
                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
