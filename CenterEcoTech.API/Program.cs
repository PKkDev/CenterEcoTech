using CenterEcoTech.EfData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"privatesettings.{builder.Environment.EnvironmentName}.json", false, true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "../center-eco-tech-client";
});

app.Run();
