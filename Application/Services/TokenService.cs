using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.UseCases.Guest.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService
{
    private const double EXPIRY_DURATION_MINUTES = 30;
    
    public string BuildToken(string key, string issuer, DtoUser user)
    {
        var claims = new[] {    
            new Claim(ClaimTypes.Name, user.Email!),
            new Claim(ClaimTypes.Role, user.Role!),
            new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString()),
            new Claim("Id",user.Id.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);           
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, 
            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor); 
    }
}