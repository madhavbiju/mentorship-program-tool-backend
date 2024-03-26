using Serilog;
using mentorship_program_tool.Log;
public class SeriLogHandler : ILogHandler
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IConfiguration _configuration;
    public SeriLogHandler(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _configuration = configuration;
    }
    public void Initialize()
    {
        //configure serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(_configuration) // Read Serilog configuration from appsettings.json
            .CreateLogger();
        _loggerFactory.AddSerilog();
    }
}

