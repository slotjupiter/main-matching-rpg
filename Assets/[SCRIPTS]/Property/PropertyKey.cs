using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    public enum STATE
    {
        GAME_STATE,
        CHARACTER_STATE,
        CHARACTER_INDEX,
        CARD_STATE
    }

    public enum EGAME_PROCESS
    {
        NONE,
        NORMAL,
        WAIT,
        WAIT_TO_COMPLETE,
        PROCESS_COMPLETE
    }

    public enum EGAME_EVENT
    {
        NONE,
        FLIPCARD_ONE,
        FLIPCARD_TWO,
        MATCHING
    }

    public enum ECHARACTER_STATE
    {
        ALIVE, DEAD
    }

    public enum ECARD_STATE
    {
        FLIP, FLIPPING, UNFLIP
    }

    public enum ECHARACTER_STATUS
    {
        MAX_HP,
        HP,
        ATTACK,
        DEFENSE,
        SPEED
    }

    public enum EABILITY_CONDITION
    {
        NONE, BEFORE_ENEMY, AFTER_ENEMY, MATCHING, FLIP, END_TURN
    }

    public enum EABILITY_EFFECT
    {
        NONE,
        DEAL_DAMAGE
    }

    public enum ETARGET
    {
        NONE, SELF, ENEMY
    }

    public enum ECHARACTER_SIDE
    {
        NONE, PLAYER, ENEMY
    }

    public enum ECHARACTER_CLASS
    {
        NORMAL, ELITE, BOSS
    }

    public enum ECARD_TYPE
    {
        NONE, ENEMY, ITEM, ABILITY
    }
}
