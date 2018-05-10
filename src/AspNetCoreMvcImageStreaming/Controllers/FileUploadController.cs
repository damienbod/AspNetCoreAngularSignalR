using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreMvcImageStreaming.Model;
using AspNetCoreMvcImageStreaming.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreMvcImageStreaming.Controllers
{
    [Route("api/imageupload")]
    public class FileUploadController : Controller
    {
        private readonly IHubContext<ImageStreamingHub> _hubContext;

        public FileUploadController(IHubContext<ImageStreamingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("files")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(FileDescriptionShort fileDescriptionShort)
        {
            var names = new List<string>();
            var contentTypes = new List<string>();
            if (ModelState.IsValid)
            {
                foreach (var file in fileDescriptionShort.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);

                        names.Add(fileName); 
                        await _hubContext.Clients.All.SendAsync("SendFileNameUpload", fileName);
                        await _hubContext.Clients.All.SendAsync("Image", file);
                    }
                }
            }

            return RedirectToAction("Index", "FileClient");
        }
    }
}

