using System.Reflection;
using System.Runtime.Serialization;
using CCCLibrary.Serialization.Attributes;
using CCCLibrary.Serialization.Interfaces;

namespace CCCLibrary.Serialization;

// TODO It should be possible to deserialize private properties too! Then we can deserialize dirty and provide nicely converted values publicly in the class.
/// <summary>
/// Ermöglicht das einfache Parsen von Catalyst-Coding-Contest Input Dateien mittels Attributen und Reflection.
/// </summary>
/// <seealso cref="IInputData"/>
/// <seealso cref="CountPropertiesAttribute"/>
/// <seealso cref="CustomParseMethodAttribute"/>
public static class InputParser
{
    private static readonly MethodInfo ParseIInputDataObjectMethod = typeof(InputParser).GetMethod(nameof(InputParser.ParseIInputDataObject), BindingFlags.NonPublic | BindingFlags.Static) ?? throw new Exception("Specified method was not found with reflection.");

    public static T Parse<T>(string inputFile, SerializationOptions serializationOptions) where T : IInputData, new()
    {
        string fileContents = File.ReadAllText(inputFile);
        //
        // //InputLexer lexer = new InputLexer(new[] { ' ', '\n', '\r', '\t' });
        // InputLexer lexer = new InputLexer(new[] { '\n', '\r', '\t' });
        //
        // IEnumerable<string> inputTokens = lexer.GetTokens(fileContents);
        //
        // return Parse<T>(inputTokens, serializationOptions);

        return Parse<T>([fileContents], serializationOptions);
    }

    public static T Parse<T>(IEnumerable<string> inputTokens, SerializationOptions serializationOptions) where T : IInputData, new()
    {
        return Parse<T>(inputTokens.GetEnumerator(), serializationOptions);
    }

    public static T Parse<T>(IEnumerator<string> inputTokens, SerializationOptions serializationOptions) where T : IInputData, new()
    {
        T obj = ParseIInputDataObject<T>(inputTokens, serializationOptions.FormatProvider);

        if (serializationOptions.ThrowWhenDataRemainsAfterDeserialisation && inputTokens.MoveNext())
        {
            throw new SerializationException("Deserialization of the object has finished but there is left over data.");
        }

        return obj;
    }


    #region Implizite Version

    //private static List<object?>? DeserializedPropertyValues;
    //private static (PropertyInfo property, int[] countIndices)[]? PropertiesWithIndices;

    //public static T Parse<T>(IEnumerator<string> inputTokens, IFormatProvider? formatProvider, bool throwWhenDataRemains = true) where T : IInputData, new()
    //{
    //    DeserializedPropertyValues = new List<object?>();
    //    PropertiesWithIndices = BuildCountTree(typeof(T));

    //    T obj = ParseIInputDataObject<T>(inputTokens, formatProvider);

    //    if (throwWhenDataRemains && inputTokens.MoveNext())
    //    {
    //        throw new SerializationException("Deserialization of the object has finished but there is left over data.");
    //    }

    //    return obj;
    //}


    //private static (PropertyInfo property, int[] countIndices)[] BuildCountTree(Type type)
    //{
    //    List<PropertyInfo> properties = new List<PropertyInfo>();

    //    AddAllPropertiesAndSubPropertiesOfType(type, properties);

    //    int lastIndex = properties.Count;
    //    (PropertyInfo property, int[] countIndices)[] propertiesWithIndices = new (PropertyInfo property, int[] countIndices)[properties.Count];

    //    for (int i = properties.Count - 1; i >= 0; i--)
    //    {
    //        var rank = GetRank(properties[i].PropertyType);

    //        int[] indices = new int[rank];

    //        if (lastIndex > i - 1)
    //        {
    //            lastIndex = i - 1;
    //        }

    //        for (int j = indices.Length - 1; j >= 0; j--)
    //        {
    //            for (int k = lastIndex; k >= 0; k--)
    //            {
    //                lastIndex--;

    //                if (IsInteger(properties[k].PropertyType))
    //                {
    //                    indices[j] = k;
    //                    break;
    //                }

