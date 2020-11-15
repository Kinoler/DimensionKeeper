using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TestMod.Helpers
{
    public static class CloneExtensions
    {
        public static Chest CloneObject(this Chest chestToClone)
        {
            var chest = (Chest)chestToClone.Clone();
            chest.item = new Item[chest.item.Length];
            for (var i = 0; i < chestToClone.item.Length; i++)
            {
                if (chestToClone.item[i].type > 0)
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
