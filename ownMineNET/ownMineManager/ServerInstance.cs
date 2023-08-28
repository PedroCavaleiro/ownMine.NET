using System.Diagnostics;
using ownMineDatabase;
// ReSharper disable CollectionNeverQueried.Local

namespace ownMineManager;

public class ServerInstance {
    
    private readonly Process                      _serverInstance;
    private readonly MinecraftServerInstance      _minecraftServerInstance;
    private readonly Dictionary<DateTime, string> _log;
    private readonly Dictionary<string, string>   _serverSettings;

    public ServerInstance(MinecraftServerInstance minecraftServerInstance, string javaLocation, string instancesLocation) {
        _serverSettings = new Dictionary<string, string>();
        var file = File.ReadLines(
            $"{instancesLocation}/{minecraftServerInstance.MinecraftServerInstanceId}/server.properties");
        foreach (var line in file) {
            if (line.First() == '#')
                continue;
            var dic = line.Split("=");
            _serverSettings.Add(dic[0], dic[1]);
        }
        
        _minecraftServerInstance                         = minecraftServerInstance;
        _log                                             =  new Dictionary<DateTime, string>();
        _serverInstance                                  =  new Process();
        _serverInstance.StartInfo.FileName               =  javaLocation;
        _serverInstance.StartInfo.Arguments              =  $"-{string.Join(" -", minecraftServerInstance.StartFlags)} -Xmx{minecraftServerInstance.MemoryAllocation}G -Xms{minecraftServerInstance.MemoryAllocation}G -jar {instancesLocation}/{minecraftServerInstance.MinecraftServerInstanceId}/server.jar nogui";
        _serverInstance.StartInfo.UseShellExecute        =  false;
        _serverInstance.StartInfo.RedirectStandardError  =  true;
        _serverInstance.StartInfo.RedirectStandardOutput =  true;
        _serverInstance.EnableRaisingEvents              =  true;
        _serverInstance.OutputDataReceived               += OutputHandler;
        _serverInstance.ErrorDataReceived                += ErrorOutputHandler;
        _serverInstance.Exited                           += OnExit;
        _serverInstance.Start();
        _serverInstance.BeginOutputReadLine();
        _serverInstance.BeginErrorReadLine();
        Task.Run(async () => await _serverInstance.WaitForExitAsync());
    }

    /// <summary>
    /// Handles the stderr of the process
    /// </summary>
    /// <param name="sender">Process sending the error</param>
    /// <param name="args">Message</param>
    private void ErrorOutputHandler(object sender, DataReceivedEventArgs args) {
        _log.Add(DateTime.UtcNow, args.Data);
    }
    
    /// <summary>
    /// Handles the stdout of the process
    /// </summary>
    /// <param name="sender">Process sending the message</param>
    /// <param name="args">Message</param>
    private void OutputHandler(object sender, DataReceivedEventArgs args) {
        _log.Add(DateTime.UtcNow, args.Data);
    }

    private void OnExit(object sender, EventArgs args) {
        
    }

}