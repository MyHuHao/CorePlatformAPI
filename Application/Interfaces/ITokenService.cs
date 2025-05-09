using System.Security.Claims;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim> claims);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}