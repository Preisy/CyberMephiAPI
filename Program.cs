using Microsoft.AspNetCore.Identity; //
// using JWTAuthentication.NET6._0.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using AspNetCore.Identity.MongoDbCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CyberMephiAPI.Models;
using CyberMephiAPI.Settings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

// Add services to the container.

// For Identity
builder.Services.AddIdentity<UserModelDao, RoleModel>()
    .AddMongoDbStores<UserModelDao, RoleModel, Guid>
    (
        mongoDbSettings.ConnectionString, mongoDbSettings.Name
    );

// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// Adding Jwt Bearer
    .AddJwtBearer(options => {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters() {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();