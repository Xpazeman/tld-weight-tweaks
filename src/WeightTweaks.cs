using MelonLoader;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppTLD.Gear;
using static Il2CppSystem.Xml.XmlWellFormedWriter.AttributeValueCache;

namespace WeightTweaks
{
    class WeightTweaks : MelonMod
    {
        public static Dictionary<string, float> originalWeights = new Dictionary<string, float>();
        public static Dictionary<string, GearItemData> itemDataList = new Dictionary<string, GearItemData>();
        public static Dictionary<string, ModTypes> itemDataType = new Dictionary<string, ModTypes>();

        public enum ModTypes
        {
            Clothing,
            Water,
            Food,
            Gun,
            Quarter,
            Tool,
            Default
        }

        public override void OnInitializeMelon()
        {
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

        public static void ModifyItemWeight(GearItem gi)
        {
            GearItemData itemData = gi.m_GearItemData;

            if (gi.m_BodyHarvest)
            {
                ModifyBodyHarvestWeight(gi);
                
                return;
            }

            if (!originalWeights.ContainsKey(itemData.name))
            {
                ModTypes itemType = GetWeightModifierType(gi);

                originalWeights.Add(itemData.name, itemData.m_BaseWeightKG);
                itemDataList.Add(itemData.name, itemData);
                itemDataType.Add(itemData.name, itemType);

                if (Settings.options.infiniteCarry)
                {
                    itemData.m_BaseWeightKG = 0;
                }
                else
                {
                    itemData.m_BaseWeightKG *= GetWeightModifier(itemType);
                }
            }

            gi.WeightKG = itemData.m_BaseWeightKG;
        }

        public static ModTypes GetWeightModifierType(GearItem item)
        {
            if (item.m_ClothingItem)
            {
                return ModTypes.Clothing;
            }
            else if (item.m_WaterSupply)
            {
                return ModTypes.Water;
            }
            else if (item.m_FoodItem)
            {
                return ModTypes.Food;
            }
            else if (item.m_GunItem || item.m_BowItem || item.m_AmmoItem || item.m_ArrowItem)
            {
                return ModTypes.Gun;
            }
            else if (item.m_BodyHarvest)
            {
                return ModTypes.Quarter;
            }
            else if (item.m_ToolsItem || item.m_FlashlightItem || item.m_CookingPotItem || item.m_FlareItem || item.m_TorchItem || item.m_FishingItem || item.m_KeroseneLampItem)
            {
                return ModTypes.Tool;
            }
            else
            {
                return ModTypes.Default;
            }
        }

        public static float GetWeightModifier(ModTypes type)
        {
            if (type == ModTypes.Clothing)
            {
                return Settings.options.clothingWeightMod;
            }
            else if (type == ModTypes.Water)
            {
                return Settings.options.waterWeightMod;
            }
            else if (type == ModTypes.Food)
            {
                return Settings.options.foodWeightMod;
            }
            else if (type == ModTypes.Gun)
            {
                return Settings.options.rifleWeightMod;
            }
            else if (type == ModTypes.Quarter)
            {
                return Settings.options.quarterWeightMod;
            }
            else if (type == ModTypes.Tool)
            {
                return Settings.options.toolWeightMod;
            }
            else
            {
                return Settings.options.defaultWeightMod;
            }
        }

        public static void ModifyBodyHarvestWeight(GearItem gi)
        {
            if (Settings.options.infiniteCarry)
            {
                gi.m_BodyHarvest.m_QuarterBagWasteMultiplier = 0;
            }
            else
            {
                gi.m_BodyHarvest.m_QuarterBagWasteMultiplier = Settings.options.quarterWeightMod;
            }
        }

        public static void RestoreItemWeights()
        {
            foreach (KeyValuePair<string, GearItemData> item in itemDataList)
            {
                float original = originalWeights.GetValueOrDefault(item.Key);
                item.Value.m_BaseWeightKG = original;
            }
        }

        public static void ResetItemWeights()
        {
            foreach (KeyValuePair<string, GearItemData> item in itemDataList)
            {
                float original = originalWeights.GetValueOrDefault(item.Key);
                ModTypes type = itemDataType.GetValueOrDefault(item.Key);

                float weightModifier = GetWeightModifier(type);

                if (Settings.options.infiniteCarry)
                {
                    item.Value.m_BaseWeightKG = 0;
                }
                else
                {
                    item.Value.m_BaseWeightKG = original * weightModifier;
                }
            }

            foreach(GearItemObject gearItem in GameManager.GetInventoryComponent().m_Items)
            {
                if (gearItem.m_GearItem.m_BodyHarvest)
                {
                    ModifyBodyHarvestWeight(gearItem.m_GearItem);
                }
                else
                {
                    gearItem.m_GearItem.WeightKG = gearItem.m_GearItem.m_GearItemData.m_BaseWeightKG;
                }
            }
        }
    }
}
