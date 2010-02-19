using AndyPike.ORMBattle.ARBaseClass.Models;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;

namespace AndyPike.ORMBattle.ARBaseClass.Controllers
{
    [Layout("Default")]
    public class HomeController : ARSmartDispatcherController
    {
        public void Index()
        {
            PropertyBag["tickets"] = Ticket.AllTicketsOrderedByDate();
        }
    }
}