using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

// namespace emaildemoapp.Pages;

namespace emaildemoapp.Pages_FData;


public class Create1Model : PageModel
{
    private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

    private readonly ILogger<Create1Model> _logger;

    public Create1Model(RazorPagesMovie.Data.RazorPagesMovieContext context, ILogger<Create1Model> logger)
    {
        _context = context;
        _logger = logger;
    }



      

    public void OnGet()
    {

    }

    [BindProperty]
    public RazorPagesMovie.Models.FData FData { get; set; } = default!;
    



    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {

        System.Diagnostics.Debug.WriteLine(
            $"Hi you're inside of OnPostAsync() and data is: ModelState.IsValid: {ModelState.IsValid} | _context.FData: {_context.FData} | FData: {FData}"
            );
        

        if (!ModelState.IsValid || _context.FData == null || FData == null)
        {
            System.Diagnostics.Debug.WriteLine("Returning Page()");
            return Page();
        }

        
        System.Diagnostics.Debug.WriteLine("Writing Data to DB...");


        _context.FData.Add(FData);
        await _context.SaveChangesAsync();

        System.Diagnostics.Debug.WriteLine("Redirecting to Success...");


        return RedirectToPage("./Success");
    }
}
