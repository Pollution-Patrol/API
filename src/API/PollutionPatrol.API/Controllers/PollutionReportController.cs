namespace PollutionPatrol.API.Controllers;

[ApiController]
[Authorize]
public sealed class PollutionReportController : ApiController
{
    private readonly IReportModule _reportModule;

    public PollutionReportController(IReportModule reportModule) => _reportModule = reportModule;

    [HttpPost("api/pollutions/report")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ReportPollutionAsync([FromBody] ReportPollutionRequest request)
    {
        var reportDto = await _reportModule.ExecuteCommandAsync(
            new ReportPollutionCommand(request.PollutionType, request.Longitude, request.Latitude));

        return Ok(reportDto);
    }

    [HttpPost("api/pollutions/report/{id:guid}/evidence")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadPollutionEvidenceFileAsync([FromRoute] Guid id, IFormFile uploadedFile)
    {
        var reportDto = await _reportModule.ExecuteCommandAsync(
            new UploadPollutionEvidenceFileCommand(id, uploadedFile));

        return Ok(reportDto);
    }
}