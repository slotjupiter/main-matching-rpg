using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace slotJupiter
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "[Game Data]/Ability", order = 1)]
    public class AbilityData : ScriptableObject
    {
        [InfoBox("Use =[BEFORE]= =[AFTER]= =[MATCHING]= to replace ability desc.")]
        public DataInfo info;
        public AbilityHolder beforeAbility;
        public AbilityHolder afterAbility;
        public AbilityHolder matchingAbility;

        public string GetAbilityDescription()
        {
            string _fullDesc = info.Description;

            if (beforeAbility.condition != EABILITY_CONDITION.NONE)
            {
                string placeholderBefore = "=[BEFORE]=";
                _fullDesc = _fullDesc.Replace(placeholderBefore, beforeAbility.AbilityDescription());
                string colorholderBefore = "BEFORE";
                _fullDesc = _fullDesc.Replace(colorholderBefore, ColorLibrary.SetColorText(ColorLibrary.Red, colorholderBefore));
            }
            if (afterAbility.condition != EABILITY_CONDITION.NONE)
            {
                string placeholderAfter = "=[AFTER]=";
                _fullDesc = _fullDesc.Replace(placeholderAfter, afterAbility.AbilityDescription());
                string colorholderBefore = "AFTER";
                _fullDesc = _fullDesc.Replace(colorholderBefore, ColorLibrary.SetColorText(ColorLibrary.Red, colorholderBefore));
            }
            if (matchingAbility.condition != EABILITY_CONDITION.NONE)
            {
                string placeholderMatching = "=[MATCHING]=";
                _fullDesc = _fullDesc.Replace(placeholderMatching, matchingAbility.AbilityDescription());
                string colorholderBefore = "MATCHING";
                _fullDesc = _fullDesc.Replace(colorholderBefore, ColorLibrary.SetColorText(ColorLibrary.Green, colorholderBefore));
            }

            return _fullDesc;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            beforeAbility?.UpdateProperties();
            afterAbility?.UpdateProperties();
            matchingAbility?.UpdateProperties();
        }
#endif
    }
}
