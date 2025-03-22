using AutoMapper;
using Data.LohikaBackend;
using Data.LohikaBackend.Entities;
using LohikaBackend.Abastract;
using LohikaBackend.Constants;
using LohikaBackend.Helpers;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LohikaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    //[Authorize(Roles = Roles.Admin)]
    public class CategoriesController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IImageService _imageService;
        
        public CategoriesController(AppEFContext context,
            IMapper mapper,
            IWebHostEnvironment env, 
            IConfiguration configuration,
            IImageService imageService)
        {
            //Thread.Sleep(2000);
            _context = context;
            _mapper = mapper;
            _env = env;
            _configuration = configuration;
            _imageService = imageService;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] CategoryAddViewModel model)
        {
            try
            {
                string fileName = String.Empty;

                var entity = _context.Categories
                    .SingleOrDefault(x => x.UrlSlug == model.UrlSlug);
                if (entity != null)
                {
                    return BadRequest(new { error = "Даний category url уже є!" });
                }

                entity = _mapper.Map<CategoryEntity>(model);

                if (model.Image != null)
                {
                    entity.Image = await _imageService.SaveImageAsync(model.Image);
                }
                _context.Categories.Add(entity);
                await _context.SaveChangesAsync();
                return Ok();
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
        [AllowAnonymous]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                //Thread.Sleep(2000);
                var model = await _context.Categories
                    .OrderBy(x => x.Priority)
                    .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                    .ToListAsync();
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

        [HttpGet]
        [AllowAnonymous]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] CategorySearchViewModel search)
        {
            try
            {
                int page = search.Page;
                //Thread.Sleep(2000);
                int pageSize = 8;
                var query = _context.Categories.AsQueryable();
                if (!string.IsNullOrEmpty(search.Id))
                {
                    int id = int.Parse(search.Id);
                    query = query.Where(x => x.Id == id);
                }
                if (!string.IsNullOrEmpty(search.Title))
                {
                    query = query.Where(x => x.Title.ToLower().Contains(search.Title.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Priority))
                {
                    int priority = int.Parse(search.Priority);
                    query = query.Where(x => x.Priority == priority);
                }
                var model = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                    .ToListAsync();
                int total = query.Count();
                int pages = (int)Math.Ceiling(total / (double)pageSize);
                return Ok(new CategorySearchResultViewModel
                {
                    Categories = model,
                    Total = total,
                    CurrentPage=page,
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

        [Route("delete/{id}")]
        
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var entity = _context.Categories.SingleOrDefault(x => x.Id == id);
                if (entity == null)
                    return NotFound();

                if (entity.Image != null)
                {
                    _imageService.DeleteImage(entity.Image);
                }

                _context.Categories.Remove(entity);
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
        [AllowAnonymous]
        public IActionResult GetCategoryById(int id)
        {
            Thread.Sleep(2000);
            var entity = _context.Categories
                .SingleOrDefault(x => x.Id == id);
            if(entity == null)
                return NotFound();
            return Ok(_mapper.Map<CategoryEditViewModel>(entity));
        }

        [HttpPut("edit")]
        public IActionResult Edit([FromBody] CategorySaveViewModel model)
        {
            try
            {
                var entity = _context.Categories
                    .SingleOrDefault(x => x.Id == model.Id);
                if (entity != null)
                {
                    entity.Title = model.Title;
                    entity.Priority = model.Priority;
                    entity.UrlSlug = model.UrlSlug;
                    string fileName = String.Empty;
                    if (model.Image != null)
                    {
                        _imageService.DeleteImage(entity.Image);
                        entity.Image = _imageService.SaveImageAsync(model.Image).Result;
                    }
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
