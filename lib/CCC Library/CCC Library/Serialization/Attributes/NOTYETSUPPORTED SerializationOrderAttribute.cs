namespace CCCLibrary.Serialization.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class SerializationOrderAttribute : Attribute
{
    public string[] PropertyOrder { get; init; }

    public SerializationOrderAttribute(params string[] propertyOrder)
    {
        PropertyOrder = propertyOrder;
    }
}
