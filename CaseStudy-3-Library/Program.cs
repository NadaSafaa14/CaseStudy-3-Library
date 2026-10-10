using CaseStudy_3_Library.Data;
using CaseStudy_3_Library.Repos.Implementations;
using CaseStudy_3_Library.Repos.Interfaces;
using CaseStudy_3_Library.UoW.Implementation;
using CaseStudy_3_Library.UoW.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


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

builder.Services.AddScoped<IUserRepo, UserRepo>(); 

builder.Services.AddScoped<IAuthRepo, AuthRepo>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,

        ValidIssuer = builder.Configuration["JWT:Issuer"],

        ValidateAudience = true,

        ValidAudience = builder.Configuration["JWT:Audience"],

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                builder.Configuration["JWT:SecretKey"]!
            )
        ),

        //NameClaimType = ClaimTypes.Name,
        //RoleClaimType = ClaimTypes.Role,

        //ClockSkew = TimeSpan.Zero
    };
});



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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
