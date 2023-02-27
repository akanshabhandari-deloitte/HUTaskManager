using System.Text;
using TaskManagerApi;
using TaskManagerApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

var logger=NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try{
var builder = WebApplication.CreateBuilder(args);
//    private static readonly ILog _log = LogManager.GetLogger();
// Add services to the container.
var connectionString = builder.Configuration["ConnectionString:TaskManagerDB"];//connection string from appsetting.json
builder.Services.AddDbContext<TaskManagerContext>(opts =>
                                                 opts.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<ILabelService, LabelService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


 
builder.Services.AddSwaggerGen(swagger =>  
{  
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo  
    {   
        Version= "v1",   
        Title = "JWT Token Authentication API",  
        Description="ASP.NETWeb API" });  
    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
    {  
        Name = "Authorization",  
        Type = SecuritySchemeType.ApiKey,  
        Scheme = "Bearer",  
        BearerFormat = "JWT",  
        In = ParameterLocation.Header,  
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
    });  
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement  
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
builder.Services.AddAuthentication(option =>  
            {  
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
  
            }).AddJwtBearer(options =>  
            {  
                options.TokenValidationParameters = new TokenValidationParameters  
                {  
                    ValidateIssuer = true,  
                    ValidateAudience = true,  
                    ValidateLifetime = false,  
                    ValidateIssuerSigningKey = true,  
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],  
                    ValidAudience = builder.Configuration["Jwt:Issuer"],  
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]  
                };  
            });

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
}
catch(Exception e)
{
    logger.Error(e);
}

