using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic;
using TestMod.Helpers;

namespace TestMod.DimensionExample
{
    public class DataParserExample : DataParser<DimensionExample>
    {
        internal static List<DimensionExample> Dimensions { get; } = new List<DimensionExample>();
        internal static CycledCounter Counter { get; } = new CycledCounter();
        internal static int CurrentLoadedDimension { get; set; } = -1;

        public override bool AlwaysNew => true;

        public static void AddDimension(DimensionExample dimension)
        {
            Dimensions.Add(dimension);
            Counter.AddNew();
            NextDimension();
        }

        public static int NextDimension()
        {
            var current = Counter.Next();
            Main.NewText($"Current dimension {current + 1}/{Counter.Max}", Color.Azure, false);
            return current;
        }

        public override DimensionExample Load()
        {
            if (Counter.Max == 0)
                return null;

            CurrentLoadedDimension = Counter.Current;
            return Dimensions[Counter.Current];
        }
        
        public override void Save(DimensionExample dimension)
        {
            if (Counter.Max == 0)
            {
                AddDimension(dimension);
                return;
            }


            if (CurrentLoadedDimension == -1)
                return;

            UpdateDimension(CurrentLoadedDimension, dimension);
        }

        public static void UpdateDimension(int num, DimensionExample dimension)
        {
            Dimensions[num] = dimension;
        }
    }
}
