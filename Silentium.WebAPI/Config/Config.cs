namespace Silentium.WebAPI.Config {
    public class ApplicationConfig {
        public IWebHostEnvironment _webHostEnvironment;

        public ApplicationConfig(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetUploadDir() {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
            return uploadPath;
        }
    }
}
