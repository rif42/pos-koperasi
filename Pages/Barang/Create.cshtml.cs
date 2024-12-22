using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using pos_koperasi.Data;
using pos_koperasi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace pos_koperasi.Pages_Barang
{
    [Authorize(Policy = "CanManageBarang")]
    public class CreateModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(pos_koperasi.Data.RazorPagesBarangContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Barang Barang { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                Barang.ImagePath = "/uploads/" + uniqueFileName;
            }

            _context.Barang.Add(Barang);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
