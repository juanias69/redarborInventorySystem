

using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(AddProductCommand).Assembly);
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString); 
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Products

// 1. Get all products (GET /products)
app.MapGet("/products", async (IMediator mediator) =>
{
    var query = new GetAllProductsQuery();
    var result = await mediator.Send(query);
    return Results.Ok(result);
});

// 2. Get a product by ID (GET /products/{id})
app.MapGet("/products/{id:int}", async (int id, IMediator mediator) =>
{
    var query = new GetProductByIdQuery { Id = id };
    var result = await mediator.Send(query);
    return result != null ? Results.Ok(result) : Results.NotFound();
});

// 3. Create a new product (POST /products)
app.MapPost("/products", async (AddProductCommand command, IMediator mediator) =>
{
    var result = await mediator.Send(command);
    return Results.Ok(result); 
});

// 4. Update a product (PUT /products/{id})
app.MapPut("/products", async (UpdateProductCommand command, IMediator mediator) =>
{
    var result = await mediator.Send(command);
    return Results.Ok(result);
});

// 5. Delete a product (DELETE /products/{id})
app.MapDelete("/products/{id:int}", async (int id, IMediator mediator) =>
{
    var command = new DeleteProductCommand { Id = id };
    var result = await mediator.Send(command);
    return Results.Ok(result);
});

#endregion

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

app.Run();
