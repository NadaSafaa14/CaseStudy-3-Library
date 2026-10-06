using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Repos.Implementations;
using CaseStudy_3_Library.Repos.Interfaces;
using CaseStudy_3_Library.UoW.Implementation;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

builder.Services.AddScoped<IBookRepo, BookRepo>();

builder.Services.AddScoped<IMemberRepo, MemberRepo>();

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

builder.Services.AddScoped<IBorrowingRepo, BorrowingRepo>();

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
