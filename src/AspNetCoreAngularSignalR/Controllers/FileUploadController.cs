using AspNetCoreAngularSignalR.Model;
using AspNetCoreAngularSignalR.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreAngularSignalR.Controllers;

[Route("api/[controller]")]
public class FileUploadController : Controller
{
    private readonly IHubContext<ImagesMessageHub> _hubContext;

    public FileUploadController(IHubContext<ImagesMessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [Route("files")]
    [HttpPost]
    [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
    public async Task<IActionResult> UploadFiles(FileDescriptionShort fileDescriptionShort)
    {
        if (ModelState.IsValid)
        {
            foreach (var file in fileDescriptionShort.File!)
            {
                if (file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);

                    var imageMessage = new ImageMessage
                    {
                        ImageHeaders = "data:" + file.ContentType + ";base64,",
                        ImageBinary = memoryStream.ToArray()
                    };

                    await _hubContext.Clients.All.SendAsync("ImageMessage", imageMessage);
                }
            }
        }

        return Redirect("/FileClient/Index");
    }
}