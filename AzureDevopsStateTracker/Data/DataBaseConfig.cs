namespace AzureDevopsStateTracker.Data
{
    public class DataBaseConfig
    {
        public DataBaseConfig(string connectionsString, string schemaName = "dbo")
        {
            ConnectionsString = connectionsString;
            SchemaName = schemaName;
        }

        public static string ConnectionsString { get; private set; }
        public static string SchemaName { get; private set; }
    }
}