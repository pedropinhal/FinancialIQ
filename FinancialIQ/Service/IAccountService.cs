namespace FinancialIQ.Service {
    public interface IAccountService {
        int AuthenticateUser(string username, string password);
        int CreateUser(string username, string email, string password);
    }
}