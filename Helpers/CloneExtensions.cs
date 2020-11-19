using Terraria;
using Terraria.ID;

namespace DimensionKeeper.Helpers
{
    public static class CloneExtensions
    {
        public static Chest CloneObject(this Chest chestToClone)
        {
            var chest = (Chest)chestToClone.Clone();
            chest.item = new Item[chest.item.Length];
            for (var i = 0; i < chestToClone.item.Length; i++)
            {
                if (chestToClone.item[i].type > ItemID.None)
                {
                    chest.item[i] = chestToClone.item[i].Clone();
                }
                else
                {
                    chest.item[i] = new Item();
                }
            }

            return chest;
        }
    }
}
