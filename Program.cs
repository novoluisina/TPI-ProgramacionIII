using Microsoft.EntityFrameworkCore;
using TPI_ProgramacionIII.DBContexts;
using TPI_ProgramacionIII.Repository.Implementations;
using TPI_ProgramacionIII.Repository.Interfaces;
using TPI_ProgramacionIII.Services.Implementations;
using TPI_ProgramacionIII.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ECommerceContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["DB:ConnectionString"]));


//regionRepositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//endregion


//#regionServices
builder.Services.AddScoped<IProductService, ProductService>();

//#endregion

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
