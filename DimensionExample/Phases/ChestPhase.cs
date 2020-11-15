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
                var chestIndex = Chest.CreateChest(chest.x, chest.y, -1);
                Main.chest[chestIndex] = chest;
                WorldGen.SquareTileFrame(chest.x, chest.y);
            }

            Recipe.FindRecipes();
            return;
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
                chestIndex = Chest.CreateChest(chest.x, chest.y, -1);
                Main.chest[chestIndex] = chest;
            }

            Recipe.FindRecipes();
        }

        public override void ExecuteSynchronizePhase(DimensionExample dimension)
        {
            var chests = new List<Chest>();

            //return;
            for (var index = 0; index < Main.chest.Length; ++index)
            {
                if (Main.chest[index] != null &&
                    Main.chest[index].x >= dimension.LocationToLoad.X &&
                    Main.chest[index].x <= dimension.LocationToLoad.X + dimension.Width &&
                    Main.chest[index].y >= dimension.LocationToLoad.Y &&
                    Main.chest[index].y <= dimension.LocationToLoad.Y + dimension.Height)
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
                    chest.x >= dimension.LocationToLoad.X &&
                    chest.x <= dimension.LocationToLoad.X + dimension.Width &&
                    chest.y >= dimension.LocationToLoad.Y &&
                    chest.y <= dimension.LocationToLoad.Y + dimension.Height)
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
