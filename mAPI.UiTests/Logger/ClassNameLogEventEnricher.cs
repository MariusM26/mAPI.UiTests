using Serilog.Core;
using Serilog.Events;

namespace mAPI.UiTests.Logger;
internal class ClassNameLogEventEnricher : ILogEventEnricher
{
    private static readonly string NamespacePrefix = $"{typeof(ClassNameLogEventEnricher).Namespace!.Split('.')[0]}.";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.TryGetValue("SourceContext", out var sourceContextValue))
        {
            if (sourceContextValue is ScalarValue sourceContext)
            {
                var className = sourceContext.Value?.ToString();

                if (className != null)
                {
                    if (className.StartsWith(NamespacePrefix, StringComparison.Ordinal))
                    {
                        var classNameParts = className.Split('.');

                        className = classNameParts[^1];
                    }

                    var classNameProperty = propertyFactory.CreateProperty("ClassName", className);

                    logEvent.AddPropertyIfAbsent(classNameProperty);
                }
            }
        }
    }
}