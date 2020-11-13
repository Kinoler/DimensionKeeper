using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TestMod.LevelSystem.UI;

namespace TestMod.Command
{
    public class LevelCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "lvl";

        public override string Description
            => "Show the level UI";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            LevelUIState.Visible = true;
        }
    }
}
