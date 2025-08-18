using AzureDevopsTracker.Entities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsTracker.Helpers
{
    public static class ReadJsonHelper
    {

        public static IEnumerable<WorkItemCustomField> ReadJson(string workItemId, string jsonTexto)
        {
            try
            {
                JObject json = JObject.Parse(jsonTexto);
                JObject? fields = json.SelectToken("resource.revision.fields") as JObject;
                if (fields == null)
                {
                    return Enumerable.Empty<WorkItemCustomField>();
                }
                var workItemCustomFields = new List<WorkItemCustomField>();

                foreach (var property in fields.Properties())
                {
                    if (property.Name.StartsWith("Custom."))
                    {
                        string key = property.Name["Custom.".Length..];
                        string value = property.Value.ToString();
                        workItemCustomFields.Add(new WorkItemCustomField(workItemId, key, value));
                    }
                }

                return workItemCustomFields;
            }
            catch
            {
                return [];
            }
        }
    }
}
