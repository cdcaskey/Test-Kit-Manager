using System.ComponentModel.DataAnnotations;
using TestKitManager.Pages.Services;

namespace TestKitManager.Pages.Machines
{
    public class Machine
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public MachineType Type { get; set; }

        public string? IpAddresses { get; set; }

#nullable disable
        public virtual List<Service> Services { get; set; }
#nullable restore

        public enum MachineType
        {
            Physical = 0,
            Virtual = 1
        }
    }
}
