using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AccountRepository : ARepository<Account>
    { 
        public AccountRepository(Dbcontext Context) : base(Context)
        {

        }

        public AccountRepository() : base()
        {

        }

        public Account GetAccountByUsername(string username)
        {
            return Set.FirstOrDefault(acc => acc.UserName.Equals(username));
        }

        public Account GetAccountByEmail(string email)
        {
            return Set.FirstOrDefault(acc => acc.Email.Equals(email));
        }

        public override Account getByIdWithObjects(int Id)
        {
            return GetById(Id);
        }

        public override int Update(Account account)
        {
            context.Accounts.Attach(account);
            var entry = context.Entry(account).State = EntityState.Modified;       
            return context.SaveChanges();
        }
    }
}
