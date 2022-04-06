
namespace Infrastructure.Options
{
    public class AuthenticationOptions
    {
        public const string Section = "Authentication";
        public string Token { get; set; } = string.Empty;
    }
}
