using System.ComponentModel.DataAnnotations;
using TestKitManager.Pages.Machines;

namespace TestKitManager.Pages.Services
{
    public class Service
    {
        public const int MaxConfigFileSize = 150000; // 150KB

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public int LocationId { get; set; }

        [Display(Name = "Config File")]
        public string? ConfigFileName { get; set; }

        public string? ConfigFileContent { get; set; }

#nullable disable
        public virtual Machine Location { get; set; }
#nullable restore

        public bool HasConfig => !string.IsNullOrEmpty(ConfigFileName) && !string.IsNullOrEmpty(ConfigFileContent);
    }
}
