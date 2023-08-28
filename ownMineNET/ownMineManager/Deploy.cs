using System.Net.NetworkInformation;

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
        var tcpConnInfo = ipGlobalProperties.GetActiveTcpConnections();
        return tcpListenersInfo.All(tcpInfo => tcpInfo.Port != port) && 
               tcpConnInfo.All(tcpInfo => tcpInfo.LocalEndPoint.Port != port);
    }

    /// <summary>
    /// Generates a port between 10000-65535, the returned port is available to use
    /// It tries to generate an available port for 10 times
    /// </summary>
    /// <returns>-1 if it's not possible to otherwise the port number</returns>
    public static int GeneratePort() {
        var rnd       = new Random();
        var attempts  = 1;
        while (attempts <= 10) {
            var randomPort = rnd.Next(10000, 65535);
            var portCheck  = PortAvailable(randomPort);
            if (portCheck)
                return randomPort;
            
            attempts++;
        }

        return -1;
    }
    
    
    
}