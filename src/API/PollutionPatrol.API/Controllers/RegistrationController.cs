namespace PollutionPatrol.API.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class RegistrationController : ApiController
{
    private readonly IUserAccessModule _userAccessModule;

    public RegistrationController(IUserAccessModule userAccessModule) => _userAccessModule = userAccessModule;

    [HttpPost("api/registration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterNewUserAsync([FromBody] RegistrationRequest request)
    {
        await _userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(request.Email, request.Password));
        return Ok();
    }

    [HttpPatch("api/registration/confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ConfirmRegistrationAsync([FromQuery] string confirmationToken)
    {
        await _userAccessModule.ExecuteCommandAsync(new ConfirmRegistrationCommand(confirmationToken));
        return Ok();
    }
}