using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using pos_koperasi.Data;
using pos_koperasi.Models;

namespace pos_koperasi.Pages_Barang
{
    public class CreateModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;

        public CreateModel(pos_koperasi.Data.RazorPagesBarangContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Barang Barang { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Barang.Add(Barang);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
