using System.IO;
using System.Reflection;
using UnityEngine;
using MelonLoader;

namespace WeightTweaks
{
    public class WeightTweaks : MelonMod
    {
        public override void OnApplicationStart()
        {
            Settings.OnLoad();

            Debug.Log("[weight-tweaks] Version " + Assembly.GetExecutingAssembly().GetName().Version);
        }

        public static void EncumberUpdate()
        {
            if (Settings.options.carryKgAdd > 0)
            {
                GameManager.GetEncumberComponent().m_MaxCarryCapacityKG = 30f + Settings.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_MaxCarryCapacityWhenExhaustedKG = 15f + Settings.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_NoSprintCarryCapacityKG = 40f + Settings.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_NoWalkCarryCapacityKG = 60f + Settings.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_EncumberLowThresholdKG = 31f + Settings.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_EncumberMedThresholdKG = 40f + Settings.options.carryKgAdd;
                GameManager.GetEncumberComponent().m_EncumberHighThresholdKG = 60f + Settings.options.carryKgAdd;
            }
        }

        public static float ModifyWeight(GearItem item, float baseWeight)
        {
            if (item.m_ClothingItem)
            {
                return baseWeight * Settings.options.clothingWeightMod;
            }
            else if (item.m_WaterSupply)
            {
                return baseWeight * Settings.options.waterWeightMod;
            }
            else if (item.m_FoodItem)
            {
                return baseWeight * Settings.options.foodWeightMod;
            }
            else if (item.m_GunItem || item.m_BowItem || item.m_AmmoItem || item.m_ArrowItem)
            {
                return baseWeight * Settings.options.rifleWeightMod;
            }
            else if (item.m_BodyHarvest)
            {
                return baseWeight * Settings.options.quarterWeightMod;
            }
            else if (item.m_ToolsItem || item.m_FlashlightItem || item.m_CookingPotItem || item.m_FlareItem || item.m_TorchItem || item.m_FishingItem || item.m_KeroseneLampItem)
            {
                return baseWeight * Settings.options.toolWeightMod;
            }
            else
            {
                return baseWeight * Settings.options.defaultWeightMod;
            }
        }
    }
}
