using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AspNetCoreAngularSignalR.Model;
using AspNetCoreAngularSignalR.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreAngularSignalR.Controllers
{
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
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            var imageMessage = new ImageMessage
                            {
                                ImageHeaders = "data:" + file.ContentType + ";base64,",
                                ImageBinary = Convert.ToBase64String(memoryStream.ToArray())
                            };

                            //return File(memoryStream.ToArray(), file.ContentType);
                            //await _hubContext.Clients.All.SendAsync("ImageMessage", file.OpenReadStream(), file.ContentType);
                            //await _hubContext.Clients.All.SendAsync("ImageMessage", File(memoryStream.ToArray(), file.ContentType));
                            await _hubContext.Clients.All.SendAsync("ImageMessage", imageMessage);
                        }
                    }
                }
            }

            return View("Views/FileClient/Index.cshtml");
        }
    }
}

