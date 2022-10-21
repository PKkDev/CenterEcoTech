using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var path = Path.Combine(AppContext.BaseDirectory, "connectionsettings.json");
builder.Configuration.AddJsonFile(path, false, true);

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

var app = builder.Build();

app.UseExceptionHandler("/error");
// app.UseDeveloperExceptionPage();

#region use swagger

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    var path = builder.Environment.IsDevelopment() ? "/swagger/v1/swagger.json" : "/Service/swagger/v1/swagger.json";
    c.SwaggerEndpoint(path, "CenterEcoTech.API v1");
});

#endregion

#region use cors

app.UseCors("CorsPolicy");

#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
