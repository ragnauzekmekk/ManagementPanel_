using ManagementPanel_.Bll.ServiceManagers;
using ManagementPanel_.Bll.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ManagementPanel_
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IUsersService, UsersServiceManager>();
            container.RegisterType<IAccountService, AccountServiceManager>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}