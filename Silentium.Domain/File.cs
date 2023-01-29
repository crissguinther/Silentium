using System.ComponentModel.DataAnnotations;

namespace Silentium.Domain {
    public class File {
        public int Id { get; set; }

        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Path is required")]
        public string Path { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Extension is required")]
        [MinLength(3)]
        [MaxLength(15)]
        public string FileExtension { get; set; }

        [Required(ErrorMessage = "\"Sent by\" is required")]
        public string? SentBy { get; set; } = "Anonymous";
    }
}