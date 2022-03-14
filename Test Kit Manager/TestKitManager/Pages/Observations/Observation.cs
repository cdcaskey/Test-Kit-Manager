using System.ComponentModel.DataAnnotations;
using TestKitManager.Pages.Machines;
using TestKitManager.Pages.Services;

namespace TestKitManager.Pages.Observations
{
    public class Observation
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public ObservationType Type { get; set; }

        public ObservationPriority Priority { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public bool ThirdPartyIssue { get; set; }

#nullable disable
        public virtual List<Machine> Machines { get; set; }

        public virtual List<Service> Services { get; set; }
#nullable restore
    }

    public enum ObservationType
    {
        Bug,
        Hardware,
        Environment,
        Test,
        Comment
    }

    public enum ObservationPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
}
