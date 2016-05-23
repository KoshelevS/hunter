using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace Hunter.ViewModels.File
{
    public class SummaryViewModel
    {
        public IList<IFormFile> Files { get; set; }
    }
}
