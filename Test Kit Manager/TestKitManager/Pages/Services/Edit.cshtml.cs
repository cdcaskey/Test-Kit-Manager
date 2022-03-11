#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestKitManager.Data;

namespace TestKitManager.Pages.Services
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;

        public EditModel(ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public bool DeleteConfig { get; set; }

        public SelectList Locations => new(_context.Machines.OrderBy(x => x.Name), "Id", "Name");

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service = await _context.Services
                .Include(s => s.Location).FirstOrDefaultAsync(m => m.Id == id);

            if (Service == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

            var existingService = _context.Services.FirstOrDefault(x => x.Id == Service.Id);
            
            if (existingService == null)
            {
                return NotFound();
            }

            // If no config is uploaded and the user hasn't elected to delete the existing config, copy the existing one
            if (Upload == null && !DeleteConfig)
            {
                Service.ConfigFileName = existingService.ConfigFileName;
                Service.ConfigFileContent = existingService.ConfigFileContent;
            }

            if (Upload != null)
            {
                Service.ConfigFileName = Upload.FileName;

                using var reader = new StreamReader(Upload.OpenReadStream());
                Service.ConfigFileContent = reader.ReadToEnd();
            }

            _context.Entry(existingService).CurrentValues.SetValues(Service);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(Service.Id))
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

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
