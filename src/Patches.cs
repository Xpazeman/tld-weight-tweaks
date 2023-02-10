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
            GearItemData itemData = __instance.m_GearItemData;
            
            if (!WeightTweaks.originalWeights.ContainsKey(itemData.name))
            {
                WeightTweaks.ModTypes itemType = WeightTweaks.GetWeightModifierType(__instance);

                WeightTweaks.originalWeights.Add(itemData.name, itemData.m_BaseWeightKG);
                WeightTweaks.itemDataList.Add(itemData.name, itemData);
                WeightTweaks.itemDataType.Add(itemData.name, itemType);

                if (Settings.options.infiniteCarry)
                {
                    itemData.m_BaseWeightKG = 0;
                }
                else
                {
                    itemData.m_BaseWeightKG *= WeightTweaks.GetWeightModifier(itemType);
                }

                __instance.WeightKG = itemData.m_BaseWeightKG;
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
