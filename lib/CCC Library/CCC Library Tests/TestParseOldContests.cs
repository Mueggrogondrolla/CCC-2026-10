using CCCLibrary.Serialization.Attributes;
using CCCLibrary.Serialization.Interfaces;

namespace CCCLibraryTests;

public class TestParseOldContests
{
    private class CCC_2018_04_Level_1_Input : IInputData
    {
        public int ImageCount { get; init; }

        [CountProperties(nameof(ImageCount))]
        public List<Image> Images { get; init; }

        public sealed class Image : IInputData
        {
            public int TimeStamp { get; init; }

            public int RowCount { get; init; }

            public int ColCount { get; init; }

            #region Goal

            ////[CountProperties(nameof(RowCount))]
            //public List<Row> Rows { get; init; }

            //public sealed class Row : IInputData
            //{
            //    //[CountProperties(nameof(ColCount))]
            //    public List<int> Values { get; init; }
            //}

            #endregion

            #region Workaround

            [CountProperties(nameof(RowCount), nameof(ColCount))]
            public int[,] Values { get; init; }

            #endregion
        }
    }

    private sealed class CCC_2018_04_Level_2_Input : CCC_2018_04_Level_1_Input { }

    private sealed class CCC_2018_04_Level_3_Input : CCC_2018_04_Level_1_Input { }



    private class CCC_2019_11_Level_1_Input : IInputData
    {
        public int Count { get; init; }
        public List<int> Altitudes { get; init; }
    }

    private sealed class CCC_2019_11_Level_2_Input : CCC_2019_11_Level_1_Input { }

    private class CCC_2019_11_Level_3_Input : IInputData
    {
        public int Rows { get; init; }
        public int Columns { get; set; }
        public int[,] Altitudes { get; init; }
    }

    private sealed class CCC_2019_11_Level_4_Input : CCC_2019_11_Level_3_Input { }

    private sealed class CCC_2019_11_Level_5_Input : CCC_2019_11_Level_3_Input
    {
        [Position(2)]
        public int Budget { get; init; }
    }



    private sealed class CCC_2020_04_Level_1_Input : IInputData
    {
        public int FlightCount { get; init; }
        public List<Flight> Flights { get; init; }
        public class Flight : IInputData
        {
            public int Timestamp { get; init; }
            public float Latitude { get; init; }
            public float Longitude { get; init; }
            public float Altitude { get; init; }
        }
    }

    private sealed class CCC_2020_04_Level_2_Input : IInputData
    {
        public int FlightCount { get; init; }
        public List<Flight> Flights { get; init; }
        public sealed class Flight : CCC_2020_04_Level_1_Input.Flight
        {
            public string Start { get; init; }
            public string Destination { get; init; }
            public int Takeoff { get; init; }
        }
    }

    private sealed class CCC_2020_04_Level_3_Input : IInputData
    {
        public int CoordinateCount { get; init; }
        public List<Coordinate> Coordinates { get; init; }
        public sealed class Coordinate : IInputData
        {
            public float Latitude { get; init; }
            public float Longitude { get; init; }
            public float Altitude { get; init; }
        }
    }

    private sealed class CCC_2020_04_Level_4_Input : IInputData
    {
        public int QueriesCount { get; init; }
        public List<Query> Queries { get; init; }
        public sealed class Query : IInputData
        {
            public int FlightId { get; init; }
            public int Timestamp { get; init; }
        }
    }

    private sealed class CCC_2020_04_Level_4_FlightFile_Input : IInputData
    {
        public string OriginAirport { get; init; }
        public string DestinationAirport { get; init; }
        public int TakeoffTimestamp { get; init; }
        public int CoordinateCount { get; init; }
        public List<Coordinate> Coordinates { get; init; }
        public sealed class Coordinate
        {
            public int TimestampOffset { get; init; }
            public float Latitude { get; init; }
            public float Longitude { get; init; }
            public float Altitude { get; init; }
        }
    }

    private sealed class CCC_2020_04_Level_5_Input : IInputData
    {
        public float TransferRange { get; init; }
        public int FlightIdCount { get; init; }
        public List<int> FlightIds { get; init; }
    }



    private class CCC_2020_10_Level_1_Input : IInputData
    {
        public int PriceCount { get; init; }
        public List<int> Prices { get; init; }
    }

    private sealed class CCC_2020_10_Level_2_Input : CCC_2020_10_Level_1_Input
    {
        public int TaskCount { get; init; }
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



