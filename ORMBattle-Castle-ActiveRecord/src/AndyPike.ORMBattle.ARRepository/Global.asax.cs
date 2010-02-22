using System.Reflection;
using System.Web;
using AndyPike.ORMBattle.ARRepository.Persistence;
using Castle.Facilities.AutomaticTransactionManagement;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace AndyPike.ORMBattle.ARRepository
{
    public class Global : HttpApplication, IContainerAccessor
    {
        protected static IWindsorContainer container;

        public void Application_OnStart()
        {
            container = new WindsorContainer(new XmlInterpreter());

            RegisterFacilities();
            RegisterComponents();
            RegisterControllers();

            RegisterRoutes(RoutingModuleEx.Engine);
        }

        public void Application_OnEnd()
        {
            container.Dispose();
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public void RegisterFacilities()
        {
            container
                .AddFacility("logging.facility", new LoggingFacility(LoggerImplementation.Log4net, "Logging.config"))
                .AddFacility<MonoRailFacility>()
                .AddFacility<TransactionFacility>();
        }

        public void RegisterComponents()
        {
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(ActiveRecordRepository<>)));
        }

        public void RegisterControllers()
        {
            container
                .Register(
                AllTypes.Pick().FromAssembly(Assembly.GetExecutingAssembly())
                    .Configure(c => c.LifeStyle.Transient)
                    .If(c => c.Name.Contains("Controller"))
                );
        }

        public void RegisterRoutes(IRoutingRuleContainer rules)
        {
            rules.Add(new PatternRoute("standard", "[controller]/[action]/[id]")
                          .DefaultForArea().IsEmpty
                          .DefaultForController().Is("Blog")
                          .DefaultForAction().Is("Index"));
        }
    }
}