    //                if (lastIndex < 0)
    //                {
    //                    throw new SerializationException("Seems like there are not enough integer properties to implicitly retrieve counts for the lists in the data type so we can deserialize them.");
    //                }
    //            }
    //        }

    //        propertiesWithIndices[i] = (properties[i], indices);
    //    }

    //    return propertiesWithIndices;


    //    bool IsInteger(Type type)
    //    {
    //        return Type.GetTypeCode(type) is (TypeCode.Byte or TypeCode.SByte or TypeCode.Int16 or TypeCode.UInt16 or TypeCode.Int32 or TypeCode.UInt32 or TypeCode.Int64 or TypeCode.UInt64);
    //    }

    //    void AddAllPropertiesAndSubPropertiesOfType(Type type, List<PropertyInfo> properties)
    //    {
    //        if (!type.IsAssignableTo(typeof(IInputData)))
    //        {
    //            return;
    //        }

    //        foreach (var property in GetSerializableProperties(type))
    //        {
    //            properties.Add(property);

    //            if (property.PropertyType.IsAssignableTo(typeof(IInputData)))
    //            {
    //                AddAllPropertiesAndSubPropertiesOfType(property.PropertyType, properties);
    //            }

    //            if (IsArray(property.PropertyType))
    //            {
    //                AddAllPropertiesAndSubPropertiesOfType(property.PropertyType.GetElementType(), properties);
    //            }

    //            if (IsCollection(property.PropertyType))
    //            {
    //                AddAllPropertiesAndSubPropertiesOfType(GetGenericCollectionElementType(property.PropertyType), properties);
    //            }

    //            if (IsDictionary(property.PropertyType))
    //            {
    //                AddAllPropertiesAndSubPropertiesOfType(property.PropertyType.GenericTypeArguments[0], properties);
    //                AddAllPropertiesAndSubPropertiesOfType(property.PropertyType.GenericTypeArguments[1], properties);
    //            }
    //        }
    //    }
    //}

    //private static int GetTotalNumberOfRequiredCountProperties(Type type)
    //{
    //    if (type.IsAssignableTo(typeof(IInputData)))
    //    {
    //        return GetSerializableProperties(type).Select(property => GetTotalNumberOfRequiredCountProperties(property.PropertyType)).Sum();
    //    }

    //    if (IsArray(type))
    //    {
    //        return type.GetArrayRank() + GetRank(type.GetElementType() ?? throw new SerializationException($"{type} was recognized as an array, but it does not have an element type."));
    //    }

    //    if (IsCollection(type))
    //    {
    //        return 1 + (type.IsGenericType ? GetRank(GetGenericCollectionElementType(type)) : 0);
    //    }

    //    if (IsDictionary(type))
    //    {
    //        // TODO Get Type Arguments of IDictionary<,> interface instead of object type as it is done for ICollection<> above
    //        return 1 + GetRank(type.GenericTypeArguments[0]) + GetRank(type.GenericTypeArguments[1]);
    //    }

    //    return 0;
    //}

    //private static IEnumerable<PropertyInfo> GetSerializableProperties(Type type)
    //{
    //    return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(property => property.CanWrite);
    //}



    //// TODO CANNOT WORK YET BECAUSE WHEN A IINPUTTYPE IN A LIST TRIES TO GET THE COUNT PROPERTY FROM ITS OWN VALUE THEN IT DOES NOT GET IT BECAUSE WHEN WE DETERMINE INDICES WE DO NOT USE NOR HAVE THE LENGTH OF THE LIST TO GET THE CORRECT INDEX!
    //// Version mit impliziter Count Ermittlung
    //private static T ParseIInputDataObject<T>(IEnumerator<string> inputTokens, IFormatProvider? formatProvider) where T : IInputData, new()
    //{
    //    T obj = new T();

    //    foreach (PropertyInfo property in GetSerializableProperties(typeof(T)))
    //    {
    //        if (property.GetCustomAttribute<CustomParseMethodAttribute>() is CustomParseMethodAttribute customParseMethodAttribute)
    //        {
    //            throw new NotImplementedException();
    //        }
    //        else if (GetRank(property.PropertyType) > 0)
    //        {
    //            CountPropertiesAttribute? countPropertiesAttribute = property.GetCustomAttribute<CountPropertiesAttribute>();

