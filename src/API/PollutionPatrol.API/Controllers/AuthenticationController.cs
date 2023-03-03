namespace PollutionPatrol.API.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class AuthenticationController : ApiController
{
    [HttpPost("api/authentication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AuthenticateUserAsync([FromBody] AuthenticateUserRequest request)
    {
        var authenticationResult = await Mediator.Send(new AuthenticateUserCommand(request.Email, request.Password));
        return Ok(authenticationResult);
    }
}