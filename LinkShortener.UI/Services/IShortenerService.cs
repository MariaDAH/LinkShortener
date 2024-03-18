﻿using LinkShortener.UI.Pages;

namespace LinkShortener.UI.Services;

public interface IShortenerService
{
    Task<LinkShare.Link?> ShareLink(string originalUrl, bool format, CancellationToken cancellationToken = default);

    Task BrowseShortLink(string url, bool format, CancellationToken cancellationToken = default);
}