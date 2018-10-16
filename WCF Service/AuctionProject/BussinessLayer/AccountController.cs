using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Database;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BussinessLayer
{
    public class AccountController : AController<Account>
    {
        public AccountRepository AccRepository
        {
            get
            {
                return base.Repository as AccountRepository;
            }
        }
        public AccountController() : base (new AccountRepository())
        {

        }

        public Account GetAccountByUsername(string username)
        {
            return AccRepository.GetAccountByUsername(username);
        }

        public bool Login(Account acc, string password)
        {
            if(acc != null)
                 return Verify(acc.Salt, acc.Password, password);
            return false;
        }

        public static bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }

        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        
        public Account HashAndSaltPassword(Account acc)
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

        public Account GetAccountByEmail(string email)
        {
           return AccRepository.GetAccountByEmail(email);
        }

        public override int Update(Account account)
        {
            return AccRepository.Update(account);
        }
    }
}
