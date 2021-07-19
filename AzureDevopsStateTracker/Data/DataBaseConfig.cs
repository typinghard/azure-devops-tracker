using AzureDevopsStateTracker.Extensions;
using System;

namespace AzureDevopsStateTracker.Data
{
    public class DataBaseConfig
    {
        public DataBaseConfig(string connectionsString, string schemaName = "dbo")
        {
            if (connectionsString.IsNullOrEmpty())
                throw new ArgumentException("The ConnectionsString is required");

            ConnectionsString = connectionsString;
            SchemaName = schemaName;
        }

        public static string ConnectionsString { get; private set; }
        public static string SchemaName { get; private set; }
    }
}