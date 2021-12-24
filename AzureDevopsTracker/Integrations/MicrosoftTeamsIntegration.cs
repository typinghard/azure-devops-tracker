using AzureDevopsTracker.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    internal class MicrosoftTeamsIntegration : MessageIntegration
    {
        public class MicrosoftTeamsMessage
        {
            public string type { get; set; }
            public string context { get; set; }
            public string themeColor { get; set; }
            public string summary { get; set; }
            public string priority { get; set; }
            public Section[] sections { get; set; }
        }

        public class Section
        {
            public string activityTitle { get; set; }
            public string activitySubtitle { get; set; }
            public string activityImage { get; set; }
            public string text { get; set; }
            public bool markdown { get; set; }
        }


        internal static class MicrosoftTeamsStrings
        {
            internal static string Type = "MessageCard";
            internal static string Context = "http://schema.org/extensions";
            internal static string ThemeColor = "7bd1d7";
        }

        internal override void Send(ChangeLog changeLog)
        {
            //Formatar o texto com o MicrosoftTeamsHelper

            //Faz o Post com a URL
            var url = MessageConfig.URL;
            var values = new MicrosoftTeamsMessage()
            {
                type = MicrosoftTeamsStrings.Type,
                context = MicrosoftTeamsStrings.Context,
                themeColor = MicrosoftTeamsStrings.ThemeColor,
                //summary = MicrosoftTeamsStrings.


            };

            var json = JsonContent.Create(values);

            HttpClient client = new HttpClient();
            var response = client.PostAsync(MessageConfig.URL, json).Result;

            //var responseString = await response.Content.ReadAsStringAsync();


            //Seta o retorno no ChangeLog

            //Retorna o ChangeLog
        }
    }
}
