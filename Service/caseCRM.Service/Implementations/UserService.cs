using caseCRM.DataAccess;
using caseCRM.DataAccess.Common;
using caseCRM.DataAccess.IRepository;
using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using caseCRM.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ResponseDto<string>> AuthenticateAsync(string userName, string password)
        {
            try
            {
                var user = await _userRepository.GetWhere(u => u.username == userName).Data.SingleOrDefaultAsync();

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
                {
                    return ResponseDto<string>.Fail("Invalid email or password", 401);
                }

                var token = GenerateJwtToken(user);

                return ResponseDto<string>.Success(token, "Authentication successful", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<string>.Fail(ex.Message);
            }
        }

        public async Task<ResponseDto<EmptyDto>> CreateUserAsync(User user, string password)
        {
            try
            {
                if (_userRepository.GetWhere(u => u.username == user.username).Data.FirstOrDefault() != null)
                {
                    return ResponseDto<EmptyDto>.Fail("Username is already registered", 400);
                }

                user.password = BCrypt.Net.BCrypt.HashPassword(password);

                await _userRepository.AddAsync(user);
                await _userRepository.SaveAsync();

                return ResponseDto<EmptyDto>.Success("User created successfully", 201);
            }
            catch (Exception ex)
            {
                return ResponseDto<EmptyDto>.Fail(ex.Message, 500);
            }
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, user.role)
                }),
                Expires = DateTime.UtcNow.AddHours(7),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
