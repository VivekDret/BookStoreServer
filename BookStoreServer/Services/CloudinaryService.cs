using BookStoreServer.Interface;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace BookStoreServer.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file, int? height = null, int? quality = null)
        {
            var folder = "CozyHeaven";

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = false,
            };

            var imageUploadResponse = await _cloudinary.UploadAsync(uploadParams);
            return imageUploadResponse;

        }
    }
}
