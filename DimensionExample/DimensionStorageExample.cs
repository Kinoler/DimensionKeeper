using System.Collections.Generic;
using DimensionKeeper.DimensionService;
using DimensionKeeper.Helpers;
using Microsoft.Xna.Framework;
using Terraria;

namespace DimensionKeeper.DimensionExample
{
    public class DimensionStorageExample : DimensionStorage<Dimension>
    {
        internal static List<Dimension> Dimensions { get; set; }
        internal static CycledCounter Counter { get; set; }
        internal static int CurrentLoadedDimension { get; set; }

        internal static void Initialize()
        {
            Dimensions = new List<Dimension>();
            Counter = new CycledCounter();
            CurrentLoadedDimension = -1;
        }

        internal static void Clear()
        {
            Dimensions = null;
            Counter = null;
        }

        public static void AddDimension(Dimension dimension)
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

        public static void UpdateDimension(int num, Dimension dimension)
        {
            Dimensions[num] = dimension;
        }

        public override Dimension Load()
        {
            if (Counter.Max == 0)
                return null;

            CurrentLoadedDimension = Counter.Current;
            return Dimensions[Counter.Current];
        }
        
        public override void Save(Dimension dimension)
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
    }
}
