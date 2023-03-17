namespace PollutionPatrol.API.Controllers;

[ApiController]
[Authorize]
public sealed class PollutionController : ApiController
{
    private readonly IPollutionModule _pollutionModule;

    public PollutionController(IPollutionModule pollutionModule) => _pollutionModule = pollutionModule;

    [HttpPost("api/pollutions/report")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ReportPollutionAsync([FromBody] ReportPollutionRequest request)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new ReportPollutionCommand(request.PollutionType, request.Longitude, request.Latitude));

        return Ok(reportDto);
    }

    [HttpPost("api/pollutions/report/{id:guid}/evidence")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadPollutionEvidenceFileAsync([FromRoute] Guid id, IFormFile uploadedFile)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new UploadPollutionEvidenceFileCommand(id, uploadedFile));

        return Ok(reportDto);
    }
}