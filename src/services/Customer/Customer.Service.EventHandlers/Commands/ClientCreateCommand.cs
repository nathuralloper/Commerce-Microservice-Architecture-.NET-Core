using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.EventHandlers.Commands
{
    public class ClientCreateCommand: INotification
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
    }
}
