using System.IO;
using System.Reflection;
using ModSettings;
using System;

namespace WeightTweaks
{
    internal class WeightTweaksSettings : ModSettingsBase
    {
        internal readonly WeightTweaksOptions setOptions = new WeightTweaksOptions();

        [Section("Carry Capacity")]

        [Name("Infinite carry")]
        [Description("Toggling this on makes all items weightless.")]
        public bool infiniteCarry = false;

        [Name("Carry capacity to add (Kg)")]
        [Description("How many Kg will be added to your carry capacity.")]
        [Slider(0, 120)]
        public int carryKgAdd = 0;

        [Section("Item Weight Modifiers")]

        [Name("Clothing Modifier")]
        [Description("Increases or reduces the weight of clothing items (e.g. 0 makes them weightless, 0.5 makes them half as heavy, 1 is game default, 2 makes them twice as heavy.")]
        [Slider(0f, 2f, 1, NumberFormat = "{0:F2}")]
        public float clothingWeightMod = 1f;

        [Name("Worn Clothing Modifier")]
        [Description("Increases or reduces the weight of clothing items you are wearing (e.g. 0 makes them weightless, 0.25 is game default, 1 would make clothing weight the same either worn or not.")]
        [Slider(0f, 1f, NumberFormat = "{0:F2}")]
        public float clothingWornWeightMod = 0.25f;

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

        internal WeightTweaksSettings()
        {
            if (File.Exists(Path.Combine(WeightTweaks.modOptionsFolder, WeightTweaks.optionsFileName)))
            {
                string opts = File.ReadAllText(Path.Combine(WeightTweaks.modOptionsFolder, WeightTweaks.optionsFileName));
                setOptions = FastJson.Deserialize<WeightTweaksOptions>(opts);

                infiniteCarry = setOptions.infiniteCarry;
                carryKgAdd = setOptions.carryKgAdd;

                clothingWeightMod = setOptions.clothingWeightMod;
                clothingWornWeightMod = setOptions.clothingWornWeightMod;
                waterWeightMod = setOptions.waterWeightMod;
                foodWeightMod = setOptions.foodWeightMod;
                rifleWeightMod = setOptions.rifleWeightMod;
                quarterWeightMod = setOptions.quarterWeightMod;
                toolWeightMod = setOptions.toolWeightMod;
                defaultWeightMod = setOptions.defaultWeightMod;
            }

            RefreshFields();
        }

        protected override void OnConfirm()
        {
            setOptions.infiniteCarry = infiniteCarry;
            setOptions.carryKgAdd = carryKgAdd;

            setOptions.clothingWeightMod = (float)Math.Round(clothingWeightMod,2);
            setOptions.clothingWornWeightMod = (float)Math.Round(clothingWornWeightMod, 2);
            setOptions.waterWeightMod = (float)Math.Round(waterWeightMod, 2);
            setOptions.foodWeightMod = (float)Math.Round(foodWeightMod, 2);
            setOptions.rifleWeightMod = (float)Math.Round(rifleWeightMod, 2);
            setOptions.quarterWeightMod = (float)Math.Round(quarterWeightMod, 2);
            setOptions.toolWeightMod = (float)Math.Round(toolWeightMod, 2);
            setOptions.defaultWeightMod = (float)Math.Round(defaultWeightMod, 2);

            string jsonOpts = FastJson.Serialize(setOptions);

            File.WriteAllText(Path.Combine(WeightTweaks.modOptionsFolder, WeightTweaks.optionsFileName), jsonOpts);
        }

        protected override void OnChange(FieldInfo field, object oldVal, object newVal)
        {
            if (field.Name == nameof(infiniteCarry))
            {
                RefreshFields();
            }
        }

        internal void RefreshFields()
        {
            SetFieldVisible(nameof(carryKgAdd), !infiniteCarry);
            SetFieldVisible(nameof(clothingWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(clothingWornWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(waterWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(foodWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(rifleWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(quarterWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(toolWeightMod), !infiniteCarry);
            SetFieldVisible(nameof(defaultWeightMod), !infiniteCarry);
        }
    }
}
