using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace slotJupiter
{
    public static class CharacterFactory
    {
        public static RuntimeCharacter Create(string _name, Dictionary<string, CharacterData> database, int characterIndex)
        {
            CharacterData _baseData = database[_name];

            RuntimeCharacter runtimeCharacter = new()
            {
                characterData = _baseData,
                PT = new()
            };

            if (runtimeCharacter.characterData == null) Debug.LogError("Character Data are NULL");

            runtimeCharacter.PT.Add(STATE.CHARACTER_STATE, ECHARACTER_STATE.ALIVE);
            runtimeCharacter.PT.Add(STATE.CHARACTER_INDEX, characterIndex);
            runtimeCharacter.PT.Add(ECHARACTER_STATUS.MAX_HP, runtimeCharacter.characterData.hp);
            runtimeCharacter.PT.Add(ECHARACTER_STATUS.HP, runtimeCharacter.characterData.hp);
            runtimeCharacter.PT.Add(ECHARACTER_STATUS.ATTACK, runtimeCharacter.characterData.atk);
            runtimeCharacter.PT.Add(ECHARACTER_STATUS.DEFENSE, runtimeCharacter.characterData.def);
            runtimeCharacter.PT.Add(ECHARACTER_STATUS.SPEED, runtimeCharacter.characterData.spd);

            return runtimeCharacter;
        }

    }
}
