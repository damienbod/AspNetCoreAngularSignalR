using System;

namespace AspNetCoreAngularSignalR.Model
{
    public class ImageMessage
    {
        public byte[] ImageBinary { get; set; }
        public string ImageHeaders { get; set; }
    }
}