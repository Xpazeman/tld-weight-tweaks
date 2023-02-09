using MelonLoader;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;

namespace WeightTweaks
{
    class WeightTweaks : MelonMod
    {
        public static List<WeightTweaksHandler> itemList = new List<WeightTweaksHandler>();
        
        public override void OnInitializeMelon()
        {
            ClassInjector.RegisterTypeInIl2Cpp<WeightTweaksHandler>();
            Settings.OnLoad();
        }

        public static void EncumberUpdate(Encumber encumberComp)
        {
            if (Settings.options.carryKgAdd > 0)
            {
                encumberComp.m_MaxCarryCapacityKG = 30f + Settings.options.carryKgAdd;
                encumberComp.m_MaxCarryCapacityWhenExhaustedKG = 15f + Settings.options.carryKgAdd;
                encumberComp.m_NoSprintCarryCapacityKG = 40f + Settings.options.carryKgAdd;
                encumberComp.m_NoWalkCarryCapacityKG = 60f + Settings.options.carryKgAdd;
                encumberComp.m_EncumberLowThresholdKG = 31f + Settings.options.carryKgAdd;
                encumberComp.m_EncumberMedThresholdKG = 40f + Settings.options.carryKgAdd;
                encumberComp.m_EncumberHighThresholdKG = 60f + Settings.options.carryKgAdd;
            }
        }

        public static float GetWeightModifier(GearItem item)
        {
            if (item.m_ClothingItem)
            {
                return Settings.options.clothingWeightMod;
            }
            else if (item.m_WaterSupply)
            {
                return Settings.options.waterWeightMod;
            }
            else if (item.m_FoodItem)
            {
                return Settings.options.foodWeightMod;
            }
            else if (item.m_GunItem || item.m_BowItem || item.m_AmmoItem || item.m_ArrowItem)
            {
                return Settings.options.rifleWeightMod;
            }
            else if (item.m_BodyHarvest)
            {
                return Settings.options.quarterWeightMod;
            }
            else if (item.m_ToolsItem || item.m_FlashlightItem || item.m_CookingPotItem || item.m_FlareItem || item.m_TorchItem || item.m_FishingItem || item.m_KeroseneLampItem)
            {
                return Settings.options.toolWeightMod;
            }
            else
            {
                return Settings.options.defaultWeightMod;
            }
        }
    }
}
