using System.ComponentModel.DataAnnotations;
using TestKitManager.Pages.Machines;

namespace TestKitManager.Pages.Services
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public int LocationId { get; set; }

#nullable disable
        public virtual Machine Location { get; set; }
#nullable restore
    }
}
