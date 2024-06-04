using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorSlices;
using Webapp.lib.Razor;

namespace Webapp.Features.Home;

[HttpGet(Examples.Url)]
[AllowAnonymous]
public class ExamplesEndpoint : EndpointWithoutRequest
{
    private readonly IRazorViewToStringRenderer _renderer;

    public ExamplesEndpoint(IRazorViewToStringRenderer renderer)
    {
        _renderer = renderer;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var content = await _renderer.RenderToStringForModelAsync(new Examples());
        await SendResultAsync(Results.Content(content, "text/html"));
    }
}

public class Examples
{
    public const string Url = "/examples";
}