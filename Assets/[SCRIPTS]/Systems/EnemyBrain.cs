using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    [Serializable]
    public class EnemyBrain
    {
        public List<RuntimeCard> enemiesList = new();

        public void EndturnAttack()
        {
            foreach(var enemy in enemiesList)
            {

            }
        }

    }
}
