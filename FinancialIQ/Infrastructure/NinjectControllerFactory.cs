using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using FinancialIQ.Domain.Concrete;
using FinancialIQ.Domain.Data;
using FinancialIQ.Service;
using Ninject;
using Ninject.Modules;
using FinancialIQ.Domain.Abstract;

namespace FinancialIQ.Infrastructure {
    public class NinjectControllerFactory : DefaultControllerFactory {

        // A Ninject "kernel" is the thing that can supply object instances
        private IKernel kernel = new StandardKernel(new FinancialIqServices());

        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
            
        }

        // Configures how abstract service types are mapped to concrete implementations
        private class FinancialIqServices : NinjectModule
        {
            public override void Load()
            {
                Bind<IFinancialIqDataContext>()
                    .To<FinancialIqDataContext>()
                    .InTransientScope()
                    .WithConstructorArgument("connection",
                                             ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString);
                Bind<IMoneyFlowRepository>()
                    .To<MoneyFlowRepository>();

                Bind<IUserRepository>()
                    .To<UserRepository>();
                    

                Bind<IMoneyService>().To<MoneyService>();
                Bind<IAccountService>().To<AccountService>();
                
                
            }
        }

    }
}