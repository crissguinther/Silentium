using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using Silentium.WebAPI.Config;
using Silentium.Services.Helpers;

namespace Silentium.Services.Uploads {
    public class UploadService {
        private readonly ApplicationConfig _config;
        
        public UploadService(ApplicationConfig config) {
            _config = config;
        }

        /// <summary>
        /// Check if two files are equal
        /// </summary>
        /// <param name="firstFile">The <see cref="FileInfo"/> of the first file</param>
        /// <param name="secondFile">The <see cref="FileInfo"/> of the second file</param>
        /// <returns></returns>
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

        /// <summary>
        /// Stores a IFormFile into the server
        /// </summary>
        /// <param name="file">A <see cref="IFormFile"/>The file to be stored</param>
        /// <returns>The string for the path of the stored file</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> StoreReceivedFile(IFormFile file) {
            string uploadLocation = Path.Combine(_config.GetUploadDir(), RandomFileNameGenerator.GetRandomNameGenerator(file.FileName));

            if (file.Length == 0) throw new Exception("File is empty");
            if (!Directory.Exists(_config.GetUploadDir())) Directory.CreateDirectory(_config.GetUploadDir());
            var uploadedFile = File.Create(uploadLocation);
            await file.CopyToAsync(uploadedFile);
            uploadedFile.Close();

            return uploadLocation;
        }

        /// <summary>
        /// Sets a password for an uploaded file
        /// </summary>
        /// <param name="filePath">The string path for the file that will be protected</param>
        /// <param name="password">The password that will be used</param>
        /// <returns>The path for the protected file</returns>
        public string SetPasswordForReceivedFile(string filePath, string password) {
            FileInfo fileInfo = new FileInfo(filePath);
            string zipName = RandomFileNameGenerator.GetRandomNameGenerator(fileInfo.FullName);
            string destiny = Path.Combine(_config.GetUploadDir(), zipName);
            byte[] buffer = new byte[4096];

            using (var zip = new ZipOutputStream(File.Create(destiny))) {
                ZipEntry entry = new ZipEntry(destiny);
                zip.PutNextEntry(entry);

                using (var fs = File.OpenRead(filePath)) {
                    var bytes = fs.Read(buffer, 0, buffer.Length);
                    zip.Write(buffer, 0, bytes);
                }
            }

            File.Delete(filePath);

            return destiny;
        }
    }
}
