using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BTL.Controllers
{
    public class AccountController : BaseController
    {
        private readonly QLThuVienDBContext _context;
        public AccountController(QLThuVienDBContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ID,UserName,Password")] Account model)
        {
            if (ModelState.IsValid)
            {
                //Kiem tra username co ton tai ko
                var loginUser = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == model.UserName);
                if (loginUser == null)
                {
                    ModelState.AddModelError("", "Dang nhap that bai");
                    return View(model);
                }
                else
                {
                    // kiem tra ma MD5 cua password hien tai co khop voi MD5 cua password khong
                    SHA256 hashMethod = SHA256.Create();
                    if (Util.Cryptography.VerifyHash(hashMethod, model.Password, loginUser.Password))
                    {
                        //luu trang thai user
                        CurrenUser = loginUser.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "dang nhap that bai");
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Password")] Account model)
        {
            if (ModelState.IsValid)
            {
                //ma hoa mat khau
                SHA256 hashMethod = SHA256.Create();
                model.Password = Util.Cryptography.GetHash(hashMethod, model.Password);

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            CurrenUser = "";
            return RedirectToAction("Login");
        }
    }
}
