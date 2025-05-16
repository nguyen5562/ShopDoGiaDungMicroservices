using Microsoft.AspNetCore.Authorization;

namespace AccountServices.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string functionCode, string actionCode)
        {
            Policy = $"{functionCode}.{actionCode}";
        }
    }
}
