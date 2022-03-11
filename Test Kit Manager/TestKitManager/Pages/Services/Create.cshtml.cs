#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestKitManager.Data;

namespace TestKitManager.Pages.Services
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
        public Service Service { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public SelectList Locations => new(_context.Machines.OrderBy(x => x.Name), "Id", "Name");

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload?.Length > Service.MaxConfigFileSize)
            {
                ModelState.AddModelError(nameof(Upload), "Config Files are limited to 150KB");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload != null)
            {
                Service.ConfigFileName = Upload.FileName;

                using var reader = new StreamReader(Upload.OpenReadStream());
                Service.ConfigFileContent = reader.ReadToEnd();
            }

            _context.Services.Add(Service);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
