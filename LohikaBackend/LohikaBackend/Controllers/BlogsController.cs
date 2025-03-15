using AutoMapper;
using Data.LohikaBackend;
using Data.LohikaBackend.Entities;
using LohikaBackend.Constants;
using LohikaBackend.Helpers;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LohikaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class BlogsController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public BlogsController(AppEFContext context,
            IWebHostEnvironment env, IConfiguration configuration,
            IMapper mapper)
        {
            //Thread.Sleep(2000);
            _context = context;
            _env = env;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] BlogAddViewModel model)
        {
            try
            {
                string fileName = String.Empty;
                var entity = _mapper.Map<BlogEntity>(model);

                if (model.Image != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        ".jpeg";
                    string pathSaveImages = InitStaticFiles
                        .CreateImageByFileName(_env, _configuration,
                            new string[] { "Folder" },
                            randomFilename, model.Image, false, false);

                    entity.Image = randomFilename;
                }
                _context.Blogs.Add(entity);
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

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromBody] ProductImageAddViewModel model)
        {
            string url = String.Format("{0}://{1}/{2}", HttpContext.Request.Scheme, HttpContext.Request.Host, "images");
            try
            {
                var entity = new BlogImageEntity();

                if (model.Image != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        ".jpeg";
                    string pathSaveImages = InitStaticFiles
                        .CreateImageByFileName(_env, _configuration,
                            new string[] { "Folder" },
                            randomFilename, model.Image, false, false);

                    entity.Name = randomFilename;
                }
                _context.BlogImages.Add(entity);
                await _context.SaveChangesAsync();
                List<BlogImageItemViewModel> images = new List<BlogImageItemViewModel>();
                string[] imageSizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
                foreach (var size in imageSizes)
                {
                    images.Add(new BlogImageItemViewModel
                    {
                        Name = String.Format("{0}/{1}_{2}", url, size, entity.Name)
                    });
                }
                return Ok(images);
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
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            try
            {
                //Thread.Sleep(2000);
                var model = await _context.Blogs
                    .OrderByDescending(x => x.DateTimePublish)
                    .Select(x => _mapper.Map<BlogItemViewModel>(x))
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
        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] BlogSearchViewModel search)
        {
            try
            {
                int page = search.Page;
                int pageSize = search.pageSize;
                var query = _context.Blogs
                    .Where(x=>x.IsShow)
                    .OrderByDescending(x => x.DateTimePublish)
                    .AsQueryable();
                //if (!string.IsNullOrEmpty(search.Id))
                //{
                //    int id = int.Parse(search.Id);
                //    query = query.Where(x => x.Id == id);
                //}
                //if (!string.IsNullOrEmpty(search.Name))
                //{
                //    query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
                //}
                //if (search.Price != null)
                //{
                //    query = query.Where(x => x.Price == search.Price);
                //}
                //if (!string.IsNullOrEmpty(search.Priority))
                //{
                //    int priority = int.Parse(search.Priority);
                //    query = query.Where(x => x.Priority == priority);
                //}
                var model = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => _mapper.Map<BlogItemViewModel>(x))
                    .ToListAsync();
                int total = query.Count();
                int pages = (int)Math.Ceiling(total / (double)pageSize);
                return Ok(new BlogSearchResultViewModel
                {
                    Products = model,
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

        [Route("delete/{id}")]

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var entity = _context.Blogs.SingleOrDefault(x => x.Id == id);
                if (entity == null)
                    return NotFound();

                if (entity.Image != null)
                {
                    var directory = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    var FilePath = Path.Combine(directory, entity.Image);
                    System.IO.File.Delete(FilePath);
                }

                _context.Blogs.Remove(entity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [Route("get/{slug}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetBlogById(string slug)
        {
            //Thread.Sleep(2000);
            var product = _context.Blogs
                .SingleOrDefault(x => x.Slug == slug);
            return Ok(_mapper.Map<BlogEditViewModel>(product));
        }

        [HttpPut("edit")]
        public IActionResult Edit([FromBody] BlogSaveViewModel model)
        {
            try
            {
                var entity = _context.Blogs
                    .SingleOrDefault(x => x.Id == model.Id);
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Text = model.Text;
                    entity.Slug = model.Slug;
                    entity.IsShow = model.IsShow;
                    var cultureInfo = new CultureInfo("uk-UA");
                    entity.DateTimePublish = DateTime.SpecifyKind(DateTime.Parse(model.DateTimePublish, cultureInfo), DateTimeKind.Utc);
                    string fileName = String.Empty;
                    if (!string.IsNullOrEmpty(model.Image))
                    {
                        string randomFilename = entity.Image;
                        string pathSaveImages = InitStaticFiles
                        .CreateImageByFileName(_env, _configuration,
                            new string[] { "Folder" },
                            randomFilename, model.Image, false, false);
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
