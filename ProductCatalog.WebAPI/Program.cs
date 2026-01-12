using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Data;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Interfaces;
using ProductCatalog.Service.Services;
using ProductCatalog.WebAPI.DTOs.Category;
using ProductCatalog.WebAPI.DTOs.CategoryInterfaces;
using ProductCatalog.WebAPI.DTOs.Interfaces;
using ProductCatalog.WebAPI.DTOs.Product;
using ProductCatalog.WebAPI.DTOs.ProductInterfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProduct, Product>();
builder.Services.AddScoped<IProductCategory, ProductCategory>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductDto, ProductDto>();
builder.Services.AddScoped<ICreateProductDto, CreateProductDto>();
builder.Services.AddScoped<IUpdateProductDto, UpdateProductDto>();
builder.Services.AddScoped<ICategoryDto, CategoryDto>();
builder.Services.AddScoped<ICreateCategoryDto, CreateCategoryDto>();
builder.Services.AddScoped<IUpdateCategoryDto, UpdateCategoryDto>();


var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features
            .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new
        {
            error = exception?.Message
        });
    });
});

// 5️⃣ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCatalog API V1");
    });
}



app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
