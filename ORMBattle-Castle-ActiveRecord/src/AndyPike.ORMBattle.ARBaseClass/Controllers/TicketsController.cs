using System;
using AndyPike.ORMBattle.ARBaseClass.Models;
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
            PopulatePropertyBagForTicket(ticket);
        }

        [AccessibleThrough(Verb.Post)]
        public void Update([ARDataBind("ticket", AutoLoadBehavior.Always)]Ticket ticket)
        {
            if(ticket.IsValid())
            {
                ticket.Save();

                Flash["success"] = "Successfully updated ticket";
                RedirectToAction("Index");
            }
            else
            {
                PropertyBag["errors"] = ticket.ValidationErrorMessages;
                PopulatePropertyBagForTicket(ticket);

                RenderView("Edit");
            }
        }

        public void New()
        {
            PopulatePropertyBagForTicket(new Ticket());
        }

        [AccessibleThrough(Verb.Post)]
        public void Create([DataBind("ticket")]Ticket ticket)
        {
            if(ticket.IsValid())
            {
                ticket.CreatedAt = DateTime.Now;
                ticket.Save();

                Flash["success"] = "Successfully created ticket";
                RedirectToAction("Index");
            }
            else
            {
                PropertyBag["errors"] = ticket.ValidationErrorMessages;
                PopulatePropertyBagForTicket(ticket);

                RenderView("Edit");
            }
        }

        private void PopulatePropertyBagForTicket(Ticket ticket)
        {
            PropertyBag["ticket"] = ticket;
            PropertyBag["projects"] = Project.FindAllOrderedByName();
            PropertyBag["users"] = User.FindAll();
            PropertyBag["types"] = Enum.GetNames(typeof(TicketType));
        }
    }
}