using AutoMapper;
using Data.LohikaBackend;
using Data.LohikaBackend.Entities;
using Data.LohikaBackend.Entities.Identity;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LohikaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CartsController(AppEFContext context,
           IMapper mapper, UserManager<AppUser> userManager)
        {
            //Thread.Sleep(2000);
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] CartAddViewModel model)
        {
            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.UserId == user.Id && x.ProductId == model.ProductId);
                if (cart == null)
                {
                    cart = _mapper.Map<CartEntity>(model);
                    cart.UserId = user.Id;
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.Quantity += model.Quantity;
                    _context.SaveChanges();
                }

                var result = _context.Carts
                    .Include(x => x.Product)
                    .Where(x => x.Id == cart.Id)
                    .Select(x => _mapper.Map<CartItemViewModel>(x))
                    .First();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                //Thread.Sleep(2000);
                string userName = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByNameAsync(userName);
                var model = await _context.Carts
                    .Where(x => x.UserId == user.Id)
                    .Include(x => x.Product)
                    .Include(x => x.Product.ProductImages)
                    .Select(x => _mapper.Map<CartItemViewModel>(x)).ToListAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cartItem = _context.Carts.SingleOrDefault(x => x.Id == id);
                if (cartItem == null)
                    return NotFound();
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [HttpPut("edit")]
        public IActionResult Save([FromBody] CartEditViewModel model)
        {
            try
            {
                var cart = _context.Carts
                    .SingleOrDefault(x => x.Id == model.Id);
                if (cart != null)
                {
                    cart.Quantity = model.Quantity;
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }
    }
}
