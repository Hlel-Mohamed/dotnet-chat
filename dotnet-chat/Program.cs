using dotnet_chat.Data;
using dotnet_chat.Helpers;
using Microsoft.EntityFrameworkCore;

// Create a new WebApplication builder with the provided command-line arguments.
var builder = WebApplication.CreateBuilder(args);

// Get the Configuration object from the builder.
IConfiguration configuration = builder.Configuration;

// Add services to the container.

// Add SwaggerGen to the service collection.
builder.Services.AddSwaggerGen();

// Add CORS services to the service collection.
builder.Services.AddCors();

// Add a DbContext of type UserContext to the service collection.
// Configure it to use MySQL with the connection string from the configuration.
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));

// Add the MVC services to the service collection.
builder.Services.AddControllers();

// Add a scoped service of type IUserRepository with an implementation type UserRepository to the service collection.
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add a scoped service of type JwtService to the service collection.
builder.Services.AddScoped<JwtService>();

// Add API explorer services to the service collection.
builder.Services.AddEndpointsApiExplorer();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.

// If the application is in development mode...
if (app.Environment.IsDevelopment()) {
    // ...use the Swagger middleware to serve generated Swagger as a JSON endpoint,
    app.UseSwagger();
    // ...and use the Swagger UI middleware to serve the Swagger UI.
    app.UseSwaggerUI();
}

// Use HTTPS redirection middleware to redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Use CORS middleware to allow cross-origin requests from specified origins.
app.UseCors(options => options
    .WithOrigins("http://localhost:3000","http://localhost:8080","http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

// Use authorization middleware to evaluate Authorization headers in the HTTP context.
app.UseAuthorization();

// Use the routing middleware to route requests to the appropriate controller action methods.
app.MapControllers();

// Run the application.
app.Run();