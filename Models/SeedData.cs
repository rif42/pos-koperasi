#define Rating
#if Rating
#region snippet_1 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pos_koperasi.Data;
using System;
using System.Linq;

namespace pos_koperasi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesBarangContext(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesBarangContext>>()))
            {
                if (context == null || context.Barang == null)
                {
                    throw new ArgumentNullException("Null pos_koperasiContext");
                }

                // Look for any Barangs.
                if (context.Barang.Any())
                {
                    return;   // DB has been seeded
                }

                #region snippet1
                context.Barang.AddRange(
                    new Barang
                    {
                        NamaBarang = "Beras",
                        TerakhirUpdate = DateTime.Parse("1989-2-12"),
                        Harga = 53000,
                        Stock = 10,
                        Deskripsi = "beras pulen, harga per pcs"

                    },
                #endregion

                    new Barang
                    {
                        NamaBarang = "Gula",
                        TerakhirUpdate = DateTime.Parse("1989-2-12"),
                        Harga = 15000,
                        Stock = 10,
                        Deskripsi = "gula pasir rafinasi, harga per 1kg"
                    },
                    new Barang
                    {
                        NamaBarang = "Telor",
                        TerakhirUpdate = DateTime.Parse("1989-2-12"),
                        Harga = 12000,
                        Stock = 10,
                        Deskripsi = "telur ayam kampung, harga per 1kg"
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
#endregion
#endif