namespace Token_based_authentication_and_middleware.Helpers.Utils.GlobalAttributes
{
    public static class GlobalAttributes
    {
        public static MySQLConfiguration mySQLConfiguration = new MySQLConfiguration();
    }

    public class MySQLConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
