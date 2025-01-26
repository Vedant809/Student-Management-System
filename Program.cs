using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Interface;
using StudentManagementSystem.Middleware;
using StudentManagementSystem.Repository;
using StudentManagementSystem.Service;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("dbconnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<APIDbContext>(options =>
options.UseSqlServer(connection));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<Logging>();

app.Run();
