using MelonLoader;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppTLD.Gear;
using static Il2CppSystem.Xml.XmlWellFormedWriter.AttributeValueCache;

namespace WeightTweaks
{
    class WeightTweaks : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
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
