#region References
using Firma.Core.CustomEntities;
using Firma.Core.Interfaces;
using Firma.Core.Services;
using Firma.Insfrastructure.Entity;
using Firma.Insfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
#endregion

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
var configure = builder.Configuration;

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.Configure<PaginationOptions>(configure.GetSection("Pagination"));

builder.Services.AddDbContext<PruebaContext>(options => options.UseSqlServer(configure.GetConnectionString("Connection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
