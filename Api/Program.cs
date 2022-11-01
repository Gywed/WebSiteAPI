using Application.UseCases.Guest;
using Infrastructure.Ef;
using Infrastructure.Utils;
using WebApiTakeAndDash;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConnectionStringProvider,ConnectionStringProvider>();
builder.Services.AddScoped<TakeAndDashContextProvider>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Dev");

app.MapControllers();

app.Run();