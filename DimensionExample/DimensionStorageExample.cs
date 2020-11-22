using System.Collections.Generic;
using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.Extensions;
using Microsoft.Xna.Framework;
using Terraria;

namespace DimensionKeeper.DimensionExample
{
    public class DimensionStorageExample
    {
        internal static CycledCounter Counter { get; set; }

        internal static void Initialize()
        {
            Counter = new CycledCounter();
            Counter.AddNew();
            Counter.AddNew();
        }

        public static int NextDimension()
        {
            var current = Counter.Next();
            Main.NewText($"Current dimension {current + 1}/{Counter.Max}", Color.Azure, false);
            return current;
        }

        public static void Load(Point location)
        {
            if (Counter.Max == 0)
                return;
            Counter.AddNew();

            SingleEntryFactory.GetEntry("SomeEntry", location)
                .LoadDimensionNet(DimensionRegisterExample.ExampleName, Counter.Current.ToString());
        }
    }
}
