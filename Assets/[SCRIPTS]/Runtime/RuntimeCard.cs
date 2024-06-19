using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    [Serializable]
    public class RuntimeCard : BaseRuntime
    {
        //Card Object.
        public Vector2Int cardPosition;
        public Card card;

        //Info.
        public string cardName;
        public ECARD_TYPE cardType;

        //Specific data on card.
        public RuntimeCharacter runtimeEnemy = null;
        public ItemData itemData = null;
        public AbilityData abilityData = null;
    }
}
