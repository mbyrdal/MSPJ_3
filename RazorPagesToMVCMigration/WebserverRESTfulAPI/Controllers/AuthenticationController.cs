using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = ServiceAPI.Models.LoginRequest;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionHelper _helper;
        private readonly string _connectionString;

        public AuthenticationController(IConfiguration configuration)
        {

            _configuration = configuration;
            _helper = new ConnectionHelper(configuration);
            _connectionString = _helper.GetDBConnectionString();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Validate user credentials against the DB using ADO.NET
            var validateUser = await ValidateUserAsync(loginRequest.Email, loginRequest.Password);
            if (!validateUser) 
            {
                return Unauthorized("Invalid email or password");
            }

            var jwtToken = GenerateJwtToken(loginRequest.Email);
            return Ok(new { token = jwtToken });
        }

        private async Task<bool> ValidateUserAsync(string email, string password)
        {
            bool res = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var userExistsQuery = "SELECT PasswordHash FROM Users WHERE " +
                                          "Email = @Email";
                    using (SqlCommand checkIfUserExistsCommand = new SqlCommand(userExistsQuery, conn))
                    {
                        checkIfUserExistsCommand.Parameters.AddWithValue("@Email", email);

                        var result = await checkIfUserExistsCommand.ExecuteScalarAsync();
                        if (result != null)
                        {
                            // Verify if stored hash password matches input
                            var storedHash = result.ToString();
                            return BCrypt.Net.BCrypt.Verify(password, storedHash);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error has occurred: {ex.Message}");
            }

            return res;
        }

        private string GenerateJwtToken(string email)
        {
            var jwtSettings = _helper.GetJwtSettings();
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10), // Token expiry
                signingCredentials: credentials);

            // Return serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestViewModel registerRequest)
        {
            if(string.IsNullOrEmpty(registerRequest.Email) || string.IsNullOrEmpty(registerRequest.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Check if the email already exists
            bool userExists = await UserExistsAsync(registerRequest.Email);

            if(userExists)
            {
                return Conflict($"User with email '{registerRequest.Email}' already exists.");
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            // Insert new User into DB
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.OpenAsync();
                var insertUserQuery = "INSERT INTO Users " +
                                      "(Email, PasswordHash, FirstName, LastName) " +
                                      "VALUES " +
                                      "(@Email, @PasswordHash, @FirstName, @LastName)";
                using(SqlCommand insertUserCommand = new SqlCommand(insertUserQuery, conn))
                {
                    insertUserCommand.Parameters.AddWithValue("@Email", registerRequest.Email);
                    insertUserCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    insertUserCommand.Parameters.AddWithValue("@FirstName", registerRequest.FirstName);
                    insertUserCommand.Parameters.AddWithValue("@LastName", registerRequest.LastName);
                    await insertUserCommand.ExecuteNonQueryAsync();
                }
            }
            return Ok("User registered successfully.");
        }

        private async Task<bool> UserExistsAsync(string email)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var userExistsQuery = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
                using (SqlCommand checkIfUserExistsCommand = new SqlCommand(userExistsQuery, conn))
                {
                    checkIfUserExistsCommand.Parameters.AddWithValue("@Email", email);

                    var result = (int) await checkIfUserExistsCommand.ExecuteScalarAsync();
                    return result > 0;
                }
            }
        }
    }
}
