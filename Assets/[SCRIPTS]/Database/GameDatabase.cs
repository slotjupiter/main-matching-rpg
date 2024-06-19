using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace slotJupiter
{
    public static class GameDatabase
    {
        public static Dictionary<string, CharacterData> base_PlayerData = new();
        public static Dictionary<string, CharacterData> base_EnemyData = new();
        public static Dictionary<string, ItemData> base_ItemData = new();
        public static bool databaseReady { get; private set; } = false;

        public static void Initialize()
        {
            base_PlayerData = Resources.LoadAll<CharacterData>("PlayerCharacters").ToDictionary(character => character.name);
            base_EnemyData = Resources.LoadAll<CharacterData>("Enemies").ToDictionary(enemy => enemy.name);
            base_ItemData = Resources.LoadAll<ItemData>("Items").ToDictionary(item => item.name);
            databaseReady = true;
        }
    }
}
