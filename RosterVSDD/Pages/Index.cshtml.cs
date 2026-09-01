using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RosterVSDD.Models;
using RosterVSDD.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RosterVSDD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRosterService _service;

        public IndexModel(IRosterService service)
        {
            _service = service;
        }

        [BindProperty]
        public RosterEntry Input { get; set; } = new RosterEntry();

        public IReadOnlyList<RosterEntry> Roster { get; set; } = new List<RosterEntry>();

        public void OnGet()
        {
            Roster = _service.GetAll();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Roster = _service.GetAll();
                return Page();
            }

            await _service.AddAsync(Input);
            return RedirectToPage();
        }
    }
}
