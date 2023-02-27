using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;  
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    
    public class LoginController : Controller
    {
        private IConfiguration _config;  
        private TaskManagerContext _db;

        public LoginController(IConfiguration config,TaskManagerContext db){
            _config= config;
            _db=db;
            
        }
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            List<Employee> employeeList = new List<Employee>();  
            
                employeeList = _db.Set < Employee > ().ToList(); 
                foreach (var item in employeeList)
                {
                    
                }
                // var user = employeeList.FirstOrDefault(x => x.Email.Equals("akansha@deloitte.com"));
                // Console.WriteLine(user?.Password) ;
                // Console.WriteLine("bye password") ;
               
            

            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                  var obj = employeeList.FirstOrDefault(x => x.Email.Equals(loginDTO.Email) && x.Password.Equals(loginDTO.Password));
                if(employeeList.Any(cus => loginDTO.Email.Contains(cus.Email)) && employeeList.Any(cus => loginDTO.Password.Contains(cus.Password)) && obj.Designation.Equals("ADMIN") )
                {
                  
                   Console.WriteLine("good") ;
                      Console.WriteLine(loginDTO.Email) ;
                       var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7261",  
                        "https://localhost:7261", 
                        claims:new List<Claim>(){new Claim("roles","admin")},
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }

                 if(employeeList.Any(cus => loginDTO.Email.Contains(cus.Email)) && employeeList.Any(cus => loginDTO.Password.Contains(cus.Password)) && obj.Designation.Equals("SDE") )
                {
                  
                       var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7261",  
                        "https://localhost:7261", 
                        claims:new List<Claim>(){new Claim("roles","user")},
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }

                  if(employeeList.Any(cus => loginDTO.Email.Contains(cus.Email)) && employeeList.Any(cus => loginDTO.Password.Contains(cus.Password)) && obj.Designation.Equals("MANAGER") )
                {
                  
                       var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7261",  
                        "https://localhost:7261", 
                        claims:new List<Claim>(){new Claim("roles","manager")},
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
                if (loginDTO.Email.Equals("krishna@deloitte.com") &&
                loginDTO.Password.Equals("krishna@123"))
                {
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7261",  
                        "https://localhost:7261", 
                        claims:new List<Claim>(){new Claim("roles","admin")},
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }
    }