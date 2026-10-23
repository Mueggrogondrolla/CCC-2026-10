using System.Collections;
using System.Reflection;
using CCCLibrary.Serialization.Attributes;

namespace CCCLibrary.Serialization;

internal static class SerializationUtilities
{
    internal static IEnumerable<PropertyInfo> GetSerializableProperties(Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(property => property.CanWrite);
    }

    internal static int GetInheritanceLevel(Type type)
    {
        int level = 0;

        while (type.BaseType != null)
        {
            level++;
            type = type.BaseType;
        }

        return level;
    }

    /// <summary>
    /// Ordnet die übergebenen Properties nach ihren Positionsattributen, dem Vererbungslevel ihrer deklarierenden Klasse
    /// und nach der Deklarationsreihenfolge innerhalb dieser Klasse.
    /// </summary>
    /// <remarks>
    /// Positionsattribute haben Vorrang, danach das Vererbungslevel und zum Schluss die Deklarationsreihenfolge.
    /// Properties aus Basisklassen haben Vorrang gegenüber Properties aus Subklassen.
    /// 
    /// Wird dieselbe Position (des Positionsattribut) in einer Klasse mehrfach verwendet, hat die zuerst deklarierte Property Vorrang.
    /// Wird dieselbe Position (des Positionsattribut) in einer Sub- und Basisklasse verwendet, hat die Property in der Subklasse Vorrang.
    /// </remarks>
    internal static List<PropertyInfo> OrderPropertiesByPositionAttributeAndInheritanceLevelAndDeclarationOrder(IEnumerable<PropertyInfo> properties)
    {
        // Sort properties by inheritance level and declaration order (base class top-down first, then subclass top-down)
        // (from https://stackoverflow.com/a/31253200)
        properties = properties.OrderBy(property => property.MetadataToken);

        List<PropertyInfo> propertiesWithPosition = new List<PropertyInfo>();
        Queue<PropertyInfo> propertiesWithoutPosition = new Queue<PropertyInfo>();

        foreach (var property in properties)
        {
            if (property.GetCustomAttribute<PositionAttribute>() != null)
            {
                propertiesWithPosition.Add(property);
            }
            else
            {
                propertiesWithoutPosition.Enqueue(property);
            }
        }

        Dictionary<int, List<PropertyInfo>> propertiesByPosition = propertiesWithPosition
            .GroupBy(property => property.GetCustomAttribute<PositionAttribute>()!.Position)
            .ToDictionary(group => group.Key, group => group.ToList());

        int minPosition = propertiesByPosition.Any() ? propertiesByPosition.Keys.Min() : 0;
        int maxPosition = propertiesByPosition.Any() ? propertiesByPosition.Keys.Max() : -1;

        if (minPosition > 0)
        {
            minPosition = 0;
        }

        List<PropertyInfo> orderedProperties = new List<PropertyInfo>();

        for (int position = minPosition; position <= maxPosition; position++)
        {
            if (propertiesByPosition.TryGetValue(position, out var propertyList))
            {
                // Properties are already ordered by Metadatatoken here (subclass top-down first, then base class top-down)
                orderedProperties.AddRange(propertyList);
            }
            else if (position >= 0 && propertiesWithoutPosition.TryDequeue(out var property))
            {
                orderedProperties.Add(property);
            }
        }

        while (propertiesWithoutPosition.TryDequeue(out var property))
        {
            orderedProperties.Add(property);
        }

        return orderedProperties;
    }



    internal static bool IsArray(Type type)
    {
        return type.IsArray && type.GetArrayRank() >= 1;
    }

    internal static bool IsCollection(Type type)
    {
        return type.GetInterfaces().Any(@interface => @interface == typeof(ICollection) || @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(ICollection<>));
    }

    internal static bool IsGenericCollection(Type type)
    {
        return type.IsGenericType && type.GetInterfaces().Any(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(ICollection<>));
    }

    internal static bool IsDictionary(Type type)
    {
        return type.GetInterfaces().Any(@interface => @interface == typeof(IDictionary) || @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IDictionary<,>));
    }

    internal static bool IsKeyValuePair(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
    }
}
