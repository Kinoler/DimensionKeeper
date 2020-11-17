using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameContent.Liquid;
using Terraria.ID;
using Terraria.ModLoader;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;
using TestMod.Helpers;

namespace TestMod.UI
{
    public class PaintTooltipUI : UIView
    {
        private bool _justLeftMouseDown;
        private bool _leftMouseDown;

        internal bool EyeDropperActive;
        internal bool TransparentSelectionEnabled = false;
        internal int startTileX = -1;
        internal int startTileY = -1;
        internal int lastMouseTileX = -1;
        internal int lastMouseTileY = -1;

		public PaintTooltipUI()
        {
            Visible = false;

            onMouseDown += (s, e) =>
            {
                if (!Main.LocalPlayer.mouseInterface && !UIView.MouseRightButton)
                {
                    _leftMouseDown = true;
                    Main.LocalPlayer.mouseInterface = true;
                }
            };
            onMouseUp += (s, e) =>
            {
                if (!Main.LocalPlayer.mouseInterface && (!UIView.MousePrevRightButton || (UIView.MousePrevLeftButton && !UIView.MouseLeftButton)))
                {
                    _justLeftMouseDown = true; 
                    _leftMouseDown = false; /*startTileX = -1; startTileY = -1;*/
                }
            };
        }
        protected override bool IsMouseInside()
        {
            //if (hidden) return false;
            if (EyeDropperActive) 
                return true;
            return base.IsMouseInside();
        }

        public override void Draw(SpriteBatch spriteBatch)
		{
            if (Visible && (base.IsMouseInside()))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			float x = Main.fontMouseText.MeasureString(UIView.HoverText).X;
			Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY) + new Vector2(16f);
			if (vector.Y > (float)(Main.screenHeight - 30))
			{
				vector.Y = (float)(Main.screenHeight - 30);
			}
			if (vector.X > (float)Main.screenWidth - x)
			{
				vector.X = (float)(Main.screenWidth - 460);
			}
			Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, UIView.HoverText, vector.X, vector.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
		}

