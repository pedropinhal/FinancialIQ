using System;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;
using FinancialIQ.Domain.Entities;


namespace FinancialIQ.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IFinancialIqDataContext _context;

        public UserRepository(IFinancialIqDataContext context)
        {
            _context = context;
        }

        public int AuthenticateUser(string username, string password)
        {
            var user = _context.Users.Where(
                u =>
                string.Equals(u.Username, username) &&
                string.Equals(Security.CreateHashedPassword(password, username), u.Password) 
                
                ).SingleOrDefault();
            
            return user != null ? user.Id : 0;
        }

        public User CreateUser(User user)
        {
            if (user.Id == 0)
            {
                //moneyLogEntry.Date = DateTime.Now;
                _context.Users.InsertOnSubmit(user);
            }
            else if (_context.Users.GetOriginalEntityState(user) == null)
            {
                _context.Users.Attach(user);
                _context.Users.Context.Refresh(RefreshMode.KeepCurrentValues, user);
            }
            try
            {
                _context.Users.Context.SubmitChanges();
            }catch(SqlException exception)
            {
                //log error
            }
            return user;

        }
    }
}