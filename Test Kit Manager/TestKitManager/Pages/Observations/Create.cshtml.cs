#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Observation.Created = Observation.LastUpdated = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Observations.Add(Observation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
