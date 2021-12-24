using AzureDevopsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    internal abstract class MessageIntegration
    {
        internal abstract void Send(ChangeLog changeLog);

        internal string GetTitle(ChangeLog changeLog)
        {
            System.Resources.ResourceManager mgr = new
                System.Resources.ResourceManager("AzureDvevopsTracker.Resource",
                System.Reflection.Assembly.GetExecutingAssembly());

            return "";
        }
    }
}
