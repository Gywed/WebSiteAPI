using System.Text;
using Application.Services;
using Application.UseCases.Administrator.Employe;
using Application.Services.Article;
using Application.Services.Brand;
using Application.Services.Category;
using Application.Services.Order;
using Application.UseCases.Administrator.Article;
using Application.UseCases.Client;
using Application.UseCases.Employe;
using Application.UseCases.Guest;
using Application.UseCases.Guest.Dtos;
using Infrastructure.Ef;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApiTakeAndDash;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["Token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddScoped<IConnectionStringProvider,ConnectionStringProvider>();
builder.Services.AddScoped<TakeAndDashContextProvider>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IBrandService,BrandService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository,BrandRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policyBuilder =>
    {
        policyBuilder
            .WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Use case users
builder.Services.AddScoped<UseCaseSignUp>();
builder.Services.AddScoped<UseCaseLogIn>();
builder.Services.AddScoped<UseCaseCreateEmploye>();
builder.Services.AddScoped<UseCaseDeleteEmploye>();
builder.Services.AddScoped<UseCaseUpdateEmploye>();
builder.Services.AddScoped<UseCaseConsultOrderContent>();
builder.Services.AddScoped<UseCaseConsultOrderOnDate>();
builder.Services.AddScoped<UseCaseFetchAllArticle>();
builder.Services.AddScoped<UseCaseFetchPaginationEmployee>();
builder.Services.AddScoped<UseCaseConsultOrderByUser>();
builder.Services.AddScoped<UseCaseSearchArticle>();
builder.Services.AddScoped<UseCaseConsultOrderByBothDateAndUser>();
builder.Services.AddScoped<UseCaseConsultOrderByCategory>();
builder.Services.AddScoped<UseCaseCreateArticle>();
builder.Services.AddScoped<UseCaseDeleteArticle>();
builder.Services.AddScoped<UseCaseUpdateArticle>();
builder.Services.AddScoped<UseCaseCreateOrderContent>();
builder.Services.AddScoped<UseCaseFetchAllCategories>();
builder.Services.AddScoped<UseCaseFetchAllBrands>();
builder.Services.AddScoped<UseCaseFetchArticleById>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("Dev");

app.MapControllers();

app.Run();
