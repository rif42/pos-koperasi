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

            Barang = await _context.Barang?.ToListAsync() ?? new List<Barang>();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            // Get the barang first to check stock
            var barang = await _context.Barang.FindAsync(id);
            if (barang == null)
            {
                return NotFound();
            }

            // Check stock
            if (barang.Stock <= 0)
            {
                TempData["Error"] = "Item is out of stock";
                return RedirectToPage();
            }

            // Get or create cart
            var cart = await _context.Carts.FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if item already exists in cart
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.BarangId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    BarangId = id,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }
            else if (cartItem.Quantity >= barang.Stock)
            {
                TempData["Error"] = "Cannot add more items than available in stock";
                return RedirectToPage();
            }
            else
            {
                cartItem.Quantity++;
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Item added to cart successfully";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostBuyNowAsync(int id)
        {
            // Get the barang first to check stock
            var barang = await _context.Barang.FindAsync(id);
            if (barang == null)
            {
                return NotFound();
            }

            // Check stock
            if (barang.Stock <= 0)
            {
                TempData["Error"] = "Item is out of stock";
                return RedirectToPage();
            }

            // Get or create cart
            var cart = await _context.Carts.FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if item already exists in cart
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.BarangId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    BarangId = id,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }
            else if (cartItem.Quantity >= barang.Stock)
            {
                TempData["Error"] = "Cannot add more items than available in stock";
                return RedirectToPage();
            }
            else
            {
                cartItem.Quantity++;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Cart/Index");
        }
    }
}
