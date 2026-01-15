namespace Solution.WebAPI.Controllers;

[ApiController]
[ProducesResponseType(statusCode:400, type: typeof(BadRequestObjectResult))]
[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
public class SecurityController(ISecurityService securityService) : ControllerBase
{
    [HttpPost]
    [Route("api/security/register")]
    [ProducesResponseType(type:typeof(Success), statusCode:200)]
    public async Task<IActionResult> RegisterAsync([FromBody] [Required] RegisterRequestModel model)
    {
        var result = await securityService.RegisterAsync(model);
        return result.Match(
          value => Ok(value),
          errors => errors.ToProblemResult()
        );
    }

    [HttpPost]
    [Route("api/security/login")]
    [ProducesResponseType(type:typeof(Success), statusCode:200)]
    public async Task<IActionResult> LoginAsync([FromBody] [Required] LoginRequestModel model)
    {
        var result = await securityService.LoginAsync(model);
        return result.Match(
          value => Ok(value),
          errors => errors.ToProblemResult()
        );
    }
}
