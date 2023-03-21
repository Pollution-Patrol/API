namespace PollutionPatrol.API.Controllers;

[ApiController]
[Authorize]
public sealed class PollutionReportController : ApiController
{
    private readonly IPollutionModule _pollutionModule;

    public PollutionReportController(IPollutionModule pollutionModule) => _pollutionModule = pollutionModule;

    [HttpPost("api/pollution-reports")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ReportPollutionAsync([FromBody] ReportPollutionRequest request)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new ReportPollutionCommand(request.PollutionType, request.Longitude, request.Latitude));

        return Ok(reportDto);
    }

    [HttpPost("api/pollution-reports/{id:guid}/evidence")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadPollutionEvidenceFileAsync([FromRoute] Guid id, IFormFile uploadedFile)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new UploadPollutionEvidenceFileCommand(id, uploadedFile));

        return Ok(reportDto);
    }

    [HttpPost("api/pollution-reports/{reportId:guid}/reviewers/{reviewerId:guid}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DesignateReportReviewerAsync([FromRoute] Guid reportId, [FromRoute] Guid reviewerId)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new DesignateReportReviewerCommand(reportId, reviewerId));

        return Ok(reportDto);
    }

    [HttpPost("api/pollution-reports/{reportId:guid}/approve")]
    [Produces(typeof(ReportDto))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ApprovePollutionReport([FromRoute] Guid reportId)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new ApprovePollutionReportCommand(reportId));

        return Ok(reportDto);
    }

    [HttpPost("api/pollution-reports/{reportId:guid}/reject")]
    [Produces(typeof(ReportDto))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RejectPollutionReport([FromRoute] Guid reportId)
    {
        var reportDto = await _pollutionModule.ExecuteCommandAsync(
            new RejectPollutionReportCommand(reportId));

        return Ok(reportDto);
    }

    [HttpGet("api/pollution-reports")]
    [Produces(typeof(ReadOnlyPagedList<ReportDto>))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ReadOnlyPagedList<ReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPollutionReportsListAsync([FromQuery] int page, [FromQuery] int size)
    {
        var reportDtosList = await _pollutionModule.ExecuteQueryAsync(
            new GetPollutionReportsQuery(page, size));

        var metadata = reportDtosList.GetPaginationMetadataJson();

        Response.Headers.Add("X-Pagination", metadata);
        return Ok(reportDtosList);
    }

    [HttpGet("api/pollution-reports/{reportId:guid}")]
    [Produces(typeof(ReportDto))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ReadOnlyPagedList<ReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPollutionReportByIdAsync([FromRoute] Guid reportId)
    {
        var reportDto = await _pollutionModule.ExecuteQueryAsync(
            new GetPollutionReportByIdQuery(reportId));

        return Ok(reportDto);
    }

    [HttpGet("api/users/{userId:guid}/pollution-reports")]
    [Produces(typeof(ReadOnlyPagedList<ReportDto>))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ReadOnlyPagedList<ReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPollutionReportsListByUserIdAsync(
        [FromRoute] Guid userId, 
        [FromQuery] int page,
        [FromQuery] int size)
    {
        var reportDtosList = await _pollutionModule.ExecuteQueryAsync(
            new GetPollutionReportsByUserIdQuery(userId, page, size));

        var metadata = reportDtosList.GetPaginationMetadataJson();

        Response.Headers.Add("X-Pagination", metadata);
        return Ok(reportDtosList);
    }

    [HttpGet("api/pollution-reports/status/{reportStatus}")]
    [Produces(typeof(ReadOnlyPagedList<ReportDto>))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ReadOnlyPagedList<ReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPollutionReportsListByStatusAsync(
        [FromRoute] string reportStatus,
        [FromQuery] int page,
        [FromQuery] int size)
    {
        var reportDtosList = await _pollutionModule.ExecuteQueryAsync(
            new GetPollutionReportsByStatusQuery(reportStatus, page, size));

        var metadata = reportDtosList.GetPaginationMetadataJson();
        
        Response.Headers.Add("X-Pagination", metadata);
        return Ok(reportDtosList);
    }
}