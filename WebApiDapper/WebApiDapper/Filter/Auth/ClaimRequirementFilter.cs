using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDapper.Enums;

namespace WebApiDapper.Filter.Auth
{
    public class ClaimRequirementFilter : Attribute, IAuthorizationFilter
    {
        private readonly FunctionCode _functionCode;
        private readonly ActionCode _actionCode;
        public ClaimRequirementFilter(FunctionCode functionCode, ActionCode actionCode)
        {
            _functionCode = functionCode;
            _actionCode = actionCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Kiểm tra claim của user
            var hasRequiredClaims = user.Claims
                .GroupBy(c => c.Type)
                .Any(group =>
                    group.Key == "FunctionCode" && group.Any(c => c.Value == _functionCode.ToString("G")) &&
                    group.Key == "ActionCode" && group.Any(c => c.Value == _actionCode.ToString()));

            if (!hasRequiredClaims)
            {
                context.Result = new ForbidResult();
            }

        }
    }
}
