using AzureDevopsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    internal class DiscordIntegration : MessageIntegration
    {
        internal override void Send(ChangeLog changeLog)
        {
            //Formatar o texto com o MarkdownHelper

            //Faz o Post com a URL
            var url = MessageConfig.URL;

            //Seta o retorno no ChangeLog

            //Retorna o ChangeLog
        }
    }
}
