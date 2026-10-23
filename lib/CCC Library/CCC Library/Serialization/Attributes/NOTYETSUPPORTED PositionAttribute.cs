namespace CCCLibrary.Serialization.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class PositionAttribute : Attribute
{
    public int Position { get; init; }

    public PositionAttribute(int position)
    {
        Position = position;
    }
}
