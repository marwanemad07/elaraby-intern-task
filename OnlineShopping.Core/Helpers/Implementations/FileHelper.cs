
using Microsoft.AspNetCore.Hosting;

namespace OnlineShopping.Core.Helpers.Implementations
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> SaveFileAsync(IFormFile file, string relativeFilePath)
        {
            try
            {
                var rootPath = GetRootPath();

                using var stream = new FileStream(Path.Combine(rootPath, relativeFilePath), FileMode.Create);
                await file.CopyToAsync(stream);

                var filePath = Path.Combine(GetRootPath(), relativeFilePath);
                return filePath;
            }
            catch (Exception ex)
            {
                // log error
                throw;
            }
        }

        private string GetRootPath()
        {
            var path = _webHostEnvironment.WebRootPath;
            return path;
        }
    }
}
