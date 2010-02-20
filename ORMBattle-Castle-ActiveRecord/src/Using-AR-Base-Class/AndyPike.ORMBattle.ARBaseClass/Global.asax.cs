using System;
using System.Reflection;
using System.Web;
using AndyPike.ORMBattle.ARBaseClass.Models;
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

            InitializeActiveRecord();
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
            //Register your own components with the container here
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
                          .DefaultForController().Is("Tickets")
                          .DefaultForAction().Is("Index"));
        }

        private void InitializeActiveRecord()
        {
            //NHibernateProfiler.Initialize();
            ActiveRecordStarter.CreateSchema();

            //Add some data for the demo
            using(new SessionScope())
            {
                var andy = new User{ Name = "Andy Pike" };
                andy.Save();

                var amber = new User{ Name = "Amber Pike" };
                amber.Save();

                var demo = new Project{ Name = "Demo Project" };
                demo.Save();

                var secret = new Project { Name = "My top secret project" };
                secret.Save();

                var ie6Bug = new Ticket
                                 {
                                     Type = TicketType.Bug, 
                                     Summary = "The boxes do not line up in IE6 ;o)", 
                                     Body = "If you go to any page on the site it look terrible in IE6.", 
                                     AssignedTo = amber,
                                     CreatedAt = DateTime.Now,
                                     Project = demo
                                 };
                ie6Bug.Save();

                var addProfilePictures = new Ticket
                                            {
                                                Type = TicketType.FeatureRequest,
                                                Summary = "Show a user's photo when they login",
                                                Body = "Show a user's Gravatar based on their email. See documentation on gravatar.com.",
                                                AssignedTo = andy,
                                                CreatedAt = DateTime.Now,
                                                Project = demo
                                            };
                addProfilePictures.Save();

                var filters = new Ticket
                                {
                                    Type = TicketType.Bug,
                                    Summary = "User filters not working",
                                    Body = "When filtering the list of users, none of the options seem to change the list.",
                                    AssignedTo = andy,
                                    CreatedAt = DateTime.Now,
                                    Project = secret
                                };
                filters.Save();
            }
        }
    }
}