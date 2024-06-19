using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}
