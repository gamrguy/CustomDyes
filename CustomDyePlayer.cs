using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CustomDyes
{
    class CustomDyePlayer : ModPlayer
    {
        public static List<Item> customDye = new List<Item>();

        private static int count = 0;
        private PlayerLayer TrackerLayer(string name, DyeSlotType slotType) 
            => new PlayerLayer("CustomDyes", "tracker" + name, (info) => {
                var item = info.drawPlayer.GetDyeItem(slotType);
                if(item != null) {
                    for(int i = count; i < Main.playerDrawData.Count; i++) {
                        if(Main.playerDrawData[i].shader == CustomDyes.DyeID) {
                            customDye.Add(item);
                        }
                    }
                }
                count = Main.playerDrawData.Count;
            });

        private readonly PlayerLayer resetLayer = new PlayerLayer("CustomDyes", "terminator", (data) => {
            count = 0;
            customDye.Clear();
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers) {
            layers.Insert(layers.IndexOf(PlayerLayer.Arms) + 1, TrackerLayer("arms", DyeSlotType.Body));
            //layers.Insert(layers.IndexOf(PlayerLayer.Face) + 1, TrackerLayer("face", DyeSlotType.Face));
            layers.Insert(layers.IndexOf(PlayerLayer.Body) + 1, TrackerLayer("body", DyeSlotType.Body));
            layers.Insert(layers.IndexOf(PlayerLayer.BackAcc) + 1, TrackerLayer("back", DyeSlotType.Back));
            layers.Insert(layers.IndexOf(PlayerLayer.BalloonAcc) + 1, TrackerLayer("balloon", DyeSlotType.Balloon));
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc) + 1, TrackerLayer("face", DyeSlotType.Face));
            layers.Insert(layers.IndexOf(PlayerLayer.FrontAcc) + 1, TrackerLayer("front", DyeSlotType.Front));
            layers.Insert(layers.IndexOf(PlayerLayer.Hair) + 1, TrackerLayer("hair", DyeSlotType.Head));
            layers.Insert(layers.IndexOf(PlayerLayer.HairBack) + 1, TrackerLayer("hairback", DyeSlotType.Head));
            layers.Insert(layers.IndexOf(PlayerLayer.HandOffAcc) + 1, TrackerLayer("handoff", DyeSlotType.HandOff));
            layers.Insert(layers.IndexOf(PlayerLayer.HandOnAcc) + 1, TrackerLayer("handon", DyeSlotType.HandOn));
            layers.Insert(layers.IndexOf(PlayerLayer.Head) + 1, TrackerLayer("head", DyeSlotType.Head));
            //layers.Insert(layers.IndexOf(PlayerLayer.HeldItem) + 1, TrackerLayer("helditem", DyeSlotType.Back));
            layers.Insert(layers.IndexOf(PlayerLayer.Legs) + 1, TrackerLayer("legs", DyeSlotType.Legs));
            layers.Insert(layers.IndexOf(PlayerLayer.MountBack) + 1, TrackerLayer("mountback", DyeSlotType.Mount));
            layers.Insert(layers.IndexOf(PlayerLayer.MountFront) + 1, TrackerLayer("mountfront", DyeSlotType.Mount));
            layers.Insert(layers.IndexOf(PlayerLayer.NeckAcc) + 1, TrackerLayer("neck", DyeSlotType.Neck));
            layers.Insert(layers.IndexOf(PlayerLayer.ShieldAcc) + 1, TrackerLayer("shield", DyeSlotType.Shield));
            layers.Insert(layers.IndexOf(PlayerLayer.ShoeAcc) + 1, TrackerLayer("shoe", DyeSlotType.Shoe));
            layers.Insert(layers.IndexOf(PlayerLayer.WaistAcc) + 1, TrackerLayer("waist", DyeSlotType.Waist));
            layers.Insert(layers.IndexOf(PlayerLayer.Wings) + 1, TrackerLayer("wings", DyeSlotType.Wings));
            layers.Insert(0, resetLayer);
        }
    }
}
