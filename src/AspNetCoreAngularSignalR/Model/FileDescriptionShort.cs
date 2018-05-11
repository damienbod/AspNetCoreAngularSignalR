using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreAngularSignalR.Model
{
    public class FileDescriptionShort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<IFormFile> File { get; set; }
    }
}