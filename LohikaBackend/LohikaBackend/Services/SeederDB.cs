using Data.LohikaBackend;
using Data.LohikaBackend.Entities;
using Data.LohikaBackend.Entities.Identity;
using LohikaBackend.Constants;
using LohikaBackend.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LohikaBackend.Services
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                context.Database.Migrate();
                IWebHostEnvironment _env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                IConfiguration _configuration= scope.ServiceProvider.GetRequiredService<IConfiguration>(); ;
                if (!managerRole.Roles.Any())
                {
                    var result = managerRole.CreateAsync(new AppRole
                    {
                        Name = Roles.Admin
                    }).Result;

                    result = managerRole.CreateAsync(new AppRole
                    {
                        Name = Roles.User
                    }).Result;
                }
                if(!manager.Users.Any())
                {
                    string email = "novakvova@gmail.com";
                    var user = new AppUser
                    {
                        Email = email,
                        UserName = email,
                        Photo = "fgbugdqn.bdv.jpeg",
                        PhoneNumber = "+11(111)111-11-11"
                    };
                    var result = manager.CreateAsync(user, "Qwerty1-").Result;
                    result = manager.AddToRoleAsync(user, Roles.Admin).Result;
                }
                if(!context.OrderStatuses.Any())
                {
                    context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "Новий заказ"
                        });
                    context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "В роботі"
                        });
                    context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "Відправлено"
                        });
                    context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "Прибув"
                        });
                    context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "Відхилено"
                        });
                    context.OrderStatuses
                        .Add(new OrderStatusEntity
                        {
                            Name = "Отримано"
                        });
                    context.SaveChanges();
                }
                //if(!context.Categories.Any())
                //{
                //    if (model.Image != null)
                //    {
                //        string randomFilename = Path.GetRandomFileName() +
                //            ".jpeg";
                //        string pathSaveImages = InitStaticFiles
                //            .CreateImageByFileName(_env, _configuration,
                //                new string[] { "Folder" },
                //                randomFilename, model.Image, false, false);

                //        entity.Image = randomFilename;
                //    }
                //    _context.Categories.Add(entity);
                //    await _context.SaveChangesAsync();
                //}

            }
        }
    }
}
