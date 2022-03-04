#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestKitManager.Data;

namespace TestKitManager.Pages.Machines
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationContext _context;

        public DetailsModel(ApplicationContext context)
        {
            _context = context;
        }

        public Machine Machine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Machine = await _context.Machines.FirstOrDefaultAsync(m => m.Id == id);

            if (Machine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
