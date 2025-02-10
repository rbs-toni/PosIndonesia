using System.Reflection;
using Microsoft.JSInterop;
using PosIndonesia.Models;

namespace PosIndonesia.Services;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class JSInterop : IAsyncDisposable
{
    readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public JSInterop(IJSRuntime jsRuntime, string jSFile)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", $"./_content/PosIndonesia/{jSFile}").AsTask());
    }

    public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<TValue>(identifier, args);
    }

    public async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(identifier, args);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
