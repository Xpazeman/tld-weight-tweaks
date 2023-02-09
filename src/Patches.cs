using System;
using HarmonyLib;
using UnityEngine;
using Il2Cpp;

namespace WeightTweaks
{
    [HarmonyPatch(typeof(Encumber), nameof(Encumber.Start))]
    internal class Encumber_Start
    {
        private static void Postfix(Encumber __instance)
        {
            WeightTweaks.EncumberUpdate(__instance);
        }
    }

    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal class GearItem_Awake
    {
        private static void Postfix(GearItem __instance)
        {
            WeightTweaksHandler handler = __instance.gameObject.AddComponent<WeightTweaksHandler>();
            handler.Init(__instance);
            WeightTweaks.itemList.Add(handler);

            if (Settings.options.infiniteCarry) { 
                handler.ModifyWeight(0);
            }
            else
            {
                handler.ModifyWeight(WeightTweaks.GetWeightModifier(__instance));
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UpdateCarryingBuff))]
    internal class PlayerManager_UpdateCarryingBuff
    {
        private static void Prefix(PlayerManager __instance)
        {
            __instance.m_ClothingWeightWhenWornModifier = Settings.options.clothingWornWeightMod;
        }
    }
}
