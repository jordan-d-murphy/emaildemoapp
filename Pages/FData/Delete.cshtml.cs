using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace emaildemoapp.Pages_FData
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public DeleteModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FData FData { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FData == null)
            {
                return NotFound();
            }

            var fdata = await _context.FData.FirstOrDefaultAsync(m => m.Id == id);

            if (fdata == null)
            {
                return NotFound();
            }
            else 
            {
                FData = fdata;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FData == null)
            {
                return NotFound();
            }
            var fdata = await _context.FData.FindAsync(id);

            if (fdata != null)
            {
                FData = fdata;
                _context.FData.Remove(FData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
