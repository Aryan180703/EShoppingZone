using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EShoppingZone.Data;
using EShoppingZone.DTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EShoppingZone.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly SignInManager<UserProfile> _signInManager;
        private readonly EShoppingZoneDBContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager, EShoppingZoneDBContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(s => s.Name == registerDto.RoleName);
            if (role == null)
                throw new Exception("Invalid Role");
            var user = new UserProfile
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                MobileNumber = registerDto.MobileNumber,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await SendWelcomeEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(user, registerDto.RoleName);
            var token = await GenerateJwtToken(user, registerDto.RoleName);
            return new AuthResponseDTO { Token = token, Role = role?.Name ?? "Customer" };

        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new Exception("Invalid email or password");

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded)
                throw new Exception("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Customer";
            var token = await GenerateJwtToken(user, role); 
            return new AuthResponseDTO { Token = token, Role = role};
        }

        public async Task SendWelcomeEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"https://yourdomain.com/verify-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:FromEmail"], _configuration["Smtp:FromName"]),
                Subject = "Welcome to Eshopping Zone!",
                Body = $"<p>Dear {user.FullName},</p>" +
               "<p>Welcome to <strong>Eshopping Zone</strong>! 🎉</p>" +
               "<p>We are thrilled to have you join our shopping community. Explore amazing products and great deals!</p>" +
               "<p>Happy Shopping! 🛒</p>" +
               "<p>Best Regards,<br/>The Eshopping Zone Team</p>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
        private async Task<string> GenerateJwtToken(UserProfile user, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
