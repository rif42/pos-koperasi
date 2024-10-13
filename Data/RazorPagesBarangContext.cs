using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Models;

namespace pos_koperasi.Data
{
    public class RazorPagesBarangContext : DbContext
    {
        public RazorPagesBarangContext (DbContextOptions<RazorPagesBarangContext> options)
            : base(options)
        {
        }

        public DbSet<pos_koperasi.Models.Barang> Barang { get; set; } = default!;
    }
}
