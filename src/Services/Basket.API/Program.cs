using Basket.API.GrpsServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisBasketDB");
});

// Register BasketRepository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Register DiscountGrpcService
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcConfigs:DiscountGrpcUrl"]);
});

// Register DiscountGrpcService
builder.Services.AddScoped<DiscountGrpcService>();


builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policy => policy.AllowAnyOrigin()  // Allows requests from any origin
//                        .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
//                        .AllowAnyHeader()); // Allows all headers
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
