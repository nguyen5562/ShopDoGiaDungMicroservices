using Microsoft.AspNetCore.Authorization;

namespace OrderServices.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string functionCode, string actionCode)
        {
            Policy = $"{functionCode}.{actionCode}";
        }
    }
}
