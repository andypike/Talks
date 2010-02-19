using System.Reflection;
using System.Web;
using Castle.ActiveRecord;
using Castle.Facilities.AutomaticTransactionManagement;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace AndyPike.ORMBattle.ARBaseClass
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

            //NHibernateProfiler.Initialize();
            ActiveRecordStarter.CreateSchema();
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
            rules.Add(new PatternRoute("Root", "/")
                          .DefaultForArea().IsEmpty
                          .DefaultForController().Is("Home")
                          .DefaultForAction().Is("Index"));

            rules.Add(new PatternRoute("[controller]/[action]/[id]")
                          .DefaultForArea().IsEmpty
                          .DefaultForController().Is("Home")
                          .DefaultForAction().Is("Index"));
        }
    }
}