using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;
using pos_koperasi.Models;

namespace pos_koperasi.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesBarangContext _context;

        public IndexModel(RazorPagesBarangContext context)
        {
            _context = context;
        }

        public IList<CartItem> CartItems { get; set; } = default!;
        public decimal Total { get; set; }

        public async Task OnGetAsync()
        {
            var cart = await _context.Carts.FirstOrDefaultAsync();
            if (cart != null)
            {
                CartItems = await _context.CartItems
                    .Include(ci => ci.Barang)
                    .Where(ci => ci.CartId == cart.Id)
                    .ToListAsync();

                Total = CartItems.Sum(ci => ci.Barang.Harga * ci.Quantity);
            }
        }

        public async Task<IActionResult> OnPostIncreaseQuantityAsync(int id)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Barang)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (cartItem == null)
                return NotFound();

            if (cartItem.Quantity >= cartItem.Barang.Stock)
            {
                TempData["Error"] = "Cannot add more items than available in stock";
                return RedirectToPage();
            }

            cartItem.Quantity++;
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDecreaseQuantityAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            if (cartItem.Quantity <= 1)
                return RedirectToPage();

            cartItem.Quantity--;
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
} 