using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;
using TestMod.Globals;
using TestMod.LevelSystem;
using TestMod.LevelSystem.UI;

namespace TestMod
{
	public class TestMod : Mod
    {
        private UserInterface _exampleUserInterface;
        
        internal UserInterface ExamplePersonUserInterface;
        internal LevelUIState LevelUiState;

        public override void AddRecipeGroups()
        {
            GetGlobalItem<AllUIUpdater>().Initialize();
        }

        public override void Load()
		{
            Terraria.ModLoader.IO.TagSerializer.AddSerializer(new LevelSerializer());

            // All code below runs only if we're not loading on a server
            if (!Main.dedServ)
			{
				// Custom UI
                LevelUiState = new LevelUIState();
                LevelUiState.Activate();
				_exampleUserInterface = new UserInterface();
				_exampleUserInterface.SetState(LevelUiState);

				// UserInterface can only show 1 UIState at a time. If you want different "pages" for a UI, switch between UIStates on the same UserInterface instance. 
				// We want both the Coin counter and the Example Person UI to be independent and coexist simultaneously, so we have them each in their own UserInterface.
				ExamplePersonUserInterface = new UserInterface();
				// We will call .SetState later in ExamplePerson.OnChatButtonClicked
			}

            DimensionLoader.RegisterDimensions<DimensionRegisterExample>();
        }

        public override void Unload()
        {
            DimensionLoader.RegisteredDimension.Clear();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (LevelUIState.Visible)
            {
                _exampleUserInterface?.Update(gameTime);
            }

            ExamplePersonUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: Coins Per Minute",
                    delegate {
                        if (LevelUIState.Visible)
                        {
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: All UI",
                    delegate
                    {
                        GetGlobalItem<AllUIUpdater>().DrawUpdateAll(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: Paint Tooltip",
                    delegate
                    {
                        GetGlobalItem<AllUIUpdater>().DrawUpdatePaintTools(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.Game)
                );
            }
        }
	}
}