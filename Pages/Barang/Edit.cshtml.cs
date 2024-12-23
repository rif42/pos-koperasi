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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace pos_koperasi.Pages_Barang
{
    [Authorize(Policy = "CanManageBarang")]
    public class EditModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(pos_koperasi.Data.RazorPagesBarangContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Barang Barang { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

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

            var barang = await _context.Barang.FindAsync(Barang.Id);
            if (barang == null)
            {
                return NotFound();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(barang.ImagePath))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, barang.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save new image
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                barang.ImagePath = "/uploads/" + uniqueFileName;
            }

            barang.NamaBarang = Barang.NamaBarang;
            barang.TerakhirUpdate = Barang.TerakhirUpdate;
            barang.Harga = Barang.Harga;
            barang.Stock = Barang.Stock;
            barang.Deskripsi = Barang.Deskripsi;

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

            return RedirectToPage("/Barang/Index");
        }

        private bool BarangExists(int id)
        {
            return _context.Barang.Any(e => e.Id == id);
        }
    }
}
