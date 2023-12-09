﻿using Serilog;
using Serilog.Core;

namespace VirusSpreadLibrary.SpreadModel
{
    public static  class Logging
    {
        public static Logger getinstance()
        {
            string customTemplate = "{Timestamp:dd/MM/yy HH:mm:ss.fff}\t[{Level:u3}]\t{Message}{NewLine}{Exception}";
            return new LoggerConfiguration()
                             .MinimumLevel.Verbose()
                             .Enrich.FromLogContext()
                             .WriteTo.Console(outputTemplate: customTemplate)
                             .WriteTo.Console()                             
                             .CreateLogger();
        }
    }
}
