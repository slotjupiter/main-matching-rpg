using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    public abstract class Ability
    {
        public abstract EABILITY_EFFECT effectType { get; }
        public abstract IEnumerator Execute(RuntimeCharacter player, RuntimeCharacter target);
        public abstract string ValueText();
    }

}
