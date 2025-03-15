using Data.LohikaBackend.Entities.Identity;
using LohikaBackend.Abastract;
using LohikaBackend.Exceptions;
using LohikaBackend.Models;
using LohikaBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Net;

namespace LohikaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IRecaptchaService _recaptchaService;

        public AccountController(IUserService userService, UserManager<AppUser> userManager,
            IJwtTokenService tokenService,
            IConfiguration configuration,
            IEmailSender emailSender,
            IRecaptchaService recaptchaService,
            IMapper mapper)
        {
            //Thread.Sleep(2000);
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _emailSender = emailSender;
            _mapper = mapper;
            _recaptchaService = recaptchaService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                //if(!_recaptchaService.IsValid(model.RecaptchaToken))
                //{
                //    return BadRequest(new AccountError("Ви не пройшли верифікацію капча!"));
                //}
                string token = await _userService.CreateUser(model);
                return Ok(
                    new { token }
                    );
            }
            catch (AccountException aex)
            {
                return BadRequest(aex.AccountError);
            }
            catch(Exception ex)
            {
                return BadRequest(new AccountError("Щось пішло не так! "+ ex.Message));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    string token = _tokenService.CreateToken(user);
                    return Ok(
                        new { token }
                    );
                }
                else
                {

                    var exc = new AccountError();
                    exc.Errors.Invalid.Add("Пароль не вірний!");
                    throw new AccountException(exc);
                }
            }
            catch (AccountException aex)
            {
                return BadRequest(aex.AccountError);
            }
            catch (Exception ex)
            {
                return BadRequest(new AccountError("Щось пішло не так! " + ex.Message));
            }
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            try
            {
                //Thread.Sleep(2000);
                string userName = User.Claims.FirstOrDefault().Value;
                var user =  await _userManager.FindByNameAsync(userName);
                var model = _mapper.Map<ProfileViewModel>(user);
                
                return Ok(model);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    invalid = ex.Message
                });
            }
        }
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return BadRequest("Not found user");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var frontEndURL = _configuration.GetValue<string>("FrontEndURL");

            var callbackUrl =
                $"{frontEndURL}/resetpassword?userId={user.Id}&" +
                $"code={WebUtility.UrlEncode(token)}";

            //Url.Action(nameof(ResetPassword), "AccountController", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { forgotPasswordModel.Email }, "Reset password token",
                $"Please reset password by clicking here: " +
               $"<a href='{callbackUrl}'>Відновити</a>");
            _emailSender.SendEmail(message);

            return Ok();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var res = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            return Ok();
        }

        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest request)
        {
            var payload = await _tokenService.VerifyGoogleToken(request);
            if (payload == null)
            {
                return BadRequest(new AccountError("Щось пішло не так!"));
            }
            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new AppUser
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName = payload.GivenName,
                        SecondName = payload.FamilyName
                    };
                    var resultCreate = await _userManager.CreateAsync(user);
                    if (!resultCreate.Succeeded)
                    {
                        return BadRequest(new AccountError("Щось пішло не так!"));
                    }

                }

                var resultAddLogin = await _userManager.AddLoginAsync(user, info);
                if (!resultAddLogin.Succeeded)
                {
                    return BadRequest(new AccountError("Щось пішло не так!"));
                }
            }

            return Ok(_tokenService.CreateToken(user));
        }

    }
}
