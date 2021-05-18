using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using System.Windows;

namespace SentryMissingBreadcrumbPoc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Setup Seriog & Sentry
            Log.Logger = BuildLogger();
        }

        private static Logger BuildLogger() => new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.File(new CompactJsonFormatter(), "./logs/log.txt", shared: true, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            .WriteTo.Sentry(o =>
            {
                o.Dsn = "https://842e9c37502d4f6b8cc1a6649dcf35e3@o210546.ingest.sentry.io/1334742";

                //o.DisableAppDomainUnhandledExceptionCapture();
                //o.DisableTaskUnobservedTaskExceptionCapture();

                o.MinimumBreadcrumbLevel = Serilog.Events.LogEventLevel.Debug;
                o.MinimumEventLevel = Serilog.Events.LogEventLevel.Error;
            })
            .CreateLogger();
    }
}
