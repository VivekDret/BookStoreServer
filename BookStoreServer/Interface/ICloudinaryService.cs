using CloudinaryDotNet.Actions;

namespace BookStoreServer.Interface
{
    public interface ICloudinaryService
    {
        public Task<ImageUploadResult> UploadImageAsync(IFormFile file, int? height = null, int? quality = null);
    }
}
