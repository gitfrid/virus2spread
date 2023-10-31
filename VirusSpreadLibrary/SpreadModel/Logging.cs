using Serilog;
using Serilog.Core;

namespace VirusSpreadLibrary.SpreadModel
{
    public static  class Logging
    {
        public static Logger getinstance()
        {
            return new LoggerConfiguration()
                             .MinimumLevel.Verbose()
                             .Enrich.FromLogContext()
                             .WriteTo.Console()
                             .CreateLogger();
        }
    }
}
