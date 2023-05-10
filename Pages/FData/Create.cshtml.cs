using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.Net;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace emaildemoapp.Pages_FData
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FData FData { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        // public async Task<IActionResult> OnPostAsync()
        // {
        //   if (!ModelState.IsValid || _context.FData == null || FData == null)
        //     {
        //         return Page();
        //     }

        //     _context.FData.Add(FData);
        //     await _context.SaveChangesAsync();

        //     return RedirectToPage("./Index");
        // }

        public async Task<IActionResult> OnPostAsync()
        {

            System.Diagnostics.Debug.WriteLine(
                $"Hi you're inside of OnPostAsync() and data is: ModelState.IsValid: {ModelState.IsValid} | _context.FData: {_context.FData} | FData: {FData}"
                );


            if (!ModelState.IsValid || _context.FData == null || FData == null)
            {
                                
                ViewData["errorMsg"] = "Invalid input. Please try again.";

                System.Diagnostics.Debug.WriteLine("Returning Page()");
                return Page();
            }

            if (FData.Password == null || !FData.Password.ToString().Equals("ApplePie123")) {
                System.Diagnostics.Debug.WriteLine("Returning Page(), password incorrect.");

                ViewData["errorMsg"] = "Wrong password. Please try again.";
                
                return Page();
            }


            System.Diagnostics.Debug.WriteLine("Writing Data to DB...");

            FData.SubmittedDate = DateTime.Now;

            _context.FData.Add(FData);
            await _context.SaveChangesAsync();

            Task<bool> result = SendEmailAsync(FData);


            System.Diagnostics.Debug.WriteLine("Redirecting to Success...");


            return RedirectToPage("./Success");
        }

        public async Task<bool> SendEmailAsync(FData fdata)
        {



            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "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");
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("jdmurphy521@outlook.com", "Jordan Murphy | Software Engineer | Upwork");
            var subject = "QR Email Demo App Response";
            var to = new EmailAddress(fdata.Email, fdata.Name);

            var plainTextContent = 
                $"Hello, {fdata.Name}! Here is the email you sent from the QR Email Demo App. \n\n\n" + 
                $"Destination Email: {fdata.Email}  \tName: {fdata.Name}\n\n" + 
                $"Address: \n\t{fdata.Address1}  \n\t{fdata.Address2}  \n\t{fdata.City}, {fdata.State} {fdata.Zip}\n\n" + 
                $"Comments: {fdata.TextArea}  \n\n\n" + 
                $"Options Checked: \n\tCheck Item A: {fdata.Check1} \n\tCheck Item B: {fdata.Check2} \n\tCheck Item C: {fdata.Check3}" + 
                $"Data Submitted: {fdata.SubmittedDate.ToLocalTime()}";


            var htmlContent = 
$@"
<div style='padding: 10px;'>

<div style='backgroud-color: #D3D3D3;'>

    <h2>Email:</h2>
    <p>{fdata.Email}</p>

    <h2>Name:</h2>
    <p>{fdata.Name}</p>

</div>


    <hr>

    <h4>Adress:</h4>
    <p>{fdata.Address1}</p>
    <p>{fdata.Address2}</p>
    <p>{fdata.City}, {fdata.State} {fdata.Zip}</p>

    <hr>

    <h4>Comments:</h4>
    <p>{fdata.TextArea}</p>

    <hr>

    <h4>Check Item A:</h4>
    <p>{fdata.Check1}</p>

    <h4>Check Item B:</h4>
    <p>{fdata.Check2}</p>

    <h4>Check Item C:</h4>
    <p>{fdata.Check3}</p>

    <hr>


    <h4>Submitted:</h4>
    <p>{fdata.SubmittedDate.ToLocalTime()}</p>

    <div>
        <p>&copy; 2023 - QR Email Demo App - Jordan Murphy | Software Engineer | Upwork</p>
    </div>
</div>


";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {

                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
