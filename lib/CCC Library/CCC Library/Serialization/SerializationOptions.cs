using System.Globalization;

namespace CCCLibrary.Serialization;

public sealed record SerializationOptions
{
    // TODO Im XML Kommentar sollten die Properties referenziert sein und nicht als literal Text integriert sein.
    /// <summary>
    /// Default Serialisierungsoptionen mit InvariantCulture als FormatProvider und ThrowsWhenDataRemainsAfterDeserialization=true.
    /// </summary>
    public static readonly SerializationOptions Default = new SerializationOptions(CultureInfo.InvariantCulture);


    public IFormatProvider? FormatProvider { get; init; }

    public bool ThrowWhenDataRemainsAfterDeserialisation { get; init; }

    public SerializationOptions(IFormatProvider? formatProvider, bool throwWhenDataRemainsAfterDeserialization = true)
    {
        FormatProvider = formatProvider;
        ThrowWhenDataRemainsAfterDeserialisation = throwWhenDataRemainsAfterDeserialization;
    }
}
