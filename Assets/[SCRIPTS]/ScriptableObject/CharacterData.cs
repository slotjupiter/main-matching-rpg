using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    [CreateAssetMenu(fileName = "New Character", menuName = "[Game Data]/Character", order = 0)]
    public class CharacterData : ScriptableObject
    {
        //Common Info likes sprite, name and desc.
        public DataInfo info;
        public ECHARACTER_CLASS characterClass;

        //Starter Status
        [Range(1, 100)] public int hp = 100;
        [Range(1, 10)] public int atk = 1;
        [Range(1, 10)] public int def = 1;
        [Range(1, 10)] public int spd = 1;

        public AbilityData characterAbility;
    }
}
