using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace TestMod.LevelSystem
{
    public class LevelSerializer : TagSerializer<LevelingService, TagCompound>
    {
        public override TagCompound Serialize(LevelingService value) => new TagCompound
        {
            [nameof(value.Level)] = value.Level,
            [nameof(value.Experience)] = value.Experience,
        };

        public override LevelingService Deserialize(TagCompound tag)
        {
            var levelingService = new LevelingService();
            levelingService.Level = tag.GetInt(nameof(levelingService.Level));
            levelingService.Experience = tag.GetInt(nameof(levelingService.Experience));

            return levelingService;
        }
    }
}
