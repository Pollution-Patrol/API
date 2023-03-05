namespace PollutionPatrol.API.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class AuthenticationController : ApiController
{
    private readonly IUserAccessModule _userAccessModule;

    public AuthenticationController(IUserAccessModule userAccessModule) => _userAccessModule = userAccessModule;

    [HttpPost("api/authentication")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AuthenticateUserAsync([FromBody] AuthenticateUserRequest request)
    {
        var authResult = await _userAccessModule.ExecuteCommandAsync(new AuthenticateUserCommand(request.Email, request.Password));
        return Ok(authResult);
    }
}