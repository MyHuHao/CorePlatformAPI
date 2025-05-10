using System.Security.Claims;

namespace Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim> claims);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}