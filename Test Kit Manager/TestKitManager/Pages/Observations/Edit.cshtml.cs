#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestKitManager.Data;

namespace TestKitManager.Pages.Observations
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;

        public EditModel(ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Observation Observation { get; set; }

        [BindProperty]
        public List<int> MachineIds { get; set; }

        [BindProperty]
        public List<int> ServiceIds { get; set; }

        public SelectList Machines => new(_context.Machines.OrderBy(x => x.Name), "Id", "Name");

        public SelectList Services => new(_context.Services.OrderBy(x => x.Name), "Id", "Name");

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Observation = await _context.Observations
                                        .Include(o => o.Machines)
                                        .Include(o => o.Services)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (Observation == null)
            {
                return NotFound();
            }

            PopulateRelatedMachinesAndServices();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Observation.LastUpdated = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            AttachRelatedMachinesAndServices();
            _context.Attach(Observation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObservationExists(Observation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ObservationExists(int id)
        {
            return _context.Observations.Any(e => e.Id == id);
        }
        private void AttachRelatedMachinesAndServices()
        {
            Observation.Machines = _context.Machines.Where(x => MachineIds.Contains(x.Id)).ToList();
            Observation.Services = _context.Services.Where(x => ServiceIds.Contains(x.Id)).ToList();
        }

        private void PopulateRelatedMachinesAndServices()
        {
            MachineIds = Observation.Machines?.Select(x => x.Id).ToList();
            ServiceIds = Observation.Services?.Select(x => x.Id).ToList();
        }
    }
}
