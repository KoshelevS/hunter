using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Hunter.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hunter.Filters;

namespace Hunter.Controllers
{
    [Route("api/FileUpload")]
    public class FileUploadController : Controller
    {
        private readonly IRepository<Domain.Core.File> _fileRepository;

        public FileUploadController(IRepository<Domain.Core.File> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [Route("files")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(IList<IFormFile> file, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var f in file)
            {
                if (f.Length <= 0) continue;

                byte[] fileContent;
                using (var memStream = new MemoryStream())
                {
                    await f.CopyToAsync(memStream, cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                        return new EmptyResult();
                    fileContent = memStream.ToArray();
                }

                var item = new Domain.Core.File
                {
                    Name = f.FileName,
                    Extension = f.ContentType,
                    FileContent = fileContent
                };
                await _fileRepository.CreateAsync(item);
            }

            return new StatusCodeResult(200);
        }

        [Route("download/{id}")]
        [HttpGet]
        public FileContentResult Download(int id)
        {
            var file = _fileRepository.Get(id);
            
            return File(file.FileContent, file.Extension, file.Name);
        }
    }
}