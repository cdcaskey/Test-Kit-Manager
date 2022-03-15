#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestKitManager.Data;

namespace TestKitManager.Pages.Observations
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationContext _context;

        public CreateModel(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Observation Observation { get; set; }

        [BindProperty]
        public List<int> MachineIds { get; set; }

        [BindProperty]
        public List<int> ServiceIds { get; set; }

        public SelectList Machines => new(_context.Machines.OrderBy(x => x.Name), "Id", "Name");

        public SelectList Services => new(_context.Services.OrderBy(x => x.Name), "Id", "Name");

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Observation.Created = Observation.LastUpdated = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            AttachRelatedMachinesAndServices();
            _context.Observations.Add(Observation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void AttachRelatedMachinesAndServices()
        {
            Observation.Machines = _context.Machines.Where(x => MachineIds.Contains(x.Id)).ToList();
            Observation.Services = _context.Services.Where(x => ServiceIds.Contains(x.Id)).ToList();
        }
    }
}
