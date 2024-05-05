using dotnet_chat.Data;
using dotnet_chat.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get the Configuration object from the builder
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000","http://localhost:8080","http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();