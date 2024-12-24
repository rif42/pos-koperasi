using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;

namespace pos_koperasi.Components
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly RazorPagesBarangContext _context;

        public CartCountViewComponent(RazorPagesBarangContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _context.Carts.FirstOrDefaultAsync();
            if (cart == null)
                return View(0);

            var itemCount = await _context.CartItems
                .Where(ci => ci.CartId == cart.Id)
                .SumAsync(ci => ci.Quantity);

            return View(itemCount);
        }
    }
} 