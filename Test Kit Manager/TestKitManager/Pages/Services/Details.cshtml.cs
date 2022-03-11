#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TestKitManager.Data;

namespace TestKitManager.Pages.Services
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationContext _context;

        public DetailsModel(ApplicationContext context)
        {
            _context = context;
        }

        public Service Service { get; set; }

        public ActionResult OnPostDownloadFile(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);

            if (service == null || !service.HasConfig)
            {
                return NotFound();
            }

            return File(Encoding.ASCII.GetBytes(service.ConfigFileContent!), "application/octet-stream", service.ConfigFileName);
        }

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
    }
}
