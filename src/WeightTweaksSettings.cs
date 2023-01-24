using Il2Cpp;
using ModSettings;
using System.Reflection;

namespace WeightTweaks
{
    internal class WeightTweaksSettings : JsonModSettings
    {
        [Section("Carry Capacity")]

        [Name("Infinite carry")]
        [Description("Toggling this on makes all items weightless.")]
        public bool infiniteCarry = false;

        [Name("Carry capacity to add (Kg)")]
        [Description("How many Kg will be added to your carry capacity.")]
        [Slider(0, 120)]
        public int carryKgAdd = 0;

        [Section("Item Weight Modifiers")]

        [Name("Modify Item Weights")]
        [Description("Toggling this on enables you to modify item weights. Turning this off might help removing the stuttering in some systems.")]
        public bool modifyWeight = false;

        [Name("Clothing Modifier")]
        [Description("Increases or reduces the weight of clothing items (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float clothingWeightMod = 1f;

        [Name("Worn Clothing Modifier")]
        [Description("Increases or reduces the weight of clothing items you are wearing (e.g. 0 makes them weightless, 0.75 is game default, 1 would make clothing weight the same either worn or not.")]
        [Slider(0f, 1f, NumberFormat = "{0:F2}")]
        public float clothingWornWeightMod = 0.75f;

        [Name("Water Modifier")]
        [Description("Increases or reduces the weight of the water supply (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float waterWeightMod = 1f;

        [Name("Food Modifier")]
        [Description("Increases or reduces the weight of food items (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float foodWeightMod = 1f;

        [Name("Weapons Modifier")]
        [Description("Increases or reduces the weight of ranged weapon items: distress pistol, rifle and bow, and its ammo. (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float rifleWeightMod = 1f;

        [Name("Quarter Bags Modifier")]
        [Description("Increases or reduces the weight of quarter bags (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float quarterWeightMod = 1f;

        [Name("Tools Modifier")]
        [Description("Increases or reduces the weight of tools like knives, hatchets, torches, fishing tackle, etc. (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float toolWeightMod = 1f;

        [Name("General Item Modifier")]
        [Description("Increases or reduces the weight of items not listed before (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float defaultWeightMod = 1f;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            Encumber encumberComp = GameManager.GetEncumberComponent();

            if (encumberComp != null)
            {
                WeightTweaks.EncumberUpdate(encumberComp);
            }
        }

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            if (field.Name == nameof(modifyWeight))
            {
                RefreshFields();
            }
        }

        internal void RefreshFields()
        {
            SetFieldVisible(nameof(clothingWeightMod), modifyWeight);
            SetFieldVisible(nameof(clothingWornWeightMod), modifyWeight);
            SetFieldVisible(nameof(waterWeightMod), modifyWeight);
            SetFieldVisible(nameof(foodWeightMod), modifyWeight);
            SetFieldVisible(nameof(rifleWeightMod), modifyWeight);
            SetFieldVisible(nameof(quarterWeightMod), modifyWeight);
            SetFieldVisible(nameof(toolWeightMod), modifyWeight);
            SetFieldVisible(nameof(defaultWeightMod), modifyWeight);
        }
    }

    internal static class Settings
    {
        public static WeightTweaksSettings options;

        public static void OnLoad()
        {
            options = new WeightTweaksSettings();
            options.RefreshFields();
            options.AddToModSettings("Weight Tweaks");
        }
    }
}
