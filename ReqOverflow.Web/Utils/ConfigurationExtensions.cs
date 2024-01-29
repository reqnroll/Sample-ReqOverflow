using System;
using Microsoft.Extensions.Configuration;

namespace ReqOverflow.Web.Utils
{
    public static class ConfigurationExtensions
    {
        public static bool AllowVotingForYourItems(this IConfiguration configuration)
            => configuration?.GetValue<bool>("AppSettings:AllowVotingForYourItems") ?? false;
    }
}
