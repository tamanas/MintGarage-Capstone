using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class AccountController : Controller
    {
        public IAccountRepository accoutRepository;

        public AccountController(IAccountRepository accountRepo)
        {
            accoutRepository = accountRepo;
        }

        public IActionResult Login()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                Account acc = accoutRepository.Account.FirstOrDefault();
                if (acc.Username == account.Username && acc.Password == account.Password)
                {
                    return RedirectToAction("Update", "Home");
                }
                else
                {
                    TempData["Message"] = "Invalid username or password. Please try again!";
                    TempData["Success"] = false;
                    return RedirectToAction("Login");
                }
            }
            return View(account);
        }

        public IActionResult Update()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdatePassword updatePassword)
        {
            if (ModelState.IsValid)
            {
                Account acc = accoutRepository.Account.FirstOrDefault();
                
                if (!acc.Password.Equals(updatePassword.CurrectPassword))
                {
                    TempData["Message"] = "Incorrect current password. Please try again!";
                    TempData["Success"] = false;
                }
                if (acc.Password.Equals(updatePassword.CurrectPassword))
                {
                    acc.Password = updatePassword.NewPassword;
                    accoutRepository.Update(acc);
                    TempData["Message"] = "Password updated successfully.";
                    TempData["Success"] = true;
                }
                return RedirectToAction("Update");
            }
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("index", "Home");
        }
    }
}
