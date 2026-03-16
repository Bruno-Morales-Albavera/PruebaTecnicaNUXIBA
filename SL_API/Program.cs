using BL;
using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<CcenterRiaContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

// Inyección de capas
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<LoginBusiness>();
builder.Services.AddScoped<SL_API.LoginService>();

// CSV
builder.Services.AddScoped<CsvRepository>();
builder.Services.AddScoped<CsvBusiness>();
builder.Services.AddScoped<SL_API.CsvService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();