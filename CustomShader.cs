using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace CustomDyes
{
    public class CustomShader : ArmorShaderData
    {
        private static List<CustomDyeData> dyeDataQueue;

        public static void AddToQueue(CustomDyeData data) {
            dyeDataQueue.Add(data);
        }

        public static void Initialize() {
            dyeDataQueue = new List<CustomDyeData>();
        }

        public static void Uninitialize() {
            dyeDataQueue = null;
        }

        public string passName1;
        public string passName2;
        public CustomShader(Ref<Effect> shader, string passName, string passName2) : base(shader, passName) {
            this.passName1 = passName;
            this.passName2 = passName2;
        }

        public override void Apply(Entity entity, DrawData? drawData = null) {
            if(entity is Player && CustomDyePlayer.customDye.Count > 0) {
                var flag = CustomDyePlayer.customDye[0];
                CustomDyePlayer.customDye.RemoveAt(0);
                var dye = flag.modItem as CustomDye;
                if(dye != null) SwapProgram(dye.currentThing.ToString());
            }
            base.Apply(entity, drawData);
        }
    }
}
