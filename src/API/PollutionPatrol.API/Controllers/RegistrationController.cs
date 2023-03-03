namespace PollutionPatrol.API.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class RegistrationController : ApiController
{
    [HttpPost("api/registration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterNewUserAsync([FromBody] RegistrationRequest request)
    {
        await Mediator.Send(new RegisterNewUserCommand(request.Email, request.Password));
        return Ok();
    }

    [HttpPatch("api/registration/confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ConfirmRegistrationAsync([FromQuery] string confirmationToken)
    {
        await Mediator.Send(new ConfirmRegistrationCommand(confirmationToken));
        return Ok();
    }
}