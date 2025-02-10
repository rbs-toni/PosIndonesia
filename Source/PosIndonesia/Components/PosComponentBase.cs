using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace PosIndonesia;
public abstract class PosComponentBase : ComponentBase
{
    [Inject]
    ILoggerFactory LoggerFactory { get; set; } = default!;
    ILogger? _logger;
    protected ILogger Logger => _logger ??= LoggerFactory.CreateLogger(GetType());
    ElementReference _ref;
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Class { get; set; }
    [Parameter]
    public string? Style { get; set; }
    [Parameter]
    public ElementReference Ref
    {
        get
        {
            return _ref;
        }
        set
        {
            _ref = value;
            RefChanged.InvokeAsync(_ref);
        }
    }
    [Parameter]
    public EventCallback<ElementReference> RefChanged { get; set; }
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? Attributes { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}

public abstract class PosJSComponentBase : PosComponentBase, IAsyncDisposable
{
    [Inject]
    IJSRuntime JSRuntime { get; set; } = default!;
    protected PosJSComponentBase(string jSFile)
    {
        JSFile = jSFile;
    }

    public string JSFile { get; }
    public IJSObjectReference? JSModule { get; private set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            JSModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/PosIndonesia/Components/{JSFile}.razor.js");
            await OnAfterImportAsync();
        }
    }
    public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object?[]? args)
    {
        if (JSModule != null)
        {
            return await JSModule.InvokeAsync<TValue>(identifier, args);
        }
        return default!;
    }

    public async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        if (JSModule != null)
        {
            await JSModule.InvokeVoidAsync(identifier, args);
        }
    }
    public virtual Task OnAfterImportAsync() => Task.CompletedTask;
    public virtual Task OnDisposingAsync() => Task.CompletedTask;
    public async ValueTask DisposeAsync()
    {
        try
        {
            await OnDisposingAsync();
            if (JSModule != null)
            {
                await JSModule.DisposeAsync();
            }
        }
        catch (Exception)
        {
        }
    }
}
