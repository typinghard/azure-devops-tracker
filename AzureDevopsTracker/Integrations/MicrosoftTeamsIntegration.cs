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


        internal static class MicrosoftTeamsStatics
        {
            internal static readonly string Type = "MessageCard";
            internal static readonly string Context = "http://schema.org/extensions";
            internal static readonly string ThemeColor = "7bd1d7";
            internal static readonly int TEXT_SIZE_TO_BREAK_LINE = 100;
            internal static readonly int OPENING_DIV_SIZE = 5;
            internal static readonly int CLOSING_DIV_SIZE = 6;
        }

        internal override void Send(ChangeLog changeLog)
        {
            var values = new MicrosoftTeamsMessage()
            {
                type = MicrosoftTeamsStatics.Type,
                context = MicrosoftTeamsStatics.Context,
                themeColor = MicrosoftTeamsStatics.ThemeColor,
                summary = GetTitle(),
                sections = new Section[1]
                {
                    new Section()
                    {
                        activityTitle = GetTitle(),
                        activitySubtitle = GetVersion(changeLog),
                        activityImage = GetAnnouncementImageUrl(),
                        text = GetText(changeLog),
                        markdown = true
                    }
                }
            };

            Notify(values);
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
            var description = GetDescription(workItem.Description);
            var descriptionLine = $"<em>**{ workItem.WorkItemId }**</em> - { description }";
            if (description.Length > MicrosoftTeamsStatics.TEXT_SIZE_TO_BREAK_LINE)
                return $"<br>{ descriptionLine }<br>";
            return descriptionLine;
        }

        private string GetDescription(string description)
        {
            if (description.StartsWith("<div>"))
                description = description[MicrosoftTeamsStatics.OPENING_DIV_SIZE..];
            if(description.EndsWith("</div>"))
                description = description[0..^MicrosoftTeamsStatics.CLOSING_DIV_SIZE];
            return description;
        }

        private string GetFooter()
        {
            return $"<br><sup> <img src='{ GetLogoTypingHard16x16Url() }' /> { GetNugetVersion() }</sup>";
        }
    }
}
