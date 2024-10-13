using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;
using pos_koperasi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pos_koperasi.Pages_Barang
{
    public class IndexModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;

        public IndexModel(pos_koperasi.Data.RazorPagesBarangContext context)
        {
            _context = context;
        }

        public IList<Barang> Barang { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? NamaBarang { get; set; }

        // [BindProperty(SupportsGet = true)]
        // public SelectList? BarangNama { get; set; }

        public async Task OnGetAsync()
        {
            var barang = from m in _context.Barang
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                barang = barang.Where(s => s.NamaBarang.Contains(SearchString));
            }

            Barang = await barang.ToListAsync();
        }
    }
}
