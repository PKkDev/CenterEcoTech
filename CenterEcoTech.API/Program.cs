using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"privatesettings.json", false, true);
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
            OnChallenge = (context) => Task.CompletedTask,
            OnForbidden = (context) => Task.CompletedTask
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

#region add services

builder.Services.AddTransient<IJWTGenerator, JWTGenerator>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<ICooperativeService, CooperativeService>();
builder.Services.AddTransient<ISmsAeroService, SmsAeroService>();
builder.Services.AddTransient<IClientRequestService, ClientRequestService>();
builder.Services.AddTransient<IMeasurementService, MeasurementService>();

#endregion add services

#region add context

builder.Services.AddDbContext<AppDataBaseContext>(options =>
{
    var conStr = builder.Configuration.GetConnectionString("AppConnectionString");
    options.UseSqlServer(conStr);
});

//using ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
//var context = serviceProvider.GetRequiredService<AppDataBaseContext>();
//AppDataBaseContext.SeedInitilData(context);

#endregion add context

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

#region AddMemoryCache

builder.Services.AddMemoryCache();

#endregion AddMemoryCache

#region add Swagger v2

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

#endregion  add Swagger v2

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

#region add SmsAero
builder.Services.AddHttpClient("smsAreaApi", s =>
{
    s.BaseAddress = new Uri(builder.Configuration["SmsAreaSettings:BaseUrl"]);
})
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var user = builder.Configuration["SmsAreaSettings:User"];
        var pass = builder.Configuration["SmsAreaSettings:Pass"];
        return new HttpClientHandler()
        {
            UseDefaultCredentials = true,
            Credentials = new NetworkCredential(user, pass)
        };
    });
#endregion add SmsAero

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
app.UseSession();
app.MapControllers();


app.UseSpa(spa =>
{
    spa.Options.SourcePath = "../center-eco-tech-client";
});

app.Run();
