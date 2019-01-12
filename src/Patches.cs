using System;
using Harmony;
using UnityEngine;

namespace WeightTweaks
{
    [HarmonyPatch(typeof(Encumber), "GetEffectiveCarryCapacityKG")]
    internal class Encumber_GetEffectiveCarryCapacityKG
    {
        private static void Prefix(Encumber __instance)
        {
            WeightTweaks.EncumberUpdate();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "PlayerCantSprintBecauseOfInjury")]
    internal class PlayerManager_PlayerCantSprintBecauseOfInjury
    {
        private static void Prefix(Encumber __instance)
        {
            WeightTweaks.EncumberUpdate();
        }
    }

    [HarmonyPatch(typeof(GearItem), "GetItemWeightKG")]
    internal class GearItem_GetItemWeightKG
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            if (WeightTweaks.options.infiniteCarry)
            {
                __result = 0;
            }
            else
            {
                __result = WeightTweaks.ModifyWeight(__instance, __result);
            }
        }
    }

    [HarmonyPatch(typeof(GearItem), "GetItemWeightIgnoreClothingWornBonusKG")]
    internal class GearItem_GetItemWeightIgnoreClothingWornBonusKG
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            if (WeightTweaks.options.infiniteCarry)
            {
                __result = 0;
            }
            else
            {
                __result = WeightTweaks.ModifyWeight(__instance, __result);
            }
        }
    }

    [HarmonyPatch(typeof(ClothingItem), "GetWeightModifier")]
    internal class ClothingItem_GetWeightModifier
    {
        private static void Prefix(ClothingItem __instance)
        {
            GameManager.GetPlayerManagerComponent().m_ClothingWeightWhenWornModifier = WeightTweaks.options.clothingWornWeightMod;
        }
    }

    [HarmonyPatch(typeof(Encumber), "GetHourlyCalorieBurnFromWeight")]
    internal class Encumber_GetHourlyCalorieBurnFromWeight
    {
        private static void Postfix(Encumber __instance, ref float __result)
        {
            float modifier = (WeightTweaks.options.carryKgAdd + 30f) / 30f;

            __result = __result / modifier;
        }
    }
}
