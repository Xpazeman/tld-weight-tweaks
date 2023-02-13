using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;

namespace WeightTweaks
{
    [HarmonyPatch(typeof(SaveGameSystem), "LoadSceneData", new Type[] { typeof(string), typeof(string) })]
    internal class SaveGameSystem_LoadSceneData
    {
        public static void Prefix(SaveGameSystem __instance, string name, string sceneSaveName)
        {
            //WeightTweaks.itemList.Clear();
            //WeightTweaks.itemDataChanged.Clear();
        }
    }

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
            WeightTweaks.ModifyItemWeight(__instance);
        }
    }

    /*[HarmonyPatch(typeof(GearItem), nameof(GearItem.CalculateLiquidWeight), new Type[] {typeof(float), typeof(GearLiquidTypeEnum)})]
    internal class GearItem_CalculateLiquidWeight
    {
        private static void Postfix(GearItem __instance, ref float __result)
        {
            __result *= WeightTweaks.GetWeightModifier(WeightTweaks.ModTypes.Water);
        }
    }*/

    [HarmonyPatch(typeof(Panel_BodyHarvest), nameof(Panel_BodyHarvest.HarvestSuccessful))]
    internal class Panel_BodyHarvest_HarvestSuccessful
    {
        private static void Prefix(Inventory __instance)
        {
            WeightTweaks.RestoreItemWeights();
        }

        private static void Postfix(Inventory __instance)
        {
            WeightTweaks.ResetItemWeights();
        }
    }

    [HarmonyPatch(typeof(Inventory), nameof(Inventory.AddGear))]
    internal class Inventory_AddGear
    {
        private static void Postfix(Inventory __instance, GearItem gi)
        {
            WeightTweaks.ModifyItemWeight(gi);
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
