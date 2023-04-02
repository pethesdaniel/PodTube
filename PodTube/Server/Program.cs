using PodTube.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using PodTube.BLL.Services;
using PodTube.DataAccess.Factory;
using AutoMapper;
using PodTube.BLL.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.SeedDbWithData(builder.Configuration);
builder.Services.RegisterDbContext(builder.Configuration);

builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<DbToDtoProfile>(); cfg.AddProfile<PagedListProfile>(); });

builder.Services.AddSwaggerGen(c => {
                    c.SwaggerDoc("1.0.0", new OpenApiInfo {
                        Version = "1.0.0",
                        Title = "PodTube - OpenAPI 3.0",
                        Description = "PodTube - OpenAPI 3.0 (ASP.NET Core 3.1)",
                    });
                    c.CustomSchemaIds(type => type.FullName);

                    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
                });

builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<ChannelService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
} else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "PodTube - OpenAPI 3.0");
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
