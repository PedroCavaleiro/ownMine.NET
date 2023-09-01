using System.Text.Json.Serialization;

namespace ownMineManager.Models; 

public class FabricVersionManifest {

    public class Game {
        [JsonPropertyName("version")]
        public string Version { get; set; }
        
        [JsonPropertyName("stable")]
        public bool Stable { get; set; }
    }
    
    public class Loader {
        [JsonPropertyName("separator")]
        public string Separator { get; set; }
        
        [JsonPropertyName("build")]
        public int Build { get; set; }
        
        [JsonPropertyName("maven")]
        public string Maven { get; set; }
        
        [JsonPropertyName("version")]
        public string Version { get; set; }
        
        [JsonPropertyName("stable")]
        public bool Stable { get; set; }
    }
    
    public class Installer {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("maven")]
        public string Maven { get; set; }
        
        [JsonPropertyName("version")]
        public string Version { get; set; }
        
        [JsonPropertyName("stable")]
        public bool Stable { get; set; }
    }
}