    //            int rank = GetRank(property.PropertyType);

    //            int[] lengths = new int[rank];

    //            if (countPropertiesAttribute != null)
    //            {
    //                if (countPropertiesAttribute.CountPropertyNames.Length != rank)
    //                {
    //                    throw new SerializationException("The number of count properties does not match the rank of the nested generic collection.");
    //                }

    //                for (int i = 0; i < rank; i++)
    //                {
    //                    PropertyInfo countProperty = typeof(T).GetProperty(countPropertiesAttribute.CountPropertyNames[i]) ?? throw new SerializationException($"The property {typeof(T).FullName}.{countPropertiesAttribute.CountPropertyNames[i]} specified in the attribute {nameof(CountPropertiesAttribute)} assigned to {typeof(T).FullName}.{property.Name} does not exist.");

    //                    object countValue = countProperty.GetValue(obj) ?? throw new SerializationException($"The value of the property {typeof(T).FullName}.{countPropertiesAttribute.CountPropertyNames[i]} of the object of type {obj} could not be retrieved or is null.");

    //                    int count;

    //                    try
    //                    {
    //                        count = (int)Convert.ChangeType(countValue, typeof(int));
    //                    }
    //                    catch (Exception ex) when (ex is InvalidCastException or FormatException or OverflowException)
    //                    {
    //                        throw new SerializationException($"The value of the property {typeof(T).FullName}.{countPropertiesAttribute.CountPropertyNames[i]} of the object of type {obj} could not be converted to an integer.", ex);
    //                    }

    //                    lengths[i] = count;
    //                }
    //            }
    //            else
    //            {
    //                var indices = PropertiesWithIndices!.Single(tuple => tuple.property.DeclaringType == property.DeclaringType && tuple.property.Name == property.Name).countIndices;

    //                if (indices.Length != rank)
    //                {
    //                    throw new SerializationException("There is a bug.");
    //                }

    //                for (int i = 0; i < indices.Length; i++)
    //                {
    //                    lengths[i] = (int)(DeserializedPropertyValues![indices[i]] ?? throw new SerializationException("There is a bug."));
    //                }
    //            }

    //            // Note: We need to add the value before retrieving it, because retrieving it deserializes the sub-items and those would then be added before we add the list itself. Luckily we do not need the list for anything so we can just add a null value for it as a placeholder so the indices will be correct for accessing count properties.
    //            DeserializedPropertyValues!.Add(null);

    //            property.SetValue(obj, ParseAnything(inputTokens, property.PropertyType, lengths, formatProvider));
    //        }
    //        else
    //        {
    //            property.SetValue(obj, ParseAnything(inputTokens, property.PropertyType, Array.Empty<int>(), formatProvider));

    //            DeserializedPropertyValues!.Add(property.GetValue(obj));
    //        }
    //    }

    //    return obj;
    //}

    #endregion

