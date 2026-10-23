namespace CCCLibrary.Serialization.Attributes;

/// <remarks>
/// Method signature: public object? METHOD_NAME(IEnumerator<string> inputTokens, IFormatProvider? formatProvider)
/// </remarks>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class CustomParseMethodAttribute : Attribute
{
    public string MethodName { get; init; }

    public CustomParseMethodAttribute(string methodName)
    {
        MethodName = methodName;
    }
}
