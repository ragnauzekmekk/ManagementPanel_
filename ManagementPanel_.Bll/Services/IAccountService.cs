using ManagementPanel_.Model.ComplexType;
using ManagementPanel_.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Bll.Services
{
    public interface IAccountService
    {
        Result<User> Login(LoginModel model);
    }
}
