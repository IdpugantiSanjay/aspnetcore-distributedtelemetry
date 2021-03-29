using System.Diagnostics;

namespace Plutus.Api.Common
{
    public static class AppActivitySource
    {
        public const string Name = "Plutus.WebApi";
        public static ActivitySource Instance { get; } = new ActivitySource(Name);
    }
}