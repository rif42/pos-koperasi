using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;
using pos_koperasi.Models;

namespace pos_koperasi.Pages_Barang
{
    public class EditModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;

        public EditModel(pos_koperasi.Data.RazorPagesBarangContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Barang Barang { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barang =  await _context.Barang.FirstOrDefaultAsync(m => m.Id == id);
            if (barang == null)
            {
                return NotFound();
            }
            Barang = barang;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Barang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarangExists(Barang.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BarangExists(int id)
        {
            return _context.Barang.Any(e => e.Id == id);
        }
    }
}
