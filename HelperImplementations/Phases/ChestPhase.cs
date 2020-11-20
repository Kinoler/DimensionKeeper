using System.Collections.Generic;
using DimensionKeeper.DimensionService;
using DimensionKeeper.Helpers;
using Terraria;

namespace DimensionKeeper.HelperImplementations.Phases
{
    public class ChestPhase: DimensionPhase<Dimension>
    {
        public override void ExecuteLoadPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;
            var dimension = entity.Dimension;

            for (var i = 0; i < dimension.Chests.Length; i++)
            {
                var chest = dimension.Chests[i].CloneObject();
                chest.x += locationToLoad.X;
                chest.y += locationToLoad.Y;

                var chestIndex = Chest.CreateChest(chest.x, chest.y, -1);
                Main.chest[chestIndex] = chest;
            }

            Recipe.FindRecipes();
        }

        public override void ExecuteSynchronizePhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;
            var chests = new List<Chest>();

            for (var index = 0; index < Main.chest.Length; ++index)
            {
                if (Main.chest[index] != null &&
                    Main.chest[index].x >= locationToLoad.X &&
                    Main.chest[index].x <= locationToLoad.X + entity.Width &&
                    Main.chest[index].y >= locationToLoad.Y &&
                    Main.chest[index].y <= locationToLoad.Y + entity.Height)
                {
                    var chest = Main.chest[index].CloneObject();

                    chest.x -= locationToLoad.X;
                    chest.y -= locationToLoad.Y;

                    chests.Add(chest);
                }
            }

            entity.Dimension.Chests = chests.ToArray();
        }

        public override void ExecuteClearPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;

            for (var index = 0; index < Main.chest.Length; index++)
            {
                var chest = Main.chest[index];
                if (chest != null &&
                    chest.x >= locationToLoad.X &&
                    chest.x <= locationToLoad.X + entity.Width &&
                    chest.y >= locationToLoad.Y &&
                    chest.y <= locationToLoad.Y + entity.Height)
                {
                    Main.chest[index] = (Chest)null;
                    if (Main.player[Main.myPlayer].chest == index)
                        Main.player[Main.myPlayer].chest = -1;
                }
            }
        }
    }
}
