using System;
using UnityEngine;

namespace slotJupiter
{
    [Serializable]
    public class AbilityHolder
    {
        [Header("[ABILITY]")]
        public EABILITY_CONDITION condition;
        public ETARGET target;
        public EABILITY_EFFECT selectedAbility = EABILITY_EFFECT.NONE;
        [SerializeReference] public Ability ability = null;

        public string AbilityDescription()
        {
            string skillDesc;
            switch (selectedAbility)
            {
                case EABILITY_EFFECT.DEAL_DAMAGE:
                    skillDesc = $"Deal {ability.ValueText()} damage to an {TargetString()}";
                    return skillDesc;
                default:
                    return null;
            }
        }

        private string TargetString()
        {
            return target switch
            {
                ETARGET.SELF => "Self",
                ETARGET.ENEMY => "Enemy",
                _ => ""
            };
        }

#if UNITY_EDITOR
        public void UpdateProperties()
        {
            switch (selectedAbility)
            {
                case EABILITY_EFFECT.DEAL_DAMAGE:
                    if (ability == null || selectedAbility != ability.effectType)
                        ability = new AttackAbility();
                    break;
                default:
                    ability = null;
                    break;
            }
        }
#endif
    }
}