    private static T ParseIInputDataObject<T>(IEnumerator<string> inputTokens, IFormatProvider? formatProvider) where T : IInputData, new()
    {
        T obj = new T();

        var properties = SerializationUtilities.GetSerializableProperties(typeof(T))
            .Where(property => property.GetCustomAttribute<IgnoreAttribute>() == null);

        var orderedProperties = SerializationUtilities.OrderPropertiesByPositionAttributeAndInheritanceLevelAndDeclarationOrder(properties);

        foreach (PropertyInfo property in orderedProperties)
        {
            if (property.GetCustomAttribute<CustomParseMethodAttribute>() is CustomParseMethodAttribute customParseMethodAttribute)
            {
                MethodInfo? parseMethod = obj.GetType().GetMethod(customParseMethodAttribute.MethodName);

                if (parseMethod == null)
                {
                    throw new SerializationException($"The specified custom parse method by the name {customParseMethodAttribute.MethodName} could not be found.");
                }

                var value = parseMethod.Invoke(obj, new object?[] { inputTokens, formatProvider });

                property.SetValue(obj, value);
            }
            else if (GetRank(property.PropertyType) > 0)
            {
                CountPropertiesAttribute? countPropertiesAttribute = property.GetCustomAttribute<CountPropertiesAttribute>();

                if (countPropertiesAttribute == null)
                {
                    throw new SerializationException($"A collection property requires a {nameof(CountPropertiesAttribute)} for de-/serialization.");
                }

                int rank = GetRank(property.PropertyType);

                if (countPropertiesAttribute.CountPropertyNames.Length != rank)
                {
                    throw new SerializationException("The number of count properties does not match the rank of the nested generic collection.");
                }

                int[] lengths = new int[rank];

                for (int i = 0; i < rank; i++)
                {
                    PropertyInfo countProperty = typeof(T).GetProperty(countPropertiesAttribute.CountPropertyNames[i]) ?? throw new SerializationException($"The property {typeof(T).FullName}.{countPropertiesAttribute.CountPropertyNames[i]} specified in the attribute {nameof(CountPropertiesAttribute)} assigned to {typeof(T).FullName}.{property.Name} does not exist.");

                    object countValue = countProperty.GetValue(obj) ?? throw new SerializationException($"The value of the property {typeof(T).FullName}.{countPropertiesAttribute.CountPropertyNames[i]} of the object of type {obj} could not be retrieved or is null.");

                    int count;

                    try
                    {
                        count = (int)Convert.ChangeType(countValue, typeof(int));
                    }
                    catch (Exception ex) when (ex is InvalidCastException or FormatException or OverflowException)
                    {
                        throw new SerializationException($"The value of the property {typeof(T).FullName}.{countPropertiesAttribute.CountPropertyNames[i]} of the object of type {obj} could not be converted to an integer.", ex);
                    }

                    lengths[i] = count;
                }

                property.SetValue(obj, ParseAnything(inputTokens, property.PropertyType, lengths, formatProvider));
            }
            else
            {
                property.SetValue(obj, ParseAnything(inputTokens, property.PropertyType, Array.Empty<int>(), formatProvider));
            }
        }

        return obj;
    }

    private static object? ParseAnything(IEnumerator<string> inputTokens, Type objectType, int[] lengths, IFormatProvider? formatProvider)
    {
        if (ImplementsIInputData(objectType) && HasParameterlessConstructor(objectType))
        {
            MethodInfo parseIInputDataObjectMethod = ParseIInputDataObjectMethod.MakeGenericMethod(objectType);
            return parseIInputDataObjectMethod.Invoke(null, new object?[] { inputTokens, formatProvider });
        }

        if (SerializationUtilities.IsArray(objectType))
        {
            return ParseArray(inputTokens, objectType, objectType.GetElementType()!, lengths, formatProvider);
        }

        if (SerializationUtilities.IsGenericCollection(objectType))
        {
            return ParseGenericCollection(inputTokens, objectType, GetElementTypeOfGenericCollectionType(objectType), lengths, formatProvider);
        }

        if (SerializationUtilities.IsKeyValuePair(objectType))
        {
            return ParseKeyValuePair(inputTokens, objectType, formatProvider);
        }

        return ParsePrimitiveTypeObject(inputTokens, objectType, formatProvider);
    }

    private static object? ParseArray(IEnumerator<string> inputTokens, Type arrayType, Type elementType, int[] lengths, IFormatProvider? formatProvider)
    {
        int[] arrayLengths = lengths[..arrayType.GetArrayRank()];
        int[] nestedLengths = lengths[arrayType.GetArrayRank()..];

        Array array = (Array)CreateArray(elementType, arrayLengths);

        int[] indices = new int[arrayType.GetArrayRank()];

        do
        {
            object? value = ParseAnything(inputTokens, elementType, nestedLengths, formatProvider);

            array.SetValue(value, indices);
        }
        while (moveToNextMultiDimensionalIndex());

        return array;


        bool moveToNextMultiDimensionalIndex()
        {
            if (indices.Select(index => index + 1).SequenceEqual(arrayLengths))
            {
                return false;
            }

            for (int dimension = arrayLengths.Length - 1; dimension >= 0; dimension--)
            {
                if (indices[dimension] + 1 == arrayLengths[dimension])
                {
                    indices[dimension] = 0;
                }
                else
                {
                    indices[dimension]++;
                    break;
                }
            }

            return true;
        }
    }

