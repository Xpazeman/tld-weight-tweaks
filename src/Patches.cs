using System;
using HarmonyLib;
using UnityEngine;
using Il2Cpp;

namespace WeightTweaks
{
    [HarmonyPatch(typeof(Encumber), nameof(Encumber.GetEffectiveCarryCapacityKG))]
    internal class Encumber_GetEffectiveCarryCapacityKG
    {
        private static void Prefix(Encumber __instance)
        {
            WeightTweaks.EncumberUpdate();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.PlayerCantSprintBecauseOfInjury))]
    internal class PlayerManager_PlayerCantSprintBecauseOfInjury
    {
        private static void Prefix(Encumber __instance)
        {
            WeightTweaks.EncumberUpdate();
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

    /*[HarmonyPatch(typeof(Encumber), nameof(Encumber.GetHourlyCalorieBurnFromWeight))]
    internal class Encumber_GetHourlyCalorieBurnFromWeight
    {
        private static void Postfix(Encumber __instance, ref float __result)
        {
            float modifier = (Settings.options.carryKgAdd + 30f) / 30f;

            __result = __result / modifier;
        }
    }*/
}
