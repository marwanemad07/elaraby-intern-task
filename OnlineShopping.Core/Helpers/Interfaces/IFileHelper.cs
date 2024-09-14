namespace OnlineShopping.Core.Helpers.Interfaces
{
    public interface IFileHelper
    {
        Task<string> SaveFileAsync(IFormFile file, string relativeFilePath);
    }
}
