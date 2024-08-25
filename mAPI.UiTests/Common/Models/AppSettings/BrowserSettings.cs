#nullable disable

namespace mAPI.UiTests.Common.Models.AppSettings;

public class BrowserSettings
{
    public string AppBaseUrl { get; set; }

    public string DownloadsPath { get; set; }

    public bool Headless { get; set; }

    public bool Incognito { get; set; }
}