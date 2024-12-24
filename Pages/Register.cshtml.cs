using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pos_koperasi.Data;
using pos_koperasi.Models;
using System.ComponentModel.DataAnnotations;

namespace pos_koperasi.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly RazorPagesBarangContext _context;

        public RegisterModel(RazorPagesBarangContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new User
            {
                Username = Username,
                Password = Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
} 