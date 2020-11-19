using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CustomDyes
{
    public enum DyeSlotType
    {
        Head,
        Body,
        Legs,

        HandOn,
        HandOff,
        Back,
        Front,
        Shoe,
        Waist,
        Wings,
        Shield,
        Neck,
        Face,
        Balloon,
        Carpet,

        Pet,
        Light,
        Minecart,
        Mount,     // WARNING: Mounts and the Flying Carpet share a layer
        Grapple
    }

    public static class PlayerExtensions
    {
        public static Item GetDyeItem(this Player player, DyeSlotType type) {
            switch(type) {
                case DyeSlotType.Head:
                case DyeSlotType.Body:
                case DyeSlotType.Legs:
                    return player.dye[(int)type];
                case DyeSlotType.Mount:
                    if(player.carpet) return player.GetDyeFromArmorSlot((EquipType)DyeSlotType.Carpet);
                    else return player.miscDyes[(int)type - 15];
                case DyeSlotType.Pet:
                case DyeSlotType.Light:
                case DyeSlotType.Minecart:
                case DyeSlotType.Grapple:
                    return player.miscDyes[(int)type - 15];
                default:
                    return player.GetDyeFromArmorSlot((EquipType)type);
            }
        }

        private static Item GetDyeFromArmorSlot(this Player player, EquipType slot) {
            for(int i = 0; i < 20; i++) {
                int num = i % 10;
                if(player.dye[num] != null && player.armor[i].type > ItemID.None && player.armor[i].stack > 0 && (i / 10 >= 1 || !player.hideVisual[num] || player.armor[i].wingSlot > 0 || player.armor[i].type == ItemID.FlyingCarpet)) {
                    if(slot == EquipType.HandsOn && player.armor[i].handOnSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.HandsOff && player.armor[i].handOffSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Back && player.armor[i].backSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Front && player.armor[i].frontSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Shoes && player.armor[i].shoeSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Waist && player.armor[i].waistSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Shield && player.armor[i].shieldSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Neck && player.armor[i].neckSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Face && player.armor[i].faceSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Balloon && player.armor[i].balloonSlot > 0)
                        return player.dye[num];

                    if(slot == EquipType.Wings && player.armor[i].wingSlot > 0)
                        return player.dye[num];

                    if(slot == (EquipType)DyeSlotType.Carpet && player.armor[i].type == ItemID.FlyingCarpet)
                        return player.dye[num];
                }
            }
            return null;
        }
    }
}
