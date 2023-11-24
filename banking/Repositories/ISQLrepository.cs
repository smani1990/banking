using banking.Models;

namespace banking.Repositories
{
    public interface ISQLrepository
    {
         Task<User?> CreateAccountAsync(User user);
        Task<Account?> GetUserAccountIdAsync(Guid id);
        Account? Transfer(Guid id, double amount, out string message);
         Task<User?> ValidateAccountAsync(string username,string password);
    }
}
