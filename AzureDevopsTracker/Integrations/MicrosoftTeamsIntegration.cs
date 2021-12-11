using AzureDevopsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    internal class MicrosoftTeamsIntegration : MessageIntegration
    {
        internal override void Send(ChangeLog changeLog)
        {
            //Formatar o texto com o MicrosoftTeamsHelper

            //Faz o Post com a URL
            var url = MessageConfig.URL;

            //Seta o retorno no ChangeLog

            //Retorna o ChangeLog
        }
    }
}
