using FakeItEasy;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Silentium.Services.Uploads;
using Silentium.WebAPI.Config;
using System.Text;

namespace SilentiumTests.Services {
    public class UploadServiceTests {
        private readonly UploadService _uploadService;
        private readonly ApplicationConfig _config;

        public UploadServiceTests() {
            var FakeConfig = A.Fake<IWebHostEnvironment>();
            _config = new ApplicationConfig(FakeConfig);
            _uploadService = new UploadService(_config);
        }        

        [Fact]
        public async Task UploadService_Should_CreateEqualFile() {
            // Arrange
            byte[] filebytes = Encoding.UTF8.GetBytes("A test image");
            IFormFile file = new FormFile(new MemoryStream(filebytes), 3, filebytes.Length, "Test", "image.png");

            var tempFile = File.Create(Path.Combine(Path.GetTempPath(), file.Name));
            await file.CopyToAsync(tempFile);
            tempFile.Close();

            // Act
            var result = await _uploadService.StoreReceivedFile(file);
            bool isCreated = File.Exists(result);
            bool isEqual = UploadService.IsEqual(new FileInfo(tempFile.Name), new FileInfo(result));

            // Assert
            Assert.NotNull(result);
            Assert.True(isCreated);
            Assert.True(isEqual);
        }

        [Fact]
        public async Task UploadService_Should_PasswordProtectUploadedFileAndDeleteTheOriginal() {
            // Arrange
            byte[] filebytes = Encoding.UTF8.GetBytes("A test file");
            IFormFile file = new FormFile(new MemoryStream(filebytes), 3, filebytes.Length, "EncryptionTest", "file.txt");
            string generatedFilePath = await _uploadService.StoreReceivedFile(file);
            string password = "aPassWord";

            var tempFile = File.Create(Path.Combine(Path.GetTempPath(), file.Name));
            await file.CopyToAsync(tempFile);
            tempFile.Close();

            // Act            
            string encryptedFilePath = _uploadService.SetPasswordForReceivedFile(generatedFilePath, password);
            bool isCreated = File.Exists(encryptedFilePath);
            bool isEqual = UploadService.IsEqual(new FileInfo(tempFile.Name), new FileInfo(encryptedFilePath));
            bool isOriginalDeleted = File.Exists(tempFile.Name);

            // Assert
            Assert.NotNull(encryptedFilePath);
            Assert.True(isCreated);
            Assert.False(isEqual);
            Assert.True(isOriginalDeleted);
        }
    }
}
