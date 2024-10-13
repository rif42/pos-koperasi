using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;
using pos_koperasi.Models;

namespace pos_koperasi.Pages_Barang
{
    public class IndexModel : PageModel
    {
        private readonly pos_koperasi.Data.RazorPagesBarangContext _context;

        public IndexModel(pos_koperasi.Data.RazorPagesBarangContext context)
        {
            _context = context;
        }

        public IList<Barang> Barang { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Barang = await _context.Barang.ToListAsync();
        }
    }
}
