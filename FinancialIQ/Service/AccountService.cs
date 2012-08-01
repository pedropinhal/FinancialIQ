using System.Linq;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;
using FinancialIQ.Domain.Entities;


namespace FinancialIQ.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AuthenticateUser(string username, string password)
        {
            return _userRepository.AuthenticateUser(username, password);
        }

        public int CreateUser(string username, string email, string password)
        {
            var user = new User {Username = username, Password = Security.CreateHashedPassword(password, username), Email = email};
            var createdUser = _userRepository.CreateUser(user);
            if (createdUser != null) { return createdUser.Id; }
            return 0;
        }
    }
}