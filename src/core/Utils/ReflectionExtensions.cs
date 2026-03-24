internal static class ActionExtensions
{
    internal static string TryGetName<T>(this Action<T> action)
    {
        // NOTE: although nullable objects are enabled here is better to be defensive about it
        if (action is null)
        {
            return "<null>";
        }

        var method = action.Method;
        var type = method.DeclaringType;

        var targetType = action.Target?.GetType();

        var className = targetType?.Name ?? type?.Name ?? "<unknown>";
        var methodName = method.Name;

        return $"{className}.{methodName}";
    }
}
