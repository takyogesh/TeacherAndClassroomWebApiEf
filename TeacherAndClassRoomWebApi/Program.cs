
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TeachersAndClassroomDll;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TeacherAndClassroomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection") ?? throw new InvalidOperationException("Connection string 'Connection' not found.")));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
