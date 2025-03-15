using AutoMapper;
using Data.LohikaBackend;
using LohikaBackend.Abastract;
using LohikaBackend.Constants;
using LohikaBackend.Exceptions;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LohikaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(AppEFContext context,
            IUserService userService,
            IMapper mapper)
        {
           //Thread.Sleep(2000);
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }
        [Route("all")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var list = _context.Users
                .Select(x => _mapper.Map<UserItemViewModel>(x))
                .ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] UserSearchViewModel search)
        {
            try
            {
                int page = search.Page;
                int pageSize = 3;
                var query = _context.Users.AsQueryable();
                if (search.Id != 0)
                {
                    query = query.Where(x => x.Id == search.Id);
                }
                if (!string.IsNullOrEmpty(search.FirstName))
                {
                    query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.SecondName))
                {
                    query = query.Where(x => x.SecondName.ToLower().Contains(search.SecondName.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Phone))
                {
                    query = query.Where(x => x.Phone.ToLower().Contains(search.Phone.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Email))
                {
                    query = query.Where(x => x.Email.ToLower().Contains(search.Email.ToLower()));
                }
                var model = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => _mapper.Map<UserItemViewModel>(x))
                    .ToListAsync();
                int total = query.Count();
                int pages = (int)Math.Ceiling(total / (double)pageSize);
                return Ok(new UserSearchResultViewModel
                {
                    Users = model,
                    Total = total,
                    CurrentPage = page,
                    Pages = pages
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RegisterViewModel model)
        {
            try
            {
                string token = await _userService.CreateUser(model);
                return Ok();
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

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //Thread.Sleep(2000);
            try
            {
                var user = _context.Users.SingleOrDefault(x => x.Id == id);
                if (user == null)
                    return NotFound();

                if (user.Photo != null)
                {
                    var directory = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    var FilePath = Path.Combine(directory, user.Photo);
                    System.IO.File.Delete(FilePath);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            //Thread.Sleep(2000);
            var user = _context.Users
                .SingleOrDefault(x => x.Id == id);
            return Ok(_mapper.Map<UserEditViewModel>(user));
        }

        [HttpPut("edit")]
        public IActionResult Edit([FromBody] UserSaveViewModel model)
        {
            try
            {
                _userService.UpdateUser(model);
                return Ok();
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
    }
}
