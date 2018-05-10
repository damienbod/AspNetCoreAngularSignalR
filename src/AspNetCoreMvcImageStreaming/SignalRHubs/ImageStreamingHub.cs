using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreMvcImageStreaming.SignalRHubs
{
    public class ImageStreamingHub : Hub
    {
        public Task SendFileNameUpload(string filename)
        {
            return Clients.All.SendAsync("Send", filename);
        }

        public ChannelReader<IFormFile> Image(IFormFile file)
        {
            var channel = Channel.CreateUnbounded<IFormFile>();

            _ = WriteToChannel(channel.Writer, file);

            return channel.Reader;

            async Task WriteToChannel(ChannelWriter<IFormFile> writer, IFormFile fileItem)
            {
                await writer.WriteAsync(fileItem);
                writer.Complete();
            }
        }
    }
}
