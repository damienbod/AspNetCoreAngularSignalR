using System.Threading.Channels;
using System.Threading.Tasks;
using AspNetCoreAngularSignalR.Model;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreAngularSignalR.SignalRHubs
{
    public class ImagesMessageHub : Hub
    {
        public Task SendFileNameUpload(string filename)
        {
            return Clients.All.SendAsync("SendFileNameUpload", filename);
        }

        public Task ImageMessage(ImageMessage file)
        {
            return Clients.All.SendAsync("ImageMessage", file);
        }

        //public ChannelReader<ImageMessage> ImageMessage(ImageMessage file)
        //{
        //    var channel = Channel.CreateUnbounded<ImageMessage>();

        //    _ = WriteToChannel(channel.Writer, file);

        //    return channel.Reader;

        //    async Task WriteToChannel(ChannelWriter<ImageMessage> writer, ImageMessage fileItem)
        //    {
        //        await writer.WriteAsync(fileItem);
        //        writer.Complete();
        //    }
        //}
    }
}
