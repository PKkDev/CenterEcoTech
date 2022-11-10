using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
using CenterEcoTech.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"privatesettings.{builder.Environment.EnvironmentName}.json", false, true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
#region add JWT authenticaton

var key = builder.Configuration["AuthOptions:TokenKey"];
var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
var issuer = builder.Configuration["AuthOptions:Issuer"];
var audience = builder.Configuration["AuthOptions:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _key,
        };
        opt.Events = new JwtBearerEvents()
        {
            OnChallenge = (context) => { return Task.CompletedTask; },
            OnForbidden = (context) => { return Task.CompletedTask; }
        };
    });

#endregion add JWT authenticaton
#region add swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CenterEcoTech.API",
        Version = "v1",
        Description = "A simple ASP.NET Core Web API",
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

#endregion add swagger

#region add context

builder.Services.AddDbContext<AppDataBaseContext>(options =>
{
    var conStr = builder.Configuration.GetConnectionString("AppConnectionString");
    options.UseSqlServer(conStr);
});
builder.Services.AddTransient<IJWTGenerator, JWTGenerator>();
builder.Services.AddTransient<IClient, ClientService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".CenterEcoTech.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});
#region проверка токена
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 6.0 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});
#endregion
//using ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
//var context = serviceProvider.GetRequiredService<AppDataBaseContext>();
//context.Database.Migrate();

#endregion add context

#region add cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder
        => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

#endregion add cors

#region add spa

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "clientWebApp";
});

#endregion add spa

var app = builder.Build();


#region use spa

app.UseSpaStaticFiles();

#endregion use spa

app.UseExceptionHandler("/error");
// app.UseDeveloperExceptionPage();

#region use swagger

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    var path = builder.Environment.IsEnvironment("LocalDev") || builder.Environment.IsDevelopment()
    ? "/swagger/v1/swagger.json"
    : "/CenterEcoTechService/swagger/v1/swagger.json";
    c.SwaggerEndpoint(path, "CenterEcoTech.API v1");
});

#endregion

#region use cors

app.UseCors("CorsPolicy");

#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSession();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "../center-eco-tech-client";
});

app.Run();
