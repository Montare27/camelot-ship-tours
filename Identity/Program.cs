using Identity.Data;
using Identity.Data.Wrapper;
using Identity.Models;
using Identity.Services;
using Interfaces;
using Interfaces.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("CamelotDb")));

builder.Services.AddSingleton<IPasswordHashService, PasswordHashBCryptService>();
builder.Services.AddTransient<IAuthRepository<User>, UserRepository>();
builder.Services.AddScoped<UnitOfWorkAuth>();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile(new AssemblyProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyProfile(typeof(AuthDbContext).Assembly));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
