using System.Text;
using Application.Services;
using Application.UseCases.Administrator.Employe;
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
builder.Services.AddScoped<UseCaseFetchAllEmploye>();
builder.Services.AddScoped<UseCaseDeleteEmploye>();
builder.Services.AddScoped<UseCaseFetchPaginationEmployee>();

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