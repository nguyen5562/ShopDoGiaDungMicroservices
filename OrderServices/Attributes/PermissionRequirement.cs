using Microsoft.AspNetCore.Authorization;

namespace OrderServices.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string FunctionCode { get; }
        public string ActionCode { get; }

        public PermissionRequirement(string functionCode, string actionCode)
        {
            FunctionCode = functionCode;
            ActionCode = actionCode;
        }
    }
}
