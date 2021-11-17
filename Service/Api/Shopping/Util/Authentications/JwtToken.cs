using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using InteliSystem.InteliMarketPlace.Applications.LoginsApp.Classes;
using InteliSystem.InteliMarketPlace.Applications.SessionsApp;
using InteliSystem.Utils.Extensions;
using Microsoft.IdentityModel.Tokens;
using Shopping.Models;

namespace Shopping.Util.Authentications
{
    public class JwtToken
    {
        public static string GetToken(User user)
        {
            var myclaims = new Claim[]
                {
                    new Claim(ClaimTypes.Actor, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.EMail),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Device", user.Device),
                    new Claim(ClaimTypes.Role, "customer")
                };

            return GetTokenCalims(myclaims);
        }

        public static string GetToken(IEnumerable<Claim> claims)
        {
            return GetTokenCalims(claims);
        }

        public static bool IsExpiredToken(string token)
        {
            var jwthandler = new JwtSecurityTokenHandler();
            var jwtoken = jwthandler.ReadToken(token);
            var expireDate = jwtoken.ValidTo;

            return (expireDate < DateTime.Now);
        }

        private static string GetTokenCalims(IEnumerable<Claim> claims)
        {
            var handle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtKey.Key);
            var tokendecriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), "HS512"),

            };

            var token = handle.CreateToken(tokendecriptor);
            return handle.WriteToken(token);
        }

        public static ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtKey.Key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals("HS512", StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }

        public static string GetRefreshToken()
        {
            var key = $"{Guid.NewGuid().ObjectToString()}-{Guid.NewGuid().ObjectToString()}";
            return key.ToBase64();
        }
    }
}