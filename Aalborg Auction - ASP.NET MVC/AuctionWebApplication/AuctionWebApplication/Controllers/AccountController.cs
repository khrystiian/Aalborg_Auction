using AuctionWebApplication.AuctionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuctionWebApplication.Controllers
{
    public class AccountController : Controller
    {
        IAuctionProjectService AccService = new AuctionProjectServiceClient("secure");


        #region GET METHODS

        // GET: Account
        public ActionResult Index()
        {
            return View(AccService.GetAllAccounts());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int id)
        {
            Account account = AccService.GetAccountById(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }


        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit()
        {
           return ReturnViewForLoggedUser();
        }

        private ActionResult ReturnViewForLoggedUser()
        {
            Account account = (Account)Session["LoggedUser"];
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }



        // GET: Accounts/Delete/5
        public ActionResult Delete()
        {
            return ReturnViewForLoggedUser();
        }

        public ActionResult Login()
        { 
            return View();
        }

        public ActionResult LogOut()
        {
            Session["LoggedUser"] = null;
            return RedirectToAction("Index", "Auction");
        }

        public ActionResult AllAuctionsWhereSeller(int accountId)
        {
            var auctions = AccService.GetAuctionsWhereSellerId(accountId);
            return View(auctions);
        }

        public ActionResult AllAuctionsWon(int accountId)
        {
            var auctions = AccService.GetAuctionsWhereWinnerId(accountId);
            return View(auctions);
        }

        public ActionResult AllAuctionsForAcc(int accountId)
        {
            var bid = AccService.GetAllBidsByAccountId(accountId);
            var auctions = AccService.GetAllAuctionsForBids(bid);
            if (auctions == null)
            {
                return HttpNotFound();
            }
            return View(auctions);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        #region Authentication
        private Account HashAndSaltPassword(Account acc)
        {
            // creates salt with random vales
            byte[] saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            acc.Salt = Convert.ToBase64String(saltBytes);
            // Create the Rfc2898DeriveBytes and get the hash value
            acc.Password = ComputeHash(acc.Salt, acc.Password);
            return acc;
        }
        private static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        public static bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }
        #endregion
         
        #endregion


        #region POST METHODS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string confirmPassword,
         [Bind(Include = "Id,UserName,Password,Email,Fname,Lname,Address,PhoneNumber")] Account account)
        {
            try
            {
                if (ModelState.IsValid && account.Password.Equals(confirmPassword))
                {
                    account.UserName = account.Email;
                    var password = account.Password;
                    account = HashAndSaltPassword(account);
                    AccService.AddAccount(account);
                    account.Password = password;
                    return Login(account);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is something wrong! Try again!.");
                    return View(account);
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "There is something wrong! Try again!.");
                return View(account);
            }

        }

        [HttpPost]
        public ActionResult AllAuctionsForAccount([Bind(Include = "Email")] Account account)
        {
            if (ModelState.IsValid)
            {
                account = AccService.GetAccountByEmail(account.Email);
                return RedirectToAction("Edit", "Auction");
            }
            return View(account);
        }

        [HttpPost]
        public ActionResult ChangePassword(string ConfirmPassword, string NewPassword, string OldPassword)
        {

            Account account = (Account) Session["LoggedUser"];
            if(Verify(account.Salt,account.Password, OldPassword) && NewPassword.Equals(ConfirmPassword))
            {
                account.Password = NewPassword;
                account = HashAndSaltPassword(account);
                AccService.UpdateAccount(account);
                Session["LoggedUser"] = account;
                return RedirectToAction("Index", "Auction");
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong! Try again!";
                return View();   
            }
        }

        
        [HttpPost]
        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
     
        [ValidateAntiForgeryToken]        
        public ActionResult Edit([Bind(Include = "Email,Fname,Lname,Address,PhoneNumber")] Account account)
        {
            if (ModelState.IsValid)
            {
                Account sessionAccount =(Account) Session["LoggedUser"];
                account.Id = sessionAccount.Id;
                account.Password = sessionAccount.Password;
                account.Salt = sessionAccount.Salt;
                account.UserName = sessionAccount.UserName;
                Session["LoggedUser"] = account;
                AccService.UpdateAccount(account);
                return RedirectToAction("Index","Auction");
            }else
            {
                ModelState.AddModelError(string.Empty, "There is something wrong! Try again!.");
            }
            return View();
        }
      
        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            Account account = (Account)Session["LoggedUser"];
            AccService.RemoveAccountById(account.Id);
            Session["LoggedUser"] = null;
            return RedirectToAction("Index","Auction");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Password")] Account account)
        {
           
            Account acc = AccService.GetAccountByUsername(account.UserName);
            if (acc != null){
             
                if (Verify(acc.Salt,acc.Password, account.Password))
                {
                    Session["LoggedUser"] = acc;
                    return RedirectToAction("Index", "Auction");
                }else
                TempData["ErrorMessage"] = "Wrong password";
            }
            else
                TempData["ErrorMessage"] = "Account with this username does not exist";
            return View("Login");
        }
        #endregion
    }
}