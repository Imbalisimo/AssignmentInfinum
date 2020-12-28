using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentInfinum
{
    public class ContactsHub : Hub
    {
        public void Send(string name, string birthDate, string address)
        {
            Clients.All.sendNewContact(name, birthDate, address);
        }
    }
}