		public void DrawGameScale(SpriteBatch spriteBatch)
		{
			try
			{
				if (Visible && !base.IsMouseInside() && EyeDropperActive)
				{
					if (!Main.LocalPlayer.mouseInterface)
					{
						DrawBrush();
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		internal static Color buffColor(Color newColor, float R, float G, float B, float A)
		{
			newColor.R = (byte)((float)newColor.R * R);
			newColor.G = (byte)((float)newColor.G * G);
			newColor.B = (byte)((float)newColor.B * B);
			newColor.A = (byte)((float)newColor.A * A);
			return newColor;
		}

		private void DrawBrush()
		{
			if (EyeDropperActive)
			{
				Vector2 upperLeft;
				Vector2 lowerRight;
				if (_leftMouseDown)
				{
					upperLeft = new Vector2(Math.Min(startTileX, lastMouseTileX), Math.Min(startTileY, lastMouseTileY));
					lowerRight = new Vector2(Math.Max(startTileX, lastMouseTileX) + 1, Math.Max(startTileY, lastMouseTileY) + 1);
				}
				else
				{
					upperLeft = Main.MouseWorld.ToTileCoordinates().ToVector2();
					lowerRight = upperLeft.Offset(1, 1);
				}
				Vector2 upperLeftScreen = upperLeft * 16f;
				Vector2 lowerRightScreen = lowerRight * 16f;
				upperLeftScreen -= Main.screenPosition;
				lowerRightScreen -= Main.screenPosition;
				if (Main.LocalPlayer.gravDir == -1f)
				{
					upperLeftScreen.Y = (float)Main.screenHeight - upperLeftScreen.Y;// - 16f;
					lowerRightScreen.Y = (float)Main.screenHeight - lowerRightScreen.Y;// - 16f;

					Utils.Swap(ref upperLeftScreen.Y, ref lowerRightScreen.Y);
				}

				DrawSelectedRectangle(upperLeftScreen, lowerRight - upperLeft, 1f, _leftMouseDown);
			}
			/*
			else if (StampToolActive && stampInfo != null)
			{
				int width = StampTiles.GetLength(0);
				int height = StampTiles.GetLength(1);
				Vector2 vector = Snap.GetSnapPosition(CheatSheet.instance.paintToolsUI.SnapType, width, height, constrainToAxis, constrainedX, constrainedY, false);

				if (!_leftMouseDown && !Main.LocalPlayer.mouseInterface)
				{
					DrawPreview(Main.spriteBatch, stampInfo.Tiles, vector);
				}

				DrawSelectedRectangle(vector, new Vector2(width, height), _leftMouseDown ? 1f : .25f, false);
			}
			*/
		}

		private void DrawSelectedRectangle(Vector2 upperLeftScreen, Vector2 brushSize, float r, bool drawBack = true)
		{
			Rectangle value = new Rectangle(0, 0, 1, 1);
			//float r = 1f;
			float g = 0.9f;
			float b = 0.1f;
			float a = 1f;
			float scale = 0.6f;
			Color color = buffColor(Color.White, r, g, b, a);
			if (drawBack)
			{
				Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, 16f * brushSize, SpriteEffects.None, 0f);
			}
			b = 0.3f;
			g = 0.95f;
			scale = (a = 1f);
			color = buffColor(Color.White, r, g, b, a);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitX * -2f, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(2f, 16f * brushSize.Y), SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitX * 16f * brushSize.X, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(2f, 16f * brushSize.Y), SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitY * -2f, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(16f * brushSize.X, 2f), SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitY * 16f * brushSize.Y, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(16f * brushSize.X, 2f), SpriteEffects.None, 0f);

			Vector2 pos = Main.MouseScreen.Offset(48, 24);
			Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, $"{brushSize.X} x {brushSize.Y}", pos.X, pos.Y, Color.White, Color.Black, Vector2.Zero, 1f);
		}


		public void UpdateGameScale()
		{
			try
			{
				Update2();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

        public void Update2()
        {
            Player player = Main.LocalPlayer;
            if (EyeDropperActive)
            {
                player.showItemIcon2 = ItemID.EmptyDropper;
                if (_leftMouseDown)
                {
                    var point = Main.MouseWorld.ToTileCoordinates();
                    if (startTileX == -1)
                    {
                        startTileX = point.X;
                        startTileY = point.Y;
                        lastMouseTileX = -1;
                        lastMouseTileY = -1;
                    }

                    lastMouseTileX = point.X;
                    lastMouseTileY = point.Y;
                }

                if (_justLeftMouseDown)
                {
                    if (startTileX != -1 && startTileY != -1 && lastMouseTileX != -1 && lastMouseTileY != -1)
                    {
                        var upperLeft = new Vector2(
                            Math.Min(startTileX, lastMouseTileX),
                            Math.Min(startTileY, lastMouseTileY));
                        var lowerRight = new Vector2(
                            Math.Max(startTileX, lastMouseTileX),
                            Math.Max(startTileY, lastMouseTileY));

                        var width = (int)(lowerRight.X + 1 - upperLeft.X);
                        var height = (int)(lowerRight.Y + 1 - upperLeft.Y);

                        var entity = new DimensionEntity<DimensionExample.DimensionExample>
                        {
                            Location = upperLeft.ToPoint(),
                            Type = DimensionRegisterExample.ExampleName,
                            Size = new Point(width, height),
                            Dimension = new DimensionExample.DimensionExample()
                        };

                        var injector = DimensionLoader.RegisteredDimension.GetInjector(entity.Type);
                        injector.Synchronize(entity);

                        DataParserExample.AddDimension(entity.Dimension);

                        Hide();
                    }

                    startTileX = -1;
                    startTileY = -1;
                    lastMouseTileX = -1;
                    lastMouseTileY = -1;
                    _justLeftMouseDown = false;
                }
            }
            /*
            if (StampToolActive)
            {
                player.showItemIcon2 = ItemID.Paintbrush;
                //		Main.LocalPlayer.showItemIconText = "Click to paint";
                if (_leftMouseDown && stampInfo != null)
                {
                    int width = StampTiles.GetLength(0);
                    int height = StampTiles.GetLength(1);
                    //Vector2 brushsize = new Vector2(width, height);
                    //Vector2 evenOffset = Vector2.Zero;
                    //if (width % 2 == 0)
                    //{
                    //	evenOffset.X = 1;
                    //}
                    //if (height % 2 == 0)
                    //{
                    //	evenOffset.Y = 1;
                    //}
                    //Point point = (Main.MouseWorld + evenOffset * 8).ToTileCoordinates();
                    ////Point point = (Main.MouseWorld + (brushSize % 2 == 0 ? Vector2.One * 8 : Vector2.Zero)).ToTileCoordinates();
                    //point.X -= width / 2;
                    //point.Y -= height / 2;
                    ////Vector2 vector = new Vector2(point.X, point.Y) * 16f;
                    ////vector -= Main.screenPosition;
                    ////if (Main.LocalPlayer.gravDir == -1f)
                    ////{
                    ////	vector.Y = (float)Main.screenHeight - vector.Y - 16f;
                    ////}

                    Point point = Snap.GetSnapPosition(CheatSheet.instance.paintToolsUI.SnapType, width, height,
                        constrainToAxis, constrainedX, constrainedY, true).ToPoint();

                    if (startTileX == -1)
                    {
                        startTileX = point.X;
                        startTileY = point.Y;
                        lastMouseTileX = -1;
                        lastMouseTileY = -1;
                    }

                    if (Main.keyState.IsKeyDown(Keys.LeftShift))
                    {
                        constrainToAxis = true;
                        if (constrainedStartX == -1 && constrainedStartY == -1)
                        {
                            constrainedStartX = point.X;
                            constrainedStartY = point.Y;
                        }

                        if (constrainedX == -1 && constrainedY == -1)
                        {
                            if (constrainedStartX != point.X)
                            {
                                constrainedY = point.Y;
                            }
                            else if (constrainedStartY != point.Y)
                            {
                                constrainedX = point.X;
                            }
                        }

                        if (constrainedX != -1)
                        {
                            point.X = constrainedX;
                        }

                        if (constrainedY != -1)
                        {
                            point.Y = constrainedY;
                        }
                    }
                    else
                    {
                        constrainToAxis = false;
                        constrainedX = -1;
                        constrainedY = -1;
                        constrainedStartX = -1;
                        constrainedStartY = -1;
                    }

                    if (lastMouseTileX != point.X || lastMouseTileY != point.Y)
                    {
                        lastMouseTileX = point.X;
                        lastMouseTileY = point.Y;
                        //Main.NewText("StartTileX " + startTileX);
                        UndoHistory.Push(Tuple.Create(point, new Tile[width, height]));
                        UpdateUndoTooltip();
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                if (WorldGen.InWorld(x + point.X, y + point.Y) && StampTiles[x, y] != null)
                                {
                                    Tile target = Framing.GetTileSafely(x + point.X, y + point.Y);
                                    UndoHistory.Peek().Item2[x, y] = new Tile(target);
                                    int cycledX = ((x + point.X - startTileX) % width + width) % width;
                                    int cycledY = ((y + point.Y - startTileY) % height + height) % height;
                                    if (TransparentSelectionEnabled) // What about just walls?
                                    {
                                        if (StampTiles[cycledX, cycledY].active())
                                        {
                                            target.CopyFrom(StampTiles[cycledX, cycledY]);
                                        }
                                    }
                                    else
                                    {
                                        target.CopyFrom(StampTiles[cycledX, cycledY]);
                                    }
                                }
                            }
                        }

                        // TODO: Experiment with WorldGen.stopDrops = true;
                        // TODO: Button to ignore TileFrame?
                        for (int i = point.X; i < point.X + width; i++)
                        {
                            for (int j = 0; j < point.Y + height; j++)
                            {
                                WorldGen.SquareTileFrame(i, j,
                                    false); // Need to do this after stamp so neighbors are correct.
                                if (Main.netMode == 1 && Framing.GetTileSafely(i, j).liquid > 0)
                                {
                                    NetMessage.sendWater(i, j); // Does it matter that this is before sendtilesquare?
                                }
                            }
                        }

                        if (Main.netMode == 1)
                        {
                            NetMessage.SendTileSquare(-1, point.X + width / 2, point.Y + height / 2,
                                Math.Max(width, height));
                        }
                    }
                }
                else
                {
                    startTileX = -1;
                    startTileY = -1;
                    constrainToAxis = false;
                    constrainedX = -1;
                    constrainedY = -1;
                    constrainedStartX = -1;
                    constrainedStartY = -1;
                }
            }

            */
            Main.LocalPlayer.showItemIcon = true;
        }


		public void Hide()
		{
            //StampToolActive = false;
            Visible = false;
            EyeDropperActive = false;
        }

		public void Show()
		{
            Visible = true;
            EyeDropperActive = true;
        }
    }
}
