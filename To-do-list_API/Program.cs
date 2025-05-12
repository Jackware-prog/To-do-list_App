using Microsoft.EntityFrameworkCore;
using To_do_list_API.FIlters;
using To_do_list_Application.Interfaces;
using To_do_list_Application.Services;
using To_do_list_Infrastructure.Persistence;
using To_do_list_Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IListItemRepository, ListItemRepository>();
builder.Services.AddScoped<IListRepository, ListRepository>();

builder.Services.AddScoped<IListItemService, ListItemService>();
builder.Services.AddScoped<IListService, ListService>();



builder.Services.AddDbContext<To_do_list_DbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
