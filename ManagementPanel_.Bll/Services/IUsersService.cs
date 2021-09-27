using ManagementPanel_.Model.ComplexType;
using ManagementPanel_.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Bll.Services
{
    public interface IUsersService
    {
        Result<User> GetUserList();
        Result AddUser(UserModel model);
        Result<User> GetUser(int id);
        Result UpdateUser(UserModel model);
        Result DeleteUser(int id);

        Result GetEmailPhoneUsernameControl(string email, string phone, string username);
    }
}
