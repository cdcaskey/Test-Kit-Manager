#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestKitManager.Data;

namespace TestKitManager.Pages.Observations
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationContext _context;

        public DetailsModel(ApplicationContext context)
        {
            _context = context;
        }

        public Observation Observation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Observation = await _context.Observations
                                        .Include(o => o.Machines)
                                        .Include(o => o.Services)
                                        .ThenInclude(s => s.Location)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (Observation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
