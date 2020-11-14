using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TestMod.DimensionLogic;
using TestMod.Interfaces;

namespace TestMod.DimensionExample.Phases
{
    public class ChestPhase: DimensionPhases<DimensionExample>
    {
        public override void ExecuteLoadPhase(DimensionExample dimension)
        {
            for (var i = 0; i < dimension.Chests.Length; i++)
            {
                var chest = dimension.Chests[i];
                var chestIndex = WorldGen.PlaceChest(chest.x, chest.y + 1);
                if (chestIndex == -1)
                {
                    TileObject.CanPlace(chest.x, chest.y, (int)21, 0, 1, out var objectData, false, false);
                    TileObject.Place(objectData);
                    chestIndex = Chest.CreateChest(chest.x, chest.y, -1);
                }

                Main.chest[chestIndex] = chest;
            }

            Recipe.FindRecipes();
            var locationPoint = dimension.Location;
            var updateExtended = 3;
            for (var x = locationPoint.X - updateExtended; x < locationPoint.X + dimension.Width + updateExtended; x++)
            {
                for (var y = 0; y < locationPoint.Y + dimension.Height + updateExtended; y++)
                {
                    WorldGen.SquareTileFrame(x, y, false); // Need to do this after stamp so neighbors are correct.
                }
            }
        }

        public override void ExecuteSavePhase(DimensionExample dimension)
        {
            var chests = new List<Chest>();
            //return;
            for (var index = 0; index < Main.chest.Length; ++index)
            {
                if (Main.chest[index] != null &&
                    Main.chest[index].x >= dimension.Location.X &&
                    Main.chest[index].x <= dimension.Location.X + dimension.Width &&
                    Main.chest[index].y >= dimension.Location.Y &&
                    Main.chest[index].y <= dimension.Location.Y + dimension.Height)
                    chests.Add(Main.chest[index]);
            }

            dimension.Chests = chests.ToArray();
        }

        public override void ExecuteClearPhase(DimensionExample dimension)
        {
            for (var index = 0; index < Main.chest.Length; index++)
            {
                var chest = Main.chest[index];
                if (chest != null &&
                    chest.x >= dimension.Location.X &&
                    chest.x <= dimension.Location.X + dimension.Width &&
                    chest.y >= dimension.Location.Y &&
                    chest.y <= dimension.Location.Y + dimension.Height)
                {
                    Main.chest[index] = (Chest)null;
                    if (Main.player[Main.myPlayer].chest == index)
                        Main.player[Main.myPlayer].chest = -1;
                }
            }

            Recipe.FindRecipes();
        }
    }
}
