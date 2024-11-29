using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using StudentManagementAPI.Services.enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddSingleton<IStudentRepository>(new CsvStudentRepository("alunos.csv"));
builder.Services.AddScoped<StudentService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Management API",
        Version = "v1",
        Description = "API para gerenciar alunos e suas informações."
    });
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "StudentManagementAPI.xml");
    options.IncludeXmlComments(xmlFile);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Permite qualquer origem
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
