using Microsoft.Extensions.Logging;

namespace SemWorkAsp.AppServices.Logging;

public class FileLoggerProvider(string path) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path);
    }
 
    public void Dispose() {}
}
