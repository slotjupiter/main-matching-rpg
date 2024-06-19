using System;
using UnityEngine;

namespace slotJupiter
{
    [Serializable]
    public class DataInfo
    {
        public Sprite Sprite;
        public string Name;
        [TextArea] public string Description;
    }
}
