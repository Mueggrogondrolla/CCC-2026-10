namespace CCCLibrary.Serialization.Interfaces;

public interface ISelfSerializing
{
    string Serialize(IFormatProvider? formatProvider);
}
