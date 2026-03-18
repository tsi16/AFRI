using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Afri.Utilities
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private ClaimsPrincipal? User => _accessor.HttpContext?.User;

        public int GetUserId()
        {
            // Try claims first
            var idClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(idClaim, out var id)) return id;

            // Fallback to session
            return _accessor.HttpContext?.Session.GetInt32("UserId") ?? 0;
        }

        public string GetUserName()
        {
            var name = User?.Identity?.Name;
            if (!string.IsNullOrEmpty(name)) return name;

            return _accessor.HttpContext?.Session.GetString("UserName") ?? "Guest";
        }

        public string GetUserRole()
        {
            var roleClaim = User?.FindFirst(ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(roleClaim)) return roleClaim;

            return _accessor.HttpContext?.Session.GetString("UserRole") ?? "Student";
        }

        public bool IsInstructor()
        {
            var role = GetUserRole();
            return role.Contains("Instructor", System.StringComparison.OrdinalIgnoreCase) ||
                   role.Contains("Teacher", System.StringComparison.OrdinalIgnoreCase) ||
                   role.Contains("Admin", System.StringComparison.OrdinalIgnoreCase);
        }

        public bool IsParent()
        {
            var role = GetUserRole();
            return role.Contains("Parent", System.StringComparison.OrdinalIgnoreCase);
        }

        public bool IsStudent() => !IsInstructor() && !IsParent();
    }
}