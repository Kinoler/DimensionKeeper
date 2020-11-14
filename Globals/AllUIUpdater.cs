using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TestMod.UI;

namespace TestMod.Globals
{
    public class AllUIUpdater : GlobalItem
    {
        public static PaintTooltipUI PaintTooltipUI;

        public void Initialize()
        {
            if (!Main.dedServ)
            {
                PaintTooltipUI = new PaintTooltipUI {Visible = false};
                PaintTooltipUI.Hide();
            }
		}
        
        public void DrawUpdateAll(SpriteBatch spriteBatch)
		{
            PaintTooltipUI.Update();
            PaintTooltipUI.Draw(spriteBatch);

            spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.UIScaleMatrix);
        }

		public void DrawUpdatePaintTools(SpriteBatch spriteBatch)
		{
            PaintTooltipUI.UpdateGameScale();
            PaintTooltipUI.DrawGameScale(spriteBatch);
		}
    }
}
