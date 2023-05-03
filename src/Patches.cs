using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;

namespace WeightTweaks
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.GetItemWeightKG), new Type[] { typeof(bool) })]
    internal class GearItem_GetItemWeightKG
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            __result = WeightTweaks.ModifyWeight(__instance, __result);
        }
    }

    [HarmonyPatch(typeof(GearItem), nameof(GearItem.GetItemWeightKG), new Type[] { typeof(float), typeof(bool) })]
    internal class GearItem_GetItemWeightKG_Stack
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            __result = WeightTweaks.ModifyWeight(__instance, __result);
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
