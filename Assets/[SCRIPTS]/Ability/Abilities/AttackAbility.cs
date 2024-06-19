using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace slotJupiter
{
    public class AttackAbility : Ability
    {
        public override EABILITY_EFFECT effectType => EABILITY_EFFECT.DEAL_DAMAGE;
        [InfoBox("PercentDamage")]
        public float value;

        public override IEnumerator Execute(RuntimeCharacter player, RuntimeCharacter target)
        {
            yield return null;
        }

        public override string ValueText()
        {
            return $"{value}%";
        }
    }
}

