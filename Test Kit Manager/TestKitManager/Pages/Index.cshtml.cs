using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using TestKitManager.Data;

namespace TestKitManager.Pages
{
    public class IndexModel : PageModel
    {
#if DEBUG
        private readonly DatabaseSeeder _seeder;

        public IndexModel(DatabaseSeeder seeder)
        {
            _seeder = seeder;
        }

        public void OnGet()
        {
            ViewData["Debug"] = true;
        }

        public void OnPostClearDatabase()
        {
            _seeder.ClearDatabase();
        }

        public void OnPostSeedDatabase()
        {
            _seeder.SeedDatabase();
        }
#else
        public void OnGet()
        {

        }
#endif
    }
}