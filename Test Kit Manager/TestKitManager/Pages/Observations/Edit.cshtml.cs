#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Observation = await _context.Observations.FirstOrDefaultAsync(m => m.Id == id);

            if (Observation == null)
            {
                return NotFound();
            }
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
    }
}
