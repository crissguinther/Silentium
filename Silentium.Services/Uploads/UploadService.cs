using Microsoft.AspNetCore.Http;
using Silentium.WebAPI.Config;

namespace Silentium.Services.Uploads {
    public class UploadService {
        private readonly ApplicationConfig _config;
        
        public UploadService(ApplicationConfig config) {
            _config = config;
        }

        public static bool IsEqual(FileInfo firstFile, FileInfo secondFile) {
            if (firstFile.Length != secondFile.Length) return false;

            using (FileStream first = firstFile.OpenRead())
            using (FileStream second = secondFile.OpenRead()) {
                for (int i = 0; i < firstFile.Length; i++) {
                    if (first.ReadByte() != second.ReadByte()) return false;
                }
            }
            
            return true;
        }

        public async Task<string> StoreReceivedFile(IFormFile file) {
            string name = file.Name;
            string ext = file.FileName.Substring(file.FileName.LastIndexOf(".")).Replace(".", "");

            string newFile = $"{name}_{DateTime.Now.Ticks}.{ext}";
            string uploadLocation = Path.Combine(_config.GetUploadDir(), newFile);

            if (file.Length == 0) throw new Exception("File is empty");
            if (!Directory.Exists(_config.GetUploadDir())) Directory.CreateDirectory(_config.GetUploadDir());
            var uploadedFile = File.Create(uploadLocation);
            await file.CopyToAsync(uploadedFile);
            uploadedFile.Close();

            return uploadLocation;
        }        

        public void EncryptReceivedFile() {
            throw new NotImplementedException();
        }
    }
}
