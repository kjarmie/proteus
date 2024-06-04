using JetBrains.Annotations;

namespace RazorSlices;

/// <summary>
/// This interface provides functionality to render a Razor view to a string.
/// </summary>
public interface IRazorViewToStringRenderer
{
    /// <summary>
    /// Renders a Razor view to a string asynchronously.
    /// </summary>
    /// <param name="viewName">The name of the Razor view to render.</param>
    /// <param name="model">The model to pass to the view.</param>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <returns>A task that returns the rendered HTML as a string.</returns>
    public Task<string> RenderToStringAsync<TModel>([AspMvcView] string viewName, TModel model);

    /// <summary>
    /// Attempts to render a Razor view to a string based on the model type asynchronously.
    /// This method assumes that a model is unique to a view, ie. there is one and only one view for a model.
    /// </summary>
    /// <param name="model">The model to use for rendering.</param>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <returns>A task that returns the rendered HTML as a string.</returns>
    public Task<string> RenderToStringForModelAsync<T>([AspMvcView] T model);
}