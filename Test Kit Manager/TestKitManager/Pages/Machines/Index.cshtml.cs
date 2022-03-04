#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestKitManager.Data;

namespace TestKitManager.Pages.Machines
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public IList<Machine> Machine { get;set; }

        public async Task OnGetAsync()
        {
            Machine = await _context.Machines.ToListAsync();
        }
    }
}
