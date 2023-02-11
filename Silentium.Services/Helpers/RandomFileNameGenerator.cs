using System.Text;

namespace Silentium.Services.Helpers {
    public static class RandomFileNameGenerator {
        public static string GetRandomNameGenerator(string fileName) {
            var currentTicks = new DateTime().Ticks;
            string fileExtension = fileName.Substring(fileName.LastIndexOf(".")).Replace(".", "");

            var sb = new StringBuilder();
            sb.Append(fileName);
            sb.Append('_');
            sb.Append(currentTicks.ToString());
            sb.Append('.');
            sb.Append(fileExtension);

            return sb.ToString();
        }
    }
}
