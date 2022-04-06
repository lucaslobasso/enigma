
namespace Infrastructure.Options
{
    public class SqlServerOptions
    {
        public const string Section = "SqlServer";
        public string ConnectionString { get; set; } = string.Empty;
    }
}
