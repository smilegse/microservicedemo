using Catalog.API.HostingService;
using Catalog.API.Interfaces.Manager;
using Catalog.API.Manager;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();

builder.Services.AddHostedService<AppHostedService>();

builder.Services.AddScoped<IProductManager, ProductManager>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()  // Allows requests from any origin
                        .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
                        .AllowAnyHeader()); // Allows all headers
});


var app = builder.Build();

//app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Catalog.API V1");
    });
}
else
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Catalog.API V1");
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
