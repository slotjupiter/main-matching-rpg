using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    [CreateAssetMenu(fileName = "New Item", menuName = "[Game Data]/Item", order = 2)]
    public class ItemData : ScriptableObject
    {
        public DataInfo info;
        public AbilityData itemAbility;
    }
}
