using System.Configuration;

namespace VirusSpreadLibrary.Properties
{
    
    public class AppConfig
    {
        public AppConfig(Settings Config)
        {            
        }
        public Settings Config { get; set; }
    };   
  
}
