using AuthenticationOne.DBContext;
using AuthenticationOne.Helper.Profiles;
using AuthenticationOne.Helper.Static;
using AuthenticationOne.Interfaces.IRepositories;
using AuthenticationOne.Interfaces.IService;
using AuthenticationOne.Models;
using AuthenticationOne.Repositories;
using AuthenticationOne.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Automapper Configuration
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// add dependancy
builder.Services.AddScoped<IAuthReposirory, AuthRepository>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<ICompanyReposirory, CompanyRepository>();
builder.Services.AddScoped<ICompanyServices, CompanyServices>();

//Register DbContext
var ConnectionString = builder.Configuration.GetConnectionString("AuthDbConnection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(ConnectionString));


//Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>()
   .AddDefaultTokenProviders();

//create validation params
var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidAudience = builder.Configuration["JWT:Audience"],
    ValidIssuer= builder.Configuration["JWT:Issuer"],
    ClockSkew=TimeSpan.Zero
};

//Inject Params
builder.Services.AddSingleton(tokenValidationParams);

//Add Authentication and JWTBearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt => 
{
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters= tokenValidationParams;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme="bearer",
        BearerFormat="JWT",
        Description="Standard Authentication header using the Bearer scheme (\"{token}\")",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Name="JWT Authentication",
        Reference= new OpenApiReference
        {
            Id=JwtBearerDefaults.AuthenticationScheme,
            Type=ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        // this is an anonims obj
        {
            jwtSecurityScheme,
            Array.Empty<string>()
        }
    });
}
    
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//cors
app.UseCors(options =>
{
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
// create scope for generate roles
using var scope =app.Services.CreateScope();
await UserRoleInitializer.RoleInitialize(scope,builder.Configuration);

//
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
