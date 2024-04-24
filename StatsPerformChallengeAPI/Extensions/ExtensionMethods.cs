namespace StatsPerformChallengeAPI.Extensions
{
    public static class ExtensionMethods
    {
        private static string SasToken = string.Empty;

        public static string AppendSasToken(this string str)
        {
            if (SasToken == string.Empty)
            {
                ConfigurationBuilder configurationBuilder = new();
                IConfiguration configuration = configurationBuilder.AddUserSecrets<Program>().Build();
                SasToken = configuration.GetSection("protectedFiles")["sasToken"] ?? string.Empty;
            }
            return str + "?" + SasToken;
        }
    }
}
