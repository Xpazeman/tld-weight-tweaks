using System.IO;
using System.Reflection;
using UnityEngine;

namespace WeightTweaks
{
    public class WeightTweaksOptions
    {
        public bool infiniteCarry = false;
        public int carryKgAdd = 0;

        public float clothingWeightMod = 1f;
        public float clothingWornWeightMod = 0.25f;
        public float waterWeightMod = 1f;
        public float foodWeightMod = 1f;
        public float rifleWeightMod = 1f;
        public float quarterWeightMod = 1f;
        public float toolWeightMod = 1f;
        public float defaultWeightMod = 1f;
    }

    public class WeightTweaks
    {
        public static string modsFolder;
        public static string modOptionsFolder;
        public static string optionsFolderName = "xpazeman-minimods";
        public static string optionsFileName = "config-weight.json";

        public static WeightTweaksOptions options;

        public static void OnLoad()
        {
            modsFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            modOptionsFolder = Path.Combine(modsFolder, optionsFolderName);

            WeightTweaksSettings weightSettings = new WeightTweaksSettings();
            weightSettings.AddToModSettings("Weight Tweaks");
            options = weightSettings.setOptions;

            Debug.Log("[weight-tweaks] Version " + Assembly.GetExecutingAssembly().GetName().Version);
        }

        public static void EncumberUpdate()
        {
            if (options.carryKgAdd > 0)
            {
                GameManager.GetEncumberComponent().m_MaxCarryCapacityKG = 30f + WeightTweaks.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_MaxCarryCapacityWhenExhaustedKG = 15f + WeightTweaks.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_NoSprintCarryCapacityKG = 40f + WeightTweaks.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_NoWalkCarryCapacityKG = 60f + WeightTweaks.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_EncumberLowThresholdKG = 31f + WeightTweaks.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_EncumberMedThresholdKG = 40f + WeightTweaks.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_EncumberHighThresholdKG = 60f + WeightTweaks.options.carryKgAdd;
            }
        }

        public static float ModifyWeight(GearItem item, float baseWeight)
        {
            if (item.m_ClothingItem)
            {
                return baseWeight * options.clothingWeightMod;
            }
            else if (item.m_WaterSupply)
            {
                return baseWeight * options.waterWeightMod;
            }
            else if (item.m_FoodItem)
            {
                return baseWeight * options.foodWeightMod;
            }
            else if (item.m_GunItem || item.m_BowItem || item.m_AmmoItem || item.m_ArrowItem)
            {
                return baseWeight * options.rifleWeightMod;
            }
            else if (item.m_BodyHarvest)
            {
                return baseWeight * options.quarterWeightMod;
            }
            else if (item.m_ToolsItem || item.m_FlashlightItem || item.m_CookingPotItem || item.m_FlareItem || item.m_TorchItem || item.m_FishingItem || item.m_KeroseneLampItem)
            {
                return baseWeight * options.toolWeightMod;
            }
            else
            {
                return baseWeight * options.defaultWeightMod;
            }
        }
    }
}
