using CCCLibrary.Serialization.Attributes;
using CCCLibrary.Serialization.Interfaces;

namespace CCCLibraryTests.Serialization.SerializationUtilities;

public class OrderPropertiesByPositionAttributeAndDeclarationOrder_Tests
{
    [Fact]
    public void Test_ParsingWhenUsingThePositionAttributeInSubClasses_ToParseThosePropertiesBeforeTheBaseClassesProperties()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2020-10 Smartgrid\_Resources";

        var inputClasses = new[] {
            typeof(CCC_2020_10_Level_1_Input),
            typeof(CCC_2020_10_Level_2_Input),
            typeof(CCC_2020_10_Level_3_Input),
            typeof(CCC_2020_10_Level_4_Input),
            typeof(CCC_2020_10_Level_5_Input),
            typeof(CCC_2020_10_Level_6_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, inputClasses);
    }


    // Level Input Classes

    private class CCC_2020_10_Level_1_Input : IInputData
    {
        public int PriceCount { get; init; }
        [CountProperties(nameof(PriceCount))]
        public List<int> Prices { get; init; }
    }

    private sealed class CCC_2020_10_Level_2_Input : CCC_2020_10_Level_1_Input
    {
        public int TaskCount { get; init; }
        [CountProperties(nameof(TaskCount))]
        public List<Task> Tasks { get; init; }
        public sealed class Task : IInputData
        {
            public int TaskId { get; init; }
            public int CompletionTime { get; init; }
        }
    }

    private class CCC_2020_10_Level_3_Input : CCC_2020_10_Level_1_Input
    {
        public int TaskCount { get; init; }
        [CountProperties(nameof(TaskCount))]
        public List<Task> Tasks { get; init; }
        public sealed class Task : IInputData
        {
            public int TaskId { get; init; }
            public int Power { get; init; }
            public int StartInterval { get; init; }
            public int EndInterval { get; init; }
        }
    }

    private class CCC_2020_10_Level_4_Input : CCC_2020_10_Level_3_Input
    {
        [Position(0)]
        public int MaxPower { get; init; }
        [Position(1)]
        public long MaxElectricityBill { get; init; }
    }

    private class CCC_2020_10_Level_5_Input : CCC_2020_10_Level_4_Input
    {
        [Position(2)]
        public int MaxConcurrentTasks { get; init; }
    }

    private sealed class CCC_2020_10_Level_6_Input : CCC_2020_10_Level_5_Input { }
}
