using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pos_koperasi.Data;
using pos_koperasi.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Globalization;


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

        public async Task<IActionResult> OnPostPurchaseAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync();
            if (cart == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .Include(ci => ci.Barang)
                .Where(ci => ci.CartId == cart.Id)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToPage();
            }

            // Check stock availability
            foreach (var item in cartItems)
            {
                if (item.Quantity > item.Barang.Stock)
                {
                    TempData["Error"] = $"Not enough stock for {item.Barang.NamaBarang}";
                    return RedirectToPage();
                }
            }

            // Generate invoice number
            var invoiceNumber = $"INV-{DateTime.Now:yyyyMMddHHmmss}";

            // Create PDF
            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // Add invoice header
            document.Add(new Paragraph("INVOICE").SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph($"Invoice Number: {invoiceNumber}"));
            document.Add(new Paragraph($"Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"));
            document.Add(new Paragraph($"Customer: {User.Identity.Name}"));
            document.Add(new Paragraph(""));

            // Create items table
            var table = new Table(4).UseAllAvailableWidth();
            table.AddCell("Item");
            table.AddCell("Price");
            table.AddCell("Quantity");
            table.AddCell("Subtotal");

            decimal total = 0;
            foreach (var item in cartItems)
            {
                table.AddCell(item.Barang.NamaBarang);
                table.AddCell(item.Barang.Harga.ToString("C", new CultureInfo("id-ID")));
                table.AddCell(item.Quantity.ToString());
                var subtotal = item.Barang.Harga * item.Quantity;
                table.AddCell(subtotal.ToString("C", new CultureInfo("id-ID")));
                total += subtotal;

                // Update stock
                item.Barang.Stock -= item.Quantity;
            }

            document.Add(table);
            document.Add(new Paragraph($"Total: {total.ToString("C", new CultureInfo("id-ID"))}").SetTextAlignment(TextAlignment.RIGHT));

            document.Close();

            // Clear cart
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            // Return PDF file
            var bytes = memoryStream.ToArray();
            return File(bytes, "application/pdf", $"{invoiceNumber}.pdf");
        }
    }
} 