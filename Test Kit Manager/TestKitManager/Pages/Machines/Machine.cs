using System.ComponentModel.DataAnnotations;

namespace TestKitManager.Pages.Machines
{
    public class Machine
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public MachineType Type { get; set; }

        public string? IpAddresses { get; set; }

        public enum MachineType
        {
            Physical = 0,
            Virtual = 1
        }
    }
}
