using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TestMod.UI;
using static Terraria.ModLoader.ModContent;

namespace TestMod.LevelSystem.UI
{
    public class LevelUIState : UIState
	{
        public static bool Visible;

		public DragableUIPanel LevelPanel;
		public LevelUIElement LevelDisplay;

		public override void OnInitialize()
		{
			LevelPanel = new DragableUIPanel();
			LevelPanel.SetPadding(0);
			LevelPanel.Left.Set(400f, 0f);
			LevelPanel.Top.Set(100f, 0f);
			LevelPanel.Width.Set(170f, 0f);
			LevelPanel.Height.Set(70f, 0f);
			LevelPanel.BackgroundColor = new Color(73, 94, 171);

			LevelDisplay = new LevelUIElement();
			LevelDisplay.Left.Set(15, 0f);
			LevelDisplay.Top.Set(20, 0f);
			LevelDisplay.Width.Set(100f, 0f);
			LevelDisplay.Height.Set(0, 1f);
			LevelPanel.Append(LevelDisplay);

			Append(LevelPanel);
        }
    }
}
