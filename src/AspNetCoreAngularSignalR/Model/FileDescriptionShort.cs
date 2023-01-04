namespace AspNetCoreAngularSignalR.Model;

public class FileDescriptionShort
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<IFormFile>? File { get; set; }
}