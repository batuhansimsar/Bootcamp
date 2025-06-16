using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bootcamp.Entities;

namespace Bootcamp.Core.Security
{
    public class JwtHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>() ?? new TokenOptions
            {
                Audience = "bootcamp-audience",
                Issuer = "bootcamp-issuer",
                AccessTokenExpiration = 60,
                SecurityKey = "this-is-a-very-secure-key-for-jwt-authentication-in-our-bootcamp-application"
            };
        }

        public string CreateToken(User user)
        {
            return CreateToken(user.Id, user.Email, user.FirstName, user.LastName, user.UserType);
        }

        public string CreateToken(int userId, string email, string firstName, string lastName, string userType)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey ?? "default-key"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            
            var jwt = CreateJwtSecurityToken(
                userId,
                email,
                firstName,
                lastName,
                userType,
                signingCredentials);
                
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            
            return token;
        }

        private JwtSecurityToken CreateJwtSecurityToken(
            int userId, 
            string email, 
            string firstName, 
            string lastName, 
            string userType, 
            SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer ?? "bootcamp-issuer",
                audience: _tokenOptions.Audience ?? "bootcamp-audience",
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(userId, email, firstName, lastName, userType),
                signingCredentials: signingCredentials
            );
            
            return jwt;
        }

        private static IEnumerable<Claim> SetClaims(int userId, string email, string firstName, string lastName, string userType)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            claims.Add(new Claim(ClaimTypes.Name, $"{firstName} {lastName}"));
            claims.Add(new Claim(ClaimTypes.Role, userType));
            
            return claims;
        }
    }
} 