using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSSQLServerTest
{
    /// <summary>
    /// Checks for a newer release by comparing the current version against
    /// a plain-text version file hosted at <see cref="VersionUrl"/>.
    /// Replace the URLs with your actual release infrastructure.
    /// </summary>
    public static class UpdateChecker
    {
        public const string CurrentVersion = "1.0.0";

        // ── Replace these with your actual endpoints ──────────────────────────
        private const string VersionUrl  =
            "https://raw.githubusercontent.com/your-username/MSSQLServerTest/main/version.txt";
        private const string DownloadUrl =
            "https://github.com/your-username/MSSQLServerTest/releases/latest";

        public static async Task<Result> CheckAsync()
        {
            try
            {
                using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
                var raw = (await http.GetStringAsync(VersionUrl)).Trim();

                if (Version.TryParse(raw, out var latest) &&
                    Version.TryParse(CurrentVersion, out var current) &&
                    latest > current)
                {
                    return new Result(
                        HasUpdate:   true,
                        Latest:      raw,
                        Message:     $"A new version ({raw}) is available.\nWould you like to open the download page?",
                        DownloadUrl: DownloadUrl);
                }

                return new Result(
                    HasUpdate:   false,
                    Latest:      CurrentVersion,
                    Message:     $"You are running the latest version ({CurrentVersion}).",
                    DownloadUrl: string.Empty);
            }
            catch (Exception ex)
            {
                return new Result(
                    HasUpdate:   false,
                    Latest:      CurrentVersion,
                    Message:     $"Unable to check for updates.\n{ex.Message}",
                    DownloadUrl: string.Empty);
            }
        }

        public sealed record Result(
            bool   HasUpdate,
            string Latest,
            string Message,
            string DownloadUrl);
    }
}
