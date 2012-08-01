using System.Collections.Generic;
using System.Linq;
using FinancialIQ.Domain.Data;


namespace FinancialIQ.Domain.Abstract
{
    public interface IUserRepository
    {
        int AuthenticateUser(string username, string password);
        User CreateUser(User user);
    }
}