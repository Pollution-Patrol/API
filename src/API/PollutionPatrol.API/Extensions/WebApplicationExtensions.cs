namespace PollutionPatrol.API.Extensions;

public static class WebApplicationExtensions
{
    public static void UseGlobalExceptionHandler(this WebApplication app) =>
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}