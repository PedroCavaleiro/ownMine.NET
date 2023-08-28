// ReSharper disable ClassNeverInstantiated.Global

using System.Text.Json.Serialization;

namespace ownMineNET.Models; 

public class OwnMineNetSettings {
    
    public string                       AllowedHosts         { get; set; }
    public DatabaseConnectionStrings    ConnectionStrings    { get; set; }
    public OwnMineConfigurationSettings OwnMineConfiguration { get; set; }
    public LoggingSettings              Logging              { get; set; }
    
    public class DatabaseConnectionStrings {
        public string Development { get; set; }
        public string Staging     { get; set; }
        public string Production  { get; set; }
    }

    public class OwnMineConfigurationSettings {
        public string InstancesLocation { get; set; }
        public string LocalBackups      { get; set; }
        public string RunAs             { get; set; }
        public string JavaLocation      { get; set; }
    }

    public class LoggingSettings {
        public LogLevelSettings LogLevel { get; set; }

        public class LogLevelSettings {
            public string           Default    { get; set; }
            [JsonPropertyName("Microsoft.AspNetCore")]
            public string MicrosoftAspNetCore { get; set; }
        }
    }
    
}