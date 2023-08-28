using System.ComponentModel.DataAnnotations;
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace ownMineDatabase; 

public class MinecraftServerInstance {
    
    [Key]
    public Guid MinecraftServerInstanceId { get; set; }
    
    public string       Name             { get; set; }
    public List<string> StartFlags       { get; set; }
    public int          MemoryAllocation { get; set; }
    
    public string BackupServer     { get; set; }
    public string BackupShare      { get; set; }
    public string BackupSubDir     { get; set; }
    public string BackupMainFolder { get; set; }
    public string BackupOldFolder  { get; set; }
    
    public string BackupUser     { get; set; }
    public string BackupDomain   { get; set; }
    public string BackupPassword { get; set; }
    public string SmbFileMode    { get; set; }
    public string SmbDirMode     { get; set; }
    
}