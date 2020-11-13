using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace TestMod.LevelSystem.UI
{
    public class LevelUIElement : UIElement
    {
        public MyPlayer Player => Main.LocalPlayer.GetModPlayer<MyPlayer>();
        public int CurrentLevel => Player.Leveling.Level;
        public int CurrentExp => Player.Leveling.Experience;
        public int NextExp => Player.Leveling.ExperienceToLevel();

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var innerDimensions = GetInnerDimensions();

            var shopx = innerDimensions.X;
            var shopy = innerDimensions.Y;

            Utils.DrawBorderStringFourWay(
                spriteBatch,
                Main.fontMouseText,
                $"{CurrentLevel} lvl",
                shopx,
                shopy,
                Color.White,
                Color.Black,
                new Vector2(0.3f));

            Utils.DrawBorderStringFourWay(
                spriteBatch,
                Main.fontMouseText,
                $"{CurrentExp}/{NextExp}",
                shopx,
                shopy + 25f,
                Color.White,
                Color.Black,
                new Vector2(0.3f));
        }
    }

}
