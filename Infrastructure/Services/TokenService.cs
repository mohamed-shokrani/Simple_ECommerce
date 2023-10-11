using Core.DTOs;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<AppUser> _userManager;
    public TokenService(IConfiguration config, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
    }

  
    public string CreateToken(AppUser user)
    {
        // the token it self  will be sent with every request
        var claims = new List<Claim>
       {
           new Claim (ClaimTypes.Email,user.Email),
           new Claim (ClaimTypes.GivenName,user.UserName),
       };
        var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            //Expires = DateTime.UtcNow.AddSeconds(10),
            SigningCredentials = creds,
            Issuer = _config["Token:Issuer"]

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public async Task<UserDto> RefreshTokenAsync(string email)
    {
       
        var userDto = new UserDto();

        var user = await _userManager.FindByEmailAsync(email);

        userDto.Token = CreateToken(user);
        userDto.Email = user.Email;
        userDto.UserName = user.UserName;
        if (user == null)
        {
            userDto.Message = "Invalid token";
            return userDto;
        }

        if (user.RefreshTokens.Any())
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
            userDto.RefreshToken = activeRefreshToken.Token;
            userDto.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
        }
        else {
            var refershToken = GenerateRefreshToken();
            userDto.RefreshToken = refershToken.Token;
            userDto.RefreshTokenExpiration = refershToken.ExpiresOn;
            user.RefreshTokens.Add(refershToken);
            await _userManager.UpdateAsync(user);
        }
       

        return userDto;
    }

    private RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var generator = new RNGCryptoServiceProvider();

        generator.GetBytes(randomNumber);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTime.UtcNow.AddMinutes(1),
            CreatedOn = DateTime.UtcNow
        };
    }
}
