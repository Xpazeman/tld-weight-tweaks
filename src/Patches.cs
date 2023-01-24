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

    [HarmonyPatch(typeof(GearItem), nameof(GearItem.GetItemWeightKG), new Type[] { typeof(bool) })]
    internal class GearItem_GetItemWeightKG
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            if (Settings.options.infiniteCarry)
            {
                __result = 0;
            }
            else if (!Settings.options.modifyWeight)
            {
                return;
            }
            else
            {
                __result = WeightTweaks.ModifyWeight(__instance, __result);
            }
        }
    }

    [HarmonyPatch(typeof(GearItem), nameof(GearItem.GetItemWeightKG), new Type[] { typeof(float), typeof(bool) })]
    internal class GearItem_GetItemWeightKG_Stack
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            if (Settings.options.infiniteCarry)
            {
                __result = 0;
            }
            else if (!Settings.options.modifyWeight)
            {
                return;
            }
            else
            {
                __result = WeightTweaks.ModifyWeight(__instance, __result);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UpdateCarryingBuff))]
    internal class PlayerManager_UpdateCarryingBuff
    {
        private static void Prefix(PlayerManager __instance)
        {
            GameManager.GetPlayerManagerComponent().m_ClothingWeightWhenWornModifier = Settings.options.clothingWornWeightMod;
        }
    }
}
