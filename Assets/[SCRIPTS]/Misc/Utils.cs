using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace slotJupiter
{
    public static class Utils
    {
        public static List<T> ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (list[j], list[i]) = (list[i], list[j]);
            }
            return list;
        }

        public static T RandomPickFromList<T>(List<T> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException("Can't pick an item from an empty list");

            int _randIndex = UnityEngine.Random.Range(0, list.Count);
            return list[_randIndex];
        }

        public static T RandomPickKeyFromDict<T, TVal>(Dictionary<T, TVal> dictionary)
        {
            if (dictionary.Count == 0)
                throw new InvalidOperationException("Can't pick a key from an empty dict");

            List<T> keys = new(dictionary.Keys);
            int _randIndex = UnityEngine.Random.Range(0, keys.Count);
            return keys[_randIndex];
        }
    }
}
