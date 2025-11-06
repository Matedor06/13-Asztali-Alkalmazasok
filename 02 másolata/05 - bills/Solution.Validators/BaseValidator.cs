using Microsoft.AspNetCore.Http;

namespace Solution.Validators;

public abstract class BaseValidator<T>(IHttpContextAccessor httpContextAccessor) : AbstractValidator<T> where T : class
{


    private string RequestMethod = httpContextAccessor?.HttpContext?.Request?.Method;

    protected bool IsPutMethod => RequestMethod is not null && HttpMethods.IsPut(RequestMethod);


}
