using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreMvcImageStreaming.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreMvcImageStreaming.Controllers
{
    [Route("api/imageupload")]
    public class FileUploadController : Controller
    {

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

                        //await file.SaveAsAsync(Path.Combine(_optionsApplicationConfiguration.Value.ServerUploadFolder, fileName));
                    }
                }
            }

            var files = new Model.FileResult
            {
                                FileNames = names,
                                ContentTypes = contentTypes,
                                Description = fileDescriptionShort.Description,
                                CreatedTimestamp = DateTime.UtcNow,
                                UpdatedTimestamp = DateTime.UtcNow,
                            };


            return RedirectToAction("Index", "FileClient");
        }
    }
}

