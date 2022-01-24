using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Statics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDevopsTracker.Integrations
{
    internal class MicrosoftTeamsIntegration : MessageIntegration
    {
        internal class MicrosoftTeamsMessage
        {
            public string type { get; set; }
            public string context { get; set; }
            public string themeColor { get; set; }
            public string summary { get; set; }
            public string priority { get; set; }
            public Section[] sections { get; set; }
        }

        internal class Section
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

            var values = new MicrosoftTeamsMessage()
            {
                type = MicrosoftTeamsStrings.Type,
                context = MicrosoftTeamsStrings.Context,
                themeColor = MicrosoftTeamsStrings.ThemeColor,
                summary = GetTitle(changeLog),
                sections = new Section[1]
                {
                    new Section()
                    {
                        activityTitle = GetTitle(changeLog),
                        activitySubtitle = $"Versão: { changeLog.Number }",
                        activityImage = GetAnnouncementImageUrl(),
                        text = GetText(changeLog),
                        markdown = true
                    }
                }
            };

            var response = Notify(values);

            //Seta o retorno no ChangeLog

            //Retorna o ChangeLog
        }

        private string GetText(ChangeLog changeLog)
        {
            if (changeLog == null || !changeLog.ChangeLogItems.Any()) return string.Empty;

            StringBuilder text = new StringBuilder();
            text.Append(GetWorkItemsDescriptionSection("Features", changeLog.ChangeLogItems.Where(x => x.WorkItemType != WorkItemStatics.WORKITEM_TYPE_BUG)));
            text.Append(GetWorkItemsDescriptionSection("Correções", changeLog.ChangeLogItems.Where(x => x.WorkItemType == WorkItemStatics.WORKITEM_TYPE_BUG)));

            text.Append(GetFooter());
            return text.ToString();
        }

        private string GetWorkItemsDescriptionSection(string sectionName, IEnumerable<ChangeLogItem> changeLogItems)
        {
            StringBuilder text = new StringBuilder();
            if (!changeLogItems.Any()) return string.Empty;

            text.Append("<br>");
            text.AppendLine("\n");

            text.Append($"\n  # **{ sectionName }**\n");
            foreach (var workItem in changeLogItems)
            {
                text.Append(GetWorkItemDescriptionLine(workItem));
            }

            text.Append("<br>");
            text.AppendLine("\n");
            return text.ToString();
        }

        private string GetWorkItemDescriptionLine(ChangeLogItem workItem)
        {
            return $"<br><em>{ workItem.WorkItemId }</em> - { workItem.Description }";
        }

        private string GetFooter()
        {
            return $"<br><sup> <img src='{ GetLogoTypingHard16x16Url() }' />{ GetNugetVersion() }</sup>";
        }
    }
}