    private static object? ParseGenericCollection(IEnumerator<string> inputTokens, Type collectionType, Type elementType, int[] lengths, IFormatProvider? formatProvider)
    {
        object collection = CreateCollection(collectionType, lengths[0]);

        MethodInfo? addMethod = typeof(ICollection<>).MakeGenericType(elementType).GetMethod("Add");

        if (addMethod == null)
        {
            throw new SerializationException($"The ICollection<T>.Add method was not found for type {collection.GetType()}.");
        }

        object?[] parameters = new object?[1];

        for (int i = 0; i < lengths[0]; i++)
        {
            object? value = ParseAnything(inputTokens, elementType, lengths[1..], formatProvider);
            parameters[0] = value;
            _ = addMethod.Invoke(collection, parameters);
        }

        return collection;
    }

    private static object? ParseKeyValuePair(IEnumerator<string> inputTokens, Type objectType, IFormatProvider? formatProvider)
    {
        object? key = ParseAnything(inputTokens, objectType.GenericTypeArguments[0], Array.Empty<int>(), formatProvider);
        object? value = ParseAnything(inputTokens, objectType.GenericTypeArguments[1], Array.Empty<int>(), formatProvider);
        return Activator.CreateInstance(objectType, key, value) ?? throw new SerializationException(MethodBase.GetCurrentMethod()?.ToString());
    }

    private static object? ParsePrimitiveTypeObject(IEnumerator<string> inputTokens, Type primitiveType, IFormatProvider? formatProvider)
    {
        if (!inputTokens.MoveNext())
        {
            throw new SerializationException("There is not enough data to read.");
        }

        return Convert.ChangeType(inputTokens.Current, primitiveType, formatProvider);
    }


    private static object CreateArray(Type elementType, int[] lengths)
    {
        return Array.CreateInstance(elementType, lengths) ?? throw new SerializationException(MethodBase.GetCurrentMethod()?.ToString());
    }

    private static object CreateCollection(Type collectionType, int capacity)
    {
        if (HasCapacityConstructor(collectionType))
        {
            return Activator.CreateInstance(collectionType, capacity) ?? throw new SerializationException(MethodBase.GetCurrentMethod()?.ToString());
        }
        else
        {
            return Activator.CreateInstance(collectionType) ?? throw new SerializationException($"{MethodBase.GetCurrentMethod()?.ToString()} #2");
        }
    }


    private static Type GetElementTypeOfGenericCollectionType(Type collectionType)
    {
        return collectionType.GetInterfaces()
            .Where(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(ICollection<>))
            .Select(i => i.GenericTypeArguments)
            .First()
            .Single();
    }

    private static int GetRank(Type type)
    {
        if (SerializationUtilities.IsArray(type))
        {
            return type.GetArrayRank() + GetRank(type.GetElementType() ?? throw new SerializationException($"{type} was recognized as an array but it does not have an element type."));
        }

        if (SerializationUtilities.IsCollection(type))
        {
            return 1 + (type.IsGenericType ? GetRank(GetElementTypeOfGenericCollectionType(type)) : 0);
        }

        if (SerializationUtilities.IsDictionary(type))
        {
            // TODO Get Type Arguments of IDictionary<,> interface instead of object type as it is done for ICollection<> above
            return 1 + GetRank(type.GenericTypeArguments[0]) + GetRank(type.GenericTypeArguments[1]);
        }

        return 0;
    }


    private static bool HasParameterlessConstructor(Type type)
    {
        // IsValueType = a struct
        return type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null;
    }

    private static bool HasCapacityConstructor(Type type)
    {
        return type
            .GetConstructor(BindingFlags.Public | BindingFlags.Instance, new[] { typeof(int) })?
            .GetParameters()
            .Single()
            .Name?
            .Equals("capacity", StringComparison.InvariantCultureIgnoreCase)
            ?? false;
    }

    private static bool ImplementsIInputData(Type type)
    {
        return type.IsAssignableTo(typeof(IInputData));
    }


}
