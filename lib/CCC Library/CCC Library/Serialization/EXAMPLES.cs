using CCCLibrary.Serialization.Attributes;

namespace CCCLibrary.Serialization;

// TODO Need to access the count properties somehow without the attributes
// Implicit (ordered properties)
class PersonImplicitlyDeserialized
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Address { get; init; }

    public DateTime Birthday { get; init; }

    public string CityOfBirth { get; init; }

    public PersonImplicitlyDeserialized Partner { get; init; }

    public int ChildrenCount { get; init; }

    public List<PersonImplicitlyDeserialized> Children { get; init; }

    public int EmailAddressesCount { get; init; }

    public List<string> EmailAddresses { get; init; }
}

record PersonImplicitlyDeserializedRecord(string FirstName, string LastName, string Address, DateTime Birthday, string CityOfBirth, PersonImplicitlyDeserialized Partner, int ChildrenCount, List<PersonImplicitlyDeserialized> Children, int EmailAddressesCount, List<string> EmailAddresses);

[SerializationOrder(
    nameof(FirstName),
    nameof(LastName),
    nameof(Address),
    nameof(Birthday),
    nameof(CityOfBirth),
    nameof(Partner),
    nameof(ChildrenCount),
    nameof(Children),
    nameof(EmailAddressesCount),
    nameof(EmailAddresses)
)]
record PersonFullyDeclaredUsingClassAttributeDeserializationRecord(string FirstName, string LastName, string Address, DateTime Birthday, string CityOfBirth, PersonImplicitlyDeserialized Partner, int ChildrenCount, List<PersonImplicitlyDeserialized> Children, int EmailAddressesCount, List<string> EmailAddresses);

record PersonFullyDeclaredUsingPropertyAttributesDeserializationRecord(
    [property: Position(0)] string FirstName,
    [property: Position(1)] string LastName,
    [property: Position(2)] string Address,
    [property: Position(3)] DateTime Birthday,
    [property: Position(4)] string CityOfBirth,
    [property: Position(5)] PersonImplicitlyDeserialized Partner,
    [property: Position(6)] int ChildrenCount,
    [property: Position(7)] List<PersonImplicitlyDeserialized> Children,
    [property: Position(8)] int EmailAddressesCount,
    [property: Position(9)] List<string> EmailAddresses);


[SerializationOrder(
    nameof(FirstName),
    nameof(LastName),
    nameof(Address),
    nameof(Birthday),
    nameof(CityOfBirth),
    nameof(Partner),
    nameof(ChildrenCount),
    nameof(Children),
    nameof(EmailAddressesCount),
    nameof(EmailAddresses)
)]
class PersonFullyDeclaredUsingClassPropertyDeserialization
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Address { get; init; }

    public DateTime Birthday { get; init; }

    public string CityOfBirth { get; init; }

    public PersonImplicitlyDeserialized Partner { get; init; }

    public int ChildrenCount { get; init; }

    public List<PersonImplicitlyDeserialized> Children { get; init; }

    public int EmailAddressesCount { get; init; }

    public List<string> EmailAddresses { get; init; }
}


// Fully declared
class PersonFullyDeclaredDeserialization
{
    [Position(2)]
    public string Address { get; init; }

    [Position(3)]
    public DateTime Birthday { get; init; }

    [Position(7)]
    public List<PersonFullyDeclaredDeserialization> Children { get; init; }

    [Position(6)]
    public int ChildrenCount { get; init; }

    [Position(4)]
    public string CityOfBirth { get; init; }

    [Position(9)]
    public List<string> EmailAddresses { get; init; }

    [Position(8)]
    public int EmailAddressesCount { get; init; }

    [Position(0)]
    public string FirstName { get; init; }

    [Position(1)]
    public string LastName { get; init; }

    [Position(5)]
    public PersonFullyDeclaredDeserialization Partner { get; init; }
}


// Partially declared - Filling the free positions
class PersonPartiallyDeclaredFillingTheFreePositionsDeserialization
{
    public string Address { get; init; }

    public DateTime Birthday { get; init; }

    public List<PersonPartiallyDeclaredFillingTheFreePositionsDeserialization> Children { get; init; }

    [Position(6)]
    public int ChildrenCount { get; init; }

    [Position(4)]
    public string CityOfBirth { get; init; }

    public List<string> EmailAddresses { get; init; }

    [Position(8)]
    public int EmailAddressesCount { get; init; }

    [Position(0)]
    public string FirstName { get; init; }

    [Position(1)]
    public string LastName { get; init; }

    [Position(5)]
    public PersonPartiallyDeclaredFillingTheFreePositionsDeserialization Partner { get; init; }
}


// Partially declared - Positional first
class PersonPartiallyDeclaredPositionalsFirstDeserialization
{
    [Position(2)]
    public string Address { get; init; }

    [Position(3)]
    public DateTime Birthday { get; init; }

    [Position(7)]
    public List<PersonPartiallyDeclaredPositionalsFirstDeserialization> Children { get; init; }

    [Position(6)]
    public int ChildrenCount { get; init; }

    [Position(4)]
    public string CityOfBirth { get; init; }

    public List<string> EmailAddresses { get; init; }

    [Position(8)]
    public int EmailAddressesCount { get; init; }

    [Position(0)]
    public string FirstName { get; init; }

    [Position(1)]
    public string LastName { get; init; }

    [Position(5)]
    public PersonPartiallyDeclaredPositionalsFirstDeserialization Partner { get; init; }
}
