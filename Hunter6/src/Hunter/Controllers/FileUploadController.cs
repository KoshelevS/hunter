using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Hunter.Filters;
using Microsoft.Net.Http.Headers;

namespace Hunter.Controllers
{
    [Route("api/FileUpload")]
    public class FileUploadController : Controller
    {
        //        private readonly IFileRepository _fileRepository;

        //        public FileUploadController(IFileRepository fileRepository)
        //        {
        //            _fileRepository = fileRepository;
        //        }

        [Route("files")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(IList<IFormFile> file)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            foreach (var f in file)
            {
                if (f.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(f.ContentDisposition).FileName.Trim('"');
                    await f.SaveAsAsync(fileName);
                }
            }
            //_fileRepository.AddFileDescriptions(f);
            
            return new HttpStatusCodeResult(200);
        }

        [Route("download/{id}")]
        [HttpGet]
        public FileStreamResult Download(int id)
        {
            //var fileDescription = _fileRepository.GetFileDescription(id);
            
            var stream = new FileStream("", FileMode.Open);
            return File(stream, ".test");
        }
    }
}