using System.Reflection;
using CCCLibrary.Serialization.Attributes;
using CCCLibrary.Serialization.Interfaces;

namespace CCCLibrary.Serialization;

public static class OutputGenerator
{
    public static string Generate(IOutputData output, IFormatProvider? formatProvider)
    {
        if (output is ISelfSerializing selfSerializingOutput)
        {
            return selfSerializingOutput.Serialize(formatProvider);
        }
        else
        {
            throw new NotImplementedException("You must use ISelfSerializing for now.");
            return SerializeIOutputData(output, formatProvider);
        }
    }


    private static string SerializeIOutputData(IOutputData output, IFormatProvider? formatProvider)
    {
        // TODO Ich will dass man in Level 2 von Level 1 ab
        IEnumerable<PropertyInfo> properties = SerializationUtilities.GetSerializableProperties(output.GetType())
            .Where(property => property.GetCustomAttribute<IgnoreAttribute>() == null)
            .OrderBy(property => property.GetCustomAttribute<PositionAttribute>()?.Position ?? int.MaxValue)
            .ThenBy(property => property.MetadataToken)
            .ThenBy(property => SerializationUtilities.GetInheritanceLevel(property.PropertyType), Comparer<int>.Create(new Comparison<int>((a, b) => a >= b ? a : b)));

        throw new NotImplementedException();
    }
}
