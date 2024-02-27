using ecomerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ecomerce.Pages.Account
{
    public class LoginModel : PageModel
    {


        private AccountService _accountService = new AccountService();



        [BindProperty]
        public string capCha { get; set; }

        [BindProperty]
        public string validate { get; set; } = "";
        public void OnGet()
        {
            validate = GenerateRandomString();
            capCha = string.Join(" ", validate.ToCharArray());
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                setNotice("Input all the field");
                return Page();
            }
            try
            {
                var acc = _accountService.getAccount(Username, Password);
                if (acc == null)
                {
                    setNotice("Wrong Username or Password");
                    return Page();
                }
                var sessionStr = acc.Type ? "staff" : "customer";
                var accJson = JsonSerializer.Serialize<Models.Account>(acc);
                HttpContext.Session.SetString(sessionStr, accJson.ToString());
                return RedirectToPage("/Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void setNotice(string notice)
        {
            validate = GenerateRandomString();
            capCha = string.Join(" ", validate.ToCharArray());
            ViewData["notice"] = notice;
        }

        public static string GenerateRandomString()
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result += chars[rnd.Next(chars.Length)];
            }
            return result;
        }
    }
}
