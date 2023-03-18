namespace PollutionPatrol.API.Controllers;

[ApiController]
[Authorize]
public sealed class PollutionReportController : ApiController
{
    private readonly IPollutionModule _pollutionModule;

    public PollutionReportController(IPollutionModule pollutionModule) => _pollutionModule = pollutionModule;

    [HttpPost("api/pollution-reports")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ReportPollutionAsync([FromBody] ReportPollutionRequest request)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new ReportPollutionCommand(request.PollutionType, request.Longitude, request.Latitude));

        return Ok(reportDto);
    }

    [HttpPost("api/pollution-reports/{id:guid}/evidence")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadPollutionEvidenceFileAsync([FromRoute] Guid id, IFormFile uploadedFile)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new UploadPollutionEvidenceFileCommand(id, uploadedFile));

        return Ok(reportDto);
    }
}