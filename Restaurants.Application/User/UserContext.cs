using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null) return null;

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(p => p.Type == ClaimTypes.NameIdentifier)!.Value;
        var name = user.FindFirst(p => p.Type == ClaimTypes.Name)!.Value;
        var email = user.FindFirst(p => p.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(p => p.Type == ClaimTypes.Role)!.Select(p => p.Value);

        return new CurrentUser(userId, name, email, roles);
    }
}
