using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
       {
           policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
           .AllowAnyHeader()
           .AllowAnyMethod();
       });
});

// Connection string’i appsettings.json’dan alacak şekilde Entity Framwoek kaydı
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Dependency Injection (DI) ↑

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

////  Veritabanı otomatik oluşturulsun
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<Context>();
//    db.Database.Migrate();
//}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();   

app.Run();
