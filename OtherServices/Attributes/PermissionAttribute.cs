using Microsoft.AspNetCore.Authorization;

namespace OtherServices.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string functionCode, string actionCode)
        {
            Policy = $"{functionCode}.{actionCode}";
        }
    }
}
