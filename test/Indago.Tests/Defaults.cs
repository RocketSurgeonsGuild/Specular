using Rocket.Surgery.Extensions.Testing;
using Serilog;

namespace Indago.Tests;

#pragma warning disable CS0436
public static class Defaults
{
    public static void LoggerTest<T>(T context, LoggerConfiguration configuration) where T : ILoggingTestContext => configuration.WriteTo.Console(outputTemplate: RocketSurgeonsTestingDefaults.OutputTemplate);
}
