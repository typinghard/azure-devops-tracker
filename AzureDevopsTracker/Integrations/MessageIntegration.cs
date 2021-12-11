using AzureDevopsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    internal abstract class MessageIntegration
    {
        internal abstract void Send(ChangeLog changeLog);
    }
}
