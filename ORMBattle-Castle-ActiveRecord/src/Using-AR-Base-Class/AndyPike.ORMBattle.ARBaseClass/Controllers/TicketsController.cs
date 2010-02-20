using AndyPike.ORMBattle.ARBaseClass.Models;
using Castle.ActiveRecord;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;

namespace AndyPike.ORMBattle.ARBaseClass.Controllers
{
    [Layout("Default")]
    public class TicketsController : ARSmartDispatcherController
    {
        public void Index()
        {
            PropertyBag["projects"] = Project.FindAllOrderedByName();
        }

        public void BugsFor([ARFetch]User user)
        {
            PropertyBag["user"] = user;
            PropertyBag["bugs"] = Ticket.FindBugsFor(user);
        }

        public void Edit([ARFetch]Ticket ticket)
        {
            PropertyBag["ticket"] = ticket;
        }

        [AccessibleThrough(Verb.Post)]
        public void Update([ARDataBind("ticket", AutoLoadBehavior.Always)]Ticket ticket)
        {
            try
            {
                ticket.SaveAndFlush();

                Flash["success"] = "Successfully updated ticket";
                RedirectToAction("Index");
            }
            catch (ActiveRecordValidationException ex)
            {
                PropertyBag["errors"] = ex;
                PropertyBag["ticket"] = ticket;
                RenderView("Edit");
            }
        }

        public void New()
        {
            //TODO: This is left for the audience to complete ;o)
        }
    }
}