    private class CCC_2021_11_Level_1_Input : IInputData
    {
        public int LineCount { get; init; }
        public List<string> LinesOfCode { get; init; }
    }

    private sealed class CCC_2021_11_Level_2_Input : CCC_2021_11_Level_1_Input { }

    private sealed class CCC_2021_11_Level_3_Input : CCC_2021_11_Level_1_Input { }

    private sealed class CCC_2021_11_Level_4_Input : CCC_2021_11_Level_1_Input { }



    private class CCC_2022_11_Level_1_Input : IInputData
    {
        public int SideLength { get; init; }
        public List<string> BoardRows { get; set; }
    }

    private class CCC_2022_11_Level_2_Input : CCC_2022_11_Level_1_Input
    {
        public int PacmanRow { get; init; }
        public int PacmanColumn { get; init; }
        public int SequenceLength { get; init; }
        public string MovementSequence { get; init; }
    }

    private sealed class CCC_2022_11_Level_3_Input : CCC_2022_11_Level_2_Input
    {
        public int GhostCount { get; init; }
        public List<Ghost> Ghosts { get; init; }
        public sealed class Ghost : IInputData
        {
            public int GhostRow { get; init; }
            public int GhostColumn { get; init; }
            public int SequenceLength { get; init; }
            public string MovementSequence { get; init; }
        }
    }

    private sealed class CCC_2022_11_Level_4_Input : CCC_2022_11_Level_1_Input
    {
        public int PacmanRow { get; init; }
        public int PacmanColumn { get; init; }
        public int MaxMovementSequenceLength { get; init; }
    }




    // TODO Make it possible to pass count properties to subclasses
    [Fact]
    public void Parse_CCC_2018_04_Inputs()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2018-04 Asteroids";

        var types = new[] {
            typeof(CCC_2018_04_Level_1_Input),
            typeof(CCC_2018_04_Level_2_Input),
            typeof(CCC_2018_04_Level_3_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, types, "lvl*.inp");
    }

    [Fact]
    public void Parse_CCC_2019_11_Inputs()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2019-11 Solar City\_Resources";

        var types = new[] {
            typeof(CCC_2019_11_Level_1_Input),
            typeof(CCC_2019_11_Level_2_Input),
            typeof(CCC_2019_11_Level_3_Input),
            typeof(CCC_2019_11_Level_4_Input),
            typeof(CCC_2019_11_Level_5_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, types);
    }

    [Fact]
    public void Parse_CCC_2020_04_Inputs()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2020-04 Quantum Breakdown\_Resources";

        var types = new[] {
            typeof(CCC_2020_04_Level_1_Input),
            typeof(CCC_2020_04_Level_2_Input),
            typeof(CCC_2020_04_Level_3_Input),
            typeof(CCC_2020_04_Level_4_Input),
            typeof(CCC_2020_04_Level_5_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, types);
    }

    [Fact]
    public void Parse_CCC_2020_10_Inputs()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2020-10 Smartgrid\_Resources";

        var types = new[] {
            typeof(CCC_2020_10_Level_1_Input),
            typeof(CCC_2020_10_Level_2_Input),
            typeof(CCC_2020_10_Level_3_Input),
            typeof(CCC_2020_10_Level_4_Input),
            typeof(CCC_2020_10_Level_5_Input),
            typeof(CCC_2020_10_Level_6_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, types);
    }

    [Fact]
    public void Parse_CCC_2021_11_Inputs()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2021-11 Compiler Breakout\CCC 2021-11 Compiler Breakout\_Resources";

        var types = new[] {
            typeof(CCC_2021_11_Level_1_Input),
            typeof(CCC_2021_11_Level_2_Input),
            typeof(CCC_2021_11_Level_3_Input),
            typeof(CCC_2021_11_Level_4_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, types);
    }

    [Fact]
    public void Parse_CCC_2022_11_Inputs()
    {
        var workingDirectory = @"..\\..\\..\\..\CCC 2022-11 Pacman\_Resources";

        var types = new[] {
            typeof(CCC_2022_11_Level_1_Input),
            typeof(CCC_2022_11_Level_2_Input),
            typeof(CCC_2022_11_Level_3_Input),
            typeof(CCC_2022_11_Level_4_Input),
        };

        TestUtilities.ParseInputFiles(workingDirectory, types);
    }
}