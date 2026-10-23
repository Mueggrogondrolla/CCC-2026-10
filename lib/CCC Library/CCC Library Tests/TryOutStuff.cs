using CCCLibrary.Serialization;
using CCCLibrary.Serialization.Attributes;
using CCCLibrary.Serialization.Interfaces;
using System.Drawing;
using System.Globalization;

namespace CCCLibraryTests;

public class TryOutStuff
{
    private class CCC_2018_Lvl3_Input : IInputData
    {
        public int ImageCount { get; init; }

        public int TestCount { get; } = 2;



        public Image InputData { get; init; }



        // Disabled: Does not work with numbers as input
        //public bool Primitive { get; init; }

        public char Primitive1 { get; init; }

        public sbyte Primitive2 { get; init; }

        public byte Primitiv3 { get; init; }

        public short Primitive4 { get; init; }

        public ushort Primitive5 { get; init; }

        public int Primitive6 { get; init; }

        public uint Primitive7 { get; init; }

        public long Primitive8 { get; init; }

        public ulong Primitive9 { get; init; }

        public float Primitive10 { get; init; }

        public double Primitive11 { get; init; }

        public decimal Primitive12 { get; init; }

        // Disabled: Does not work with numbers as input
        //public DateTime Primitive13 { get; init; }

        public string Primitive14 { get; init; }


        [CountProperties(nameof(TestCount))]
        public System.Collections.Generic.List<int> GenericCollection { get; init; }

        [CountProperties(nameof(TestCount))]
        public System.Collections.Generic.LinkedList<int> GenericCollection2 { get; init; }

        [CountProperties(nameof(TestCount))]
        public System.Collections.Generic.HashSet<int> GenericCollection3 { get; init; }

        [CountProperties(nameof(TestCount))]
        public System.Collections.Generic.Dictionary<int, int> GenericCollection4 { get; init; }

        // Disabled: More than one generic parameter does not work yet
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Generic.SortedList<int, int> GenericCollection5 { get; init; }

        [CountProperties(nameof(TestCount))]
        public System.Collections.Generic.SortedSet<int> GenericCollection6 { get; init; }

        // Disabled: More than one generic parameter does not work yet
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Generic.SortedDictionary<int, int> GenericCollection7 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Generic.Stack<int> GenericCollection8 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Generic.Queue<int> GenericCollection9 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Generic.PriorityQueue<int, int> GenericCollection10 { get; init; }



        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.BitArray Collection { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.ArrayList Collection2 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Hashtable Collection3 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.SortedList Collection4 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Stack Collection5 { get; init; }

        // Disabled: Does not implement ICollection<> so it does not work yet (requires custom deserialization)
        //[CountProperties(nameof(TestCount))]
        //public System.Collections.Queue Collection6 { get; init; }



        [CountProperties(nameof(TestCount))]
        public List<int> Images { get; init; }

        [CountProperties(nameof(TestCount))]
        public int[] Images2 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount))]
        public int[,] Images3 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,,] Images4 { get; init; }


        [CountProperties(nameof(TestCount), nameof(TestCount))]
        public List<List<int>> Images5 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount))]
        public List<int[]> Images6 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<int[,]> Images7 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<int[,,]> Images8 { get; init; }


        [CountProperties(nameof(TestCount), nameof(TestCount))]
        public List<int>[] Images9 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount))]
        public int[][] Images10 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,][] Images11 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,,][] Images12 { get; init; }


        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<int>[,] Images13 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[][,] Images14 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,][,] Images15 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,,][,] Images16 { get; init; }


        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<int>[,,] Images17 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[][,,] Images18 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,][,,] Images19 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public int[,,][,,] Images20 { get; init; }


        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<int[][,,]> Images21 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<List<int>[]> Images22 { get; init; }

        [CountProperties(nameof(TestCount), nameof(TestCount), nameof(TestCount))]
        public List<int[]>[] Images23 { get; init; }

        public int MoveAhead { get; } = 200;

        [CountProperties(nameof(MoveAhead))]
        public int[] TryMoveDictionaryToUniqueKeysWithThisDeserialization { get; init; }

        [CountProperties(nameof(TestCount))]
        public Dictionary<object, string> Other { get; init; }



        public CCC_2018_Lvl3_Input()
        {
            //Images = new List<List<int>>[0,0];
        }

        public class Image : IInputData
        {
            public int Timestamp { get; set; }

            public int RowCount { get; set; }

            public int ColCount { get; set; }

            public int Value { get; set; }
        }
    }



    [Fact]
    public void Main()
    {
        InputLexer lexer = new InputLexer(new[] { ' ', '\n', '\r', '\t' });

        var filepath = @"E:\Dateien\_Freizeit (Unterhaltung)\Projekte\Programmieren\_CodingContest\Coding Contest\CCC 2018-04 Asteroids\lvl1-2.inp";
        var fileContents = File.ReadAllText(filepath);
        var tokens = lexer.GetTokens(fileContents);
        var input = InputParser.Parse<CCC_2018_Lvl3_Input>(tokens, new SerializationOptions(formatProvider: CultureInfo.InvariantCulture, throwWhenDataRemainsAfterDeserialization: false));

        //var x = InputParser.Parse<X>(tokens, new SerializationOptions(formatProvider: CultureInfo.InvariantCulture, throwWhenDataRemainsAfterDeserialization: false));
    }

    // Test CustomParseMethod
    class X : IInputData
    {
        [CustomParseMethod(nameof(ParsePoint))]
        public Point Date { get; init; }


        public object? ParsePoint(IEnumerator<string> tokens, IFormatProvider? formatProvider)
        {
            tokens.MoveNext();
            var x = int.Parse(tokens.Current, formatProvider);
            tokens.MoveNext();
            var y = int.Parse(tokens.Current, formatProvider);
            return new Point(x, y);
        }
    }
}
