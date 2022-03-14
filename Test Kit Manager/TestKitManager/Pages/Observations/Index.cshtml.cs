#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestKitManager.Data;

namespace TestKitManager.Pages.Observations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public IList<Observation> Observation { get;set; }

        public async Task OnGetAsync()
        {
            Observation = await _context.Observations.ToListAsync();
        }
    }
}
