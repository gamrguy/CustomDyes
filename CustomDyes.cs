using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CustomDyes
{
	public class CustomDyes : Mod
	{
		public static int DyeID => GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<CustomDye>());

        public override void PostSetupContent() {
            GameShaders.Armor.BindShader(ModContent.ItemType<CustomDye>(), new CustomShader(Main.PixelShaderRef, "ArmorLivingRainbow", "ArmorLivingOcean"));
        }
    }
}