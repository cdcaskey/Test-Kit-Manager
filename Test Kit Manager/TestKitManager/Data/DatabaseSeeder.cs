using Microsoft.EntityFrameworkCore;
using TestKitManager.Pages.Machines;
using TestKitManager.Pages.Services;

namespace TestKitManager.Data
{
    public class DatabaseSeeder
    {
        private readonly ApplicationContext _context;

        public DatabaseSeeder(ApplicationContext context)
        {
            _context = context;
        }

        public void ClearDatabase()
        {
            _context.ChangeTracker.Clear();

            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
        }

        public void SeedDatabase()
        {
            ClearDatabase();

            _context.Machines.AddRange(new Machine
            {
                Id = 1,
                Name = "Physical Machine 1",
                Type = Machine.MachineType.Physical,
            },
            new Machine
            {
                Id = 2,
                Name = "Physical Machine 2",
                Type = Machine.MachineType.Physical,
            },
            new Machine
            {
                Id = 3,
                Name = "Virtual Machine 1",
                Type = Machine.MachineType.Virtual,
                IpAddresses = "192.168.1.100"
            },
            new Machine
            {
                Id = 4,
                Name = "Virtual Machine 2",
                Type = Machine.MachineType.Virtual,
                IpAddresses = "192.168.1.101"
            },
            new Machine
            {
                Id = 5,
                Name = "Virtual Machine 3",
                Type = Machine.MachineType.Virtual,
                IpAddresses = "192.168.1.103;10.0.0.3"
            },
            new Machine
            {
                Id = 6,
                Name = "Virtual Machine 4",
                Type = Machine.MachineType.Virtual,
                IpAddresses = "192.168.1.104;10.0.0.4"
            });

            _context.Services.AddRange(new Service
            {
                Id = 1,
                LocationId = 1,
                Name = "Monitoring Service",
                Version = "1.0.0"
            },
            new Service
            {
                Id = 2,
                LocationId = 2,
                Name = "Monitoring Service",
                Version = "1.0.0"
            },
            new Service
            {
                Id = 3,
                LocationId = 3,
                Name = "Monitoring Service",
                Version = "1.0.0"
            },
            new Service
            {
                Id = 4,
                LocationId = 4,
                Name = "Monitoring Service",
                Version = "1.0.0"
            },
            new Service
            {
                Id = 5,
                LocationId = 5,
                Name = "Monitoring Service",
                Version = "1.0.0"
            },
            new Service
            {
                Id = 6,
                LocationId = 6,
                Name = "Monitoring Service",
                Version = "1.0.0"
            },
            new Service
            {
                Id = 7,
                LocationId = 1,
                Name = "Remote Debugger",
                Version = "6.9",
                ConfigFileName = "Debugger.config",
                ConfigFileContent = "<configuration><DoDebugging>true</DoDebugging></configuration>"
            },
            new Service
            {
                Id = 8,
                LocationId = 3,
                Name = "Virtual Service",
                Version = "4.201.16",
                ConfigFileName = "Virtual.xml",
                ConfigFileContent = "<configuration><FancyVirtualStuff><DoStuff value=\"true\" /></FancyVirtualStuff></configuration>"
            },
            new Service
            {
                Id = 9,
                LocationId = 5,
                Name = "Virtual Service",
                Version = "5.150.25"
            });

            _context.SaveChanges();
        }
    }
}
