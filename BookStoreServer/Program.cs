using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using BookStoreServer.Repository;
using CartStoreServer.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BookStoreServer.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure form options to allow large files and multipart requests
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
    options.ValueLengthLimit = int.MaxValue;
});



builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    //options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //options.JsonSerializerOptions.DictionaryKeyPolicy = null;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
    });
});

#region Dependency Injection
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IRepository<CartDetail>, CartDetailRepository>();
builder.Services.AddScoped<IRepository<OrderDetail>, OrderDetailRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
builder.Services.AddScoped<IRepository<Refund>, RefundRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<OrderTbl>, OrderTblRepository>();
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IAccountService, AccountServices>();

#endregion


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
