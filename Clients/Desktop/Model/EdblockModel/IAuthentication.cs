using Edblock.UserManagementModel.Clients;

namespace EdblockModel;

public interface IAuthentication
{
    Task<UserModel> AuthenticateAccount(string login, string password);
}
