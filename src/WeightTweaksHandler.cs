using UnityEngine;
using Il2Cpp;
using Il2CppInterop.Runtime.Attributes;

namespace WeightTweaks
{
    class WeightTweaksHandler : MonoBehaviour
    {
        private float originalWeightKG;
        public GearItem? item;

        public WeightTweaksHandler() { }

        public WeightTweaksHandler(System.IntPtr intPtr) : base(intPtr) { }

        [HideFromIl2Cpp]
        public void Init(GearItem gi)
        {
            this.item = gi;
            if (this.item)
            {
                this.originalWeightKG = this.item.m_GearItemData.m_BaseWeightKG;
            }
        }

        [HideFromIl2Cpp]
        public void ModifyWeight(float multiplier)
        {
            if (this.item != null)
            {
                this.item.m_GearItemData.m_BaseWeightKG = this.originalWeightKG * multiplier;
                this.item.WeightKG = this.originalWeightKG * multiplier;
            }
        }
    }
}
