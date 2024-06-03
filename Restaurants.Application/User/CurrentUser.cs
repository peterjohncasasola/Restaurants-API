namespace Restaurants.Application.User;

public record CurrentUser(string Id, string Name, string Email, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
