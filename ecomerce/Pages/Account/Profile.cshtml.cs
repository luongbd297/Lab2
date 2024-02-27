using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using System.Text.Json;

namespace ecomerce.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly ecomerce.Models.MyStoreContext _context;

        public ProfileModel(ecomerce.Models.MyStoreContext context)
        {
            _context = context;
        }

        public Models.Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var staff = HttpContext.Session.GetString("staff");
            var cus = HttpContext.Session.GetString("customer");
            string acc = null;
            if (staff != null) acc = staff;
            else if (cus != null) acc = cus;

            var account = JsonSerializer.Deserialize<Models.Account>(acc);
            if (account == null) return RedirectToPage("/Account/Login");
            else Account = account;
            return Page();
        }
    }
}
