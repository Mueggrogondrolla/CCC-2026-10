namespace CCCLibrary.Serialization.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class CountPropertiesAttribute : Attribute
{
    public string[] CountPropertyNames { get; init; }

    public CountPropertiesAttribute(params string[] countPropertyNames)
    {
        CountPropertyNames = countPropertyNames;
    }
}
