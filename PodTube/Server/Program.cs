using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using PodTube.BLL.Services;
using PodTube.DataAccess.Factory;
using AutoMapper;
using PodTube.BLL.Mapper;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Swashbuckle.Swagger;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PodTube.Shared.Models.RequestBody;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PodTube.DataAccess.Entities;
using PodTube.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.SeedDbWithData(builder.Configuration);
builder.Services.RegisterDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(DbToDtoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(RequestBodyToDbProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PagedListProfile).Assembly);

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("1.0.0", new OpenApiInfo {
        Version = "1.0.0",
        Title = "PodTube - OpenAPI 3.0",
        Description = "PodTube - OpenAPI 3.0 (ASP.NET Core 3.1)",
    });
    c.CustomSchemaIds(type => type.FriendlyId().Replace("[", "<").Replace("]", ">"));

    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
    c.MapType<VideoRequestBody>(() => new OpenApiSchema { Type = "string" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<ChannelService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PlaylistService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<FileService>();

builder.Services.Configure<KestrelServerOptions>(options => {
    options.Limits.MaxRequestBodySize = 1073741824; // 1GB
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters() {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Security:ValidIssuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Security:ValidAudience"),
            IssuerSigningKey = new SymmetricSecurityKey(
               System.IO.File.ReadAllBytes(builder.Configuration.GetValue<string>("Security:IssuerSigningKey") ?? "")
            ),
        };
    });

builder.Services
    .AddIdentityCore<User>(options => {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<PodTubeDbContext>();

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
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "PodTube - OpenAPI 3.0");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
