using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Extensions;
using ownMineManager.Models;

namespace ownMineManager; 

public class Deploy {

    /// <summary>
    /// Checks if a TCP port is being used on the local machine either by a client or server
    /// </summary>
    /// <param name="port">TCP port to check</param>
    /// <returns>True if the port is available false otherwise</returns>
    public static bool PortAvailable(int port) {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpListenersInfo   = ipGlobalProperties.GetActiveTcpListeners();
        var tcpConnInfo        = ipGlobalProperties.GetActiveTcpConnections();
        return tcpListenersInfo.All(tcpInfo => tcpInfo.Port != port) && 
               tcpConnInfo.All(tcpInfo => tcpInfo.LocalEndPoint.Port != port);
    }

    /// <summary>
    /// Generates a port between 10000-65535, the returned port is available to use
    /// It tries to generate an available port for 10 times
    /// </summary>
    /// <returns>-1 if it's not possible to otherwise the port number</returns>
    public static int GeneratePort() {
        var rnd      = new Random();
        var attempts = 1;
        while (attempts <= 10) {
            var randomPort = rnd.Next(10000, 65535);
            var portCheck  = PortAvailable(randomPort);
            if (portCheck)
                return randomPort;
            
            attempts++;
        }

        return -1;
    }

    public static async void GetMinecraftVersions() {
        var vanillaVersions = await GetMinecraftVanillaVersions();
        vanillaVersions.Versions = vanillaVersions.Versions.Where(v => v.ComplianceLevel >= 1).ToList();

        var fabricVersions = await GetMinecraftFabricVersions();
    }

    private static async Task<VanillaVersionManifest> GetMinecraftVanillaVersions() {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://piston-meta.mojang.com/mc/game/");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await httpClient.GetAsync("version_manifest_v2.json");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadJsonContentAsAsync<VanillaVersionManifest>();
        return null;
    }
    
    private static async Task<VanillaVersionManifest> GetMinecraftFabricVersions() {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://meta.fabricmc.net/v2/versions/");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var installerResponse = await httpClient.GetAsync("installer");
        var gameResponse      = await httpClient.GetAsync("game");
        var loaderResponse    = await httpClient.GetAsync("loader");

        if (!installerResponse.IsSuccessStatusCode)
            return null;
        if (!gameResponse.IsSuccessStatusCode)
            return null;
        if (!loaderResponse.IsSuccessStatusCode)
            return null;

        var installerManifest =
            await installerResponse.Content.ReadJsonContentAsAsync<FabricVersionManifest.Installer>();
        var gameManifest   = await gameResponse.Content.ReadJsonContentAsAsync<FabricVersionManifest.Game>();
        var loaderManifest = await gameResponse.Content.ReadJsonContentAsAsync<FabricVersionManifest.Loader>();

        var versions = new {
            Installers = installerManifest,
            Games = gameManifest,
            Loaders = loaderManifest
        };
        
        return null;
    }
    
}