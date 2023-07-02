using Api.Extensions;
using Api.Swagger;
using Application;
using Application.Interfaces;
using Interfaces.Mapper;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("Moderators", policyBuilder => policyBuilder.RequireRole("Moderator"));
    opt.AddPolicy("Business", policyBuilder => policyBuilder.RequireRole("Business"));
});

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddAutoMapper(expression => {
    expression.AddProfile(new AssemblyProfile(Assembly.GetExecutingAssembly()));
    expression.AddProfile(new AssemblyProfile(typeof(ICamelotDbContext).Assembly));
});

builder.Services.AddMemoryCache();

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("Ships", new OpenApiInfo{Title = "Ships", Version = "v1"});
    c.SwaggerDoc("Trips", new OpenApiInfo{Title = "Trips", Version = "v2"});
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(
c => {
    c.SwaggerEndpoint("/swagger/Ships/swagger.json", "Ships");
    c.SwaggerEndpoint("/swagger/Trips/swagger.json", "Trips");
});

app.UseCors(x=>x.WithOrigins());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSession();

app.Run();
