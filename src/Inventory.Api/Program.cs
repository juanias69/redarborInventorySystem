using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.IServices;
using Inventory.Application.Queries.Categories.Handler;
using Inventory.Application.Queries.Inventories.Handler;
using Inventory.Application.Queries.Products.Handler;
using Inventory.Application.Services;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes("systemInventoryProject_redarbor.2024");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, 
            ValidateAudience = false, 
            ValidateLifetime = true 
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});
builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IInventoryAppService, InventoryAppService>();
builder.Services.AddMediatR(typeof(GetAllProductsQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllCategoriesQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllInventoryQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<IInventoryCommandRepository, InventoryCommandRepository>();
builder.Services.AddScoped<IInventoryQueryRepository, InventoryQueryRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory System", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticación JWT usando el esquema Bearer. \n\n" +
                      "Ingrese 'Bearer' [espacio] y luego el token en el campo de texto.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId("your-client-id");
        c.OAuthClientSecret("your-client-secret");
        c.OAuthUsePkce();
    });
}

app.UseAuthentication();
app.UseAuthorization();

#region Products

app.MapGet("/products", [Authorize] async (IProductAppService productService) =>
{
    var result = await productService.GetAllProductsAsync();
    return Results.Ok(result);
});

app.MapGet("/products/{id:int}", async (int id, IProductAppService productService) =>
{
    var result = await productService.GetProductByIdAsync(id);
    return Results.Ok(result);
}).WithName("GetAllProducts").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Products" } });

app.MapPost("/products", async (ProductDto obj, IProductAppService productService) =>
{
    var result = await productService.AddProductAsync(obj);
    return Results.Ok(result);
}).WithName("CreateNewProduct").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Products" } });

app.MapPut("/products", async (ProductDto obj, IProductAppService productService) =>
{
    var result = await productService.UpdateProductAsync(obj);
    return Results.Ok(result);
}).WithName("UpdateProduct").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Products" } });

app.MapDelete("/products/{id:int}", async (int id, IProductAppService productService) =>
{
    var result = await productService.DeleteProductAsync(id);
    return Results.Ok(result);
}).WithName("DeleteProduct").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Products" } });

#endregion

#region Categories

app.MapGet("/categories", async (ICategoryAppService categoryAppService) =>
{
    var result = await categoryAppService.GetAllCategoriesAsync();
    return Results.Ok(result);
}).WithName("GetAllCategories").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Categories" } });

app.MapPost("/categories", async (CategoryDto obj, ICategoryAppService categoryAppService) =>
{
    var result = await categoryAppService.AddCategoryAsync(obj);
    return Results.Ok(result);
}).WithName("CreateCategory").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Categories" } });

app.MapDelete("/categories/{id:int}", async (int id, ICategoryAppService categoryAppService) =>
{
    var result = await categoryAppService.DeleteCategoryAsync(id);
    return Results.Ok(result);
}).WithName("DeleteCategory").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Categories" } });

#endregion

#region Inventories

app.MapPost("/Inventories", async (InventoryDto obj, IInventoryAppService inventoryAppService) =>
{
    var result = await inventoryAppService.AddInventoryAsync(obj);
    return Results.Ok(result);
}).WithName("AddToInventory").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Inventories" } });

app.MapPut("/Inventories", async (InventoryDto obj, IInventoryAppService inventoryAppService) =>
{
    var result = await inventoryAppService.UpdateInventoryAsync(obj);
    return Results.Ok(result);
}).WithName("UpdateInventory").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Inventories" } });

app.MapGet("/Inventories", async (IInventoryAppService inventoryAppService) =>
{
    var result = await inventoryAppService.GetAllInventoriesAsync();
    return Results.Ok(result);
}).WithName("GetAllInventory").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Inventories" } });

app.MapGet("/Inventories/{id:int}", async (int id, IInventoryAppService inventoryAppService) =>
{
    var result = await inventoryAppService.GetInventoryByProductIdAsync(id);
    return Results.Ok(result);
}).WithName("GetInventaryByProductId").WithMetadata(new SwaggerOperationAttribute { Tags = new[] { "Inventories" } });

#endregion

#region Security

app.MapPost("/generate-token", () =>
{
    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes("systemInventoryProject_redarbor.2024");

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "user_test"),
            new Claim(ClaimTypes.Name, "userTest")
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);

    return Results.Ok(new { token = tokenString });
});

#endregion

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

app.Run();
