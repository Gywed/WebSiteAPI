using System.Text;
using Application.Services;
using Application.UseCases.Administrator.Employe;
using Application.Services.Article;
using Application.Services.Brand;
using Application.Services.Category;
using Application.Services.Order;
using Application.UseCases.Administrator.Admin;
using Application.UseCases.Administrator.Article;
using Application.UseCases.Administrator.Family;
using Application.UseCases.Client;
using Application.UseCases.Employe;
using Application.UseCases.Guest;
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
builder.Services.AddScoped<IFamilyRepository,FamilyRepository>();

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
builder.Services.AddScoped<UseCaseCreateAdmin>();
builder.Services.AddScoped<UseCaseDeleteEmploye>();
builder.Services.AddScoped<UseCaseUpdateEmploye>();
builder.Services.AddScoped<UseCaseFetchPaginationEmployee>();
builder.Services.AddScoped<UseCaseFetchUsernameByEmail>();

// Use case orders
builder.Services.AddScoped<UseCaseConsultOrderContent>();
builder.Services.AddScoped<UseCaseConsultOrderOnDate>();
builder.Services.AddScoped<UseCaseConsultOrderByUserName>();
builder.Services.AddScoped<UseCaseConsultOrderByBothDateAndUser>();
builder.Services.AddScoped<UseCaseConsultOrderByCategory>();
builder.Services.AddScoped<UseCaseCreateOrderContent>();
builder.Services.AddScoped<UseCaseConsultOrderByUserId>();
builder.Services.AddScoped<UseCasePrepareOrder>();
builder.Services.AddScoped<UseCaseCancelOrder>();

// Use case article
builder.Services.AddScoped<UseCaseFetchAllArticle>();
builder.Services.AddScoped<UseCaseSearchArticle>();
builder.Services.AddScoped<UseCaseCreateArticle>();
builder.Services.AddScoped<UseCaseDeleteArticle>();
builder.Services.AddScoped<UseCaseUpdateArticle>();
builder.Services.AddScoped<UseCaseFetchAllCategories>();
builder.Services.AddScoped<UseCaseFetchAllBrands>();
builder.Services.AddScoped<UseCaseFetchArticleById>();
builder.Services.AddScoped<UseCaseFetchArticleByCategory>();
builder.Services.AddScoped<UseCaseFetchArticleByCategoryAndFilter>();
builder.Services.AddScoped<UseCaseUpdatePreparedArticle>();
builder.Services.AddScoped<UseCaseCreateFamily>();
builder.Services.AddScoped<UseCaseDeleteFamily>();
builder.Services.AddScoped<UseCaseUpdateFamily>();
builder.Services.AddScoped<UseCaseFetchFamilies>();
builder.Services.AddScoped<UseCaseAddArticleInFamily>();
builder.Services.AddScoped<UseCaseFetchArticlesOfFamily>();
builder.Services.AddScoped<UseCaseRemoveArticleFromFamily>();
builder.Services.AddScoped<UseCaseFetchFamiliesOfArticle>();
builder.Services.AddScoped<UseCaseFetchArticlesInSameFamilies>();
builder.Services.AddScoped<UseCaseFetchAllArticleFileName>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("Dev");

app.MapControllers();

app.Run();
