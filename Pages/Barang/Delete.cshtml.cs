using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;
using pos_koperasi.Models;
using Microsoft.AspNetCore.Authorization;

namespace pos_koperasi.Pages_Barang
{
    [Authorize(Policy = "CanManageBarang")]
    public class DeleteModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;

        public DeleteModel(pos_koperasi.Data.RazorPagesBarangContext context)
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

            var barang = await _context.Barang.FirstOrDefaultAsync(m => m.Id == id);

            if (barang == null)
            {
                return NotFound();
            }
            else
            {
                Barang = barang;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barang = await _context.Barang.FindAsync(id);
            if (barang != null)
            {
                Barang = barang;
                _context.Barang.Remove(Barang);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
