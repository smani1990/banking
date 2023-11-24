using banking.Data;
using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Repositories
{
    public class SQLUserRepository : ISQLrepository
    {
        public DBRepositories DbContext { get; }
        public SQLUserRepository(DBRepositories dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<User?> CreateAccountAsync(User user)
        {
            var existingaccount = await DbContext.user.FirstOrDefaultAsync(x => x.Name == user.Name);
            if (existingaccount != null)
            {
                return user;
            }

            await DbContext.user.AddAsync(user);
            await DbContext.SaveChangesAsync();
            return null;

        }
        public async Task<User?> ValidateAccountAsync(string username, string password)
        {
            var existingaccount = await DbContext.user.Include("Account").FirstOrDefaultAsync(x => x.Name == username && x.Password == password);
           
            return existingaccount;

        }

        public async Task<Account?> GetUserAccountIdAsync(Guid id)
        {
            return await DbContext.account.FirstOrDefaultAsync(x => x.Id == id);

        }

        public Account? Transfer(Guid id, double amount,out string message)
        {
            message = "Success";
            var existingaccount =  DbContext.account.FirstOrDefault(x => x.Id == id);
            if (existingaccount == null)
            {
                message="Account does not exist";
                return existingaccount;
            }
            else if(existingaccount.Balance== amount)
            {
                message = "There is no more balance in your account. You will be levied a Minimum maintenance charge";
                return existingaccount;

            }
            else if (existingaccount.Balance < amount)
            {
                message = "There is insufficient funds in your account";
                return existingaccount;

            }
            message = "Success";
            existingaccount.Type = existingaccount.Type;
           existingaccount.Balance = existingaccount.Balance - amount;
            DbContext.account.Update(existingaccount);

             DbContext.SaveChanges();
            return existingaccount;




        }

    }
}
