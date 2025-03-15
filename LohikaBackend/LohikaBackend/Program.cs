using Data.LohikaBackend;
using Data.LohikaBackend.Entities.Identity;
using FluentValidation.AspNetCore;
using LohikaBackend.Abastract;
using LohikaBackend.Mapper;
using LohikaBackend.Models;
using LohikaBackend.Services;
using LohikaBackend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppEFContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<AppEFContext>().AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();

var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<String>("JwtKey")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signinKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
})
.AddGoogle(opt =>
{
    //opt.ClientId = "62751843627-3hvrb4vhojmd60im3q708b1usgoob3ka.apps.googleusercontent.com";
    opt.ClientId = "796507215676-0vbbdt783daqei6gmmb665337eiqq6v6.apps.googleusercontent.com";
    //opt.ClientSecret = "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";
    opt.ClientSecret = "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";
    opt.SignInScheme = IdentityConstants.ExternalScheme;
});

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddAutoMapper(typeof(AppMapProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LohikaBackend", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });

builder.Services.AddCors();

var emailConfig = builder.Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

var googleAuthSettings = builder.Configuration
    .GetSection("GoogleAuthSettings")
    .Get<GoogleAuthSettings>();

builder.Services.AddSingleton(googleAuthSettings);

builder.Services.AddScoped<IEmailSender, EmailSender>();
//Recaptcha
builder.Services.AddTransient<IRecaptchaService, RecaptchaService>();

var app = builder.Build();

app.UseCors(options =>
                options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LohikaBackend v1"));
//}

app.UseAuthentication();
app.UseAuthorization();

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
});


app.MapControllers();

app.SeedData();

SendEmailService.SendInfoStartApp(builder.Configuration);

app.Run();

