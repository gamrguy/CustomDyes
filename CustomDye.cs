using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CustomDyes
{
    class CustomDye : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.ReflectiveGoldDye;

        public enum StuffThings
        {
            ArmorLivingRainbow,
            ArmorLivingOcean,
            ArmorVoid
        }

        public StuffThings currentThing = StuffThings.ArmorLivingRainbow;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Custom Dye (Blank)");
        }

        public override void SetDefaults() {
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 10;
            item.useAnimation = 10;
            item.consumable = false;
        }

        public override bool UseItem(Player player) {
            currentThing += 1;
            if(!Enum.IsDefined(typeof(StuffThings), currentThing)) {
                currentThing = 0;
            }
            return true;
        }

        public override ModItem Clone(Item item) {
            (item.modItem as CustomDye).currentThing = currentThing;
            return item.modItem;
        }

        public override void NetSend(BinaryWriter writer) {
            writer.Write((int)currentThing);
        }

        public override void NetRecieve(BinaryReader reader) {
            currentThing = (StuffThings)reader.ReadInt32();
        }
    }
}
