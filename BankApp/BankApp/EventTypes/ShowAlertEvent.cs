using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;

namespace BankApp.EventTypes
{
    class ShowAlertEvent : PubSubEvent<string>
    {
    }
}
