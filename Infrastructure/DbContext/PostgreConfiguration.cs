namespace Infrastructure.DbContext
{
    public class PostgreConfiguration
    {
        public PostgreConfiguration(string connectionString) => ConnectionString = connectionString;
        public string ConnectionString { get; set; }
    }
}
