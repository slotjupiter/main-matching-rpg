using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotJupiter
{
    public static class CardFactory
    {
        public static RuntimeCard Create(string _name)
        {
            ItemData _baseData = GameDatabase.base_ItemData[_name];

            RuntimeCard runtimeCard = new()
            {
                cardType = ECARD_TYPE.ITEM,
                itemData = _baseData,
                PT = new()
            };

            if (runtimeCard.itemData == null) Debug.LogError("Item Data are NULL");
            runtimeCard.PT.Add(STATE.CARD_STATE, ECARD_STATE.UNFLIP);

            return runtimeCard;
        }

        public static RuntimeCard Create(ref AbilityData _targetData)
        {
            RuntimeCard runtimeCard = new()
            {
                cardType = ECARD_TYPE.ABILITY,
                abilityData = _targetData,
                PT = new()
            };

            if (runtimeCard.abilityData == null) Debug.LogError("Ability Data are NULL");
            runtimeCard.PT.Add(STATE.CARD_STATE, ECARD_STATE.UNFLIP);

            return runtimeCard;
        }

        public static RuntimeCard Create(RuntimeCharacter _runtimeEnemy)
        {
            RuntimeCard runtimeCard = new()
            {
                cardType = ECARD_TYPE.ENEMY,
                runtimeEnemy = _runtimeEnemy,
                PT = new()
            };

            if (runtimeCard.runtimeEnemy == null) Debug.LogError("Runtime Enemy Data are NULL");
            runtimeCard.PT.Add(STATE.CARD_STATE, ECARD_STATE.UNFLIP);

            return runtimeCard;
        }

        public static Card CreateCardWidget(RuntimeCard _runtimeCard, Transform parent, Vector2Int _cardPos)
        {
            var newCardOBJ = Object.Instantiate(GameController.instance.boardManager.cardPrefab, parent.transform);
            _runtimeCard.card = newCardOBJ;
            _runtimeCard.cardPosition = _cardPos;
            newCardOBJ.Initialize(_runtimeCard);
            newCardOBJ.name = _runtimeCard.cardName;
            return newCardOBJ;
        }

        public static void SwapCardWidget(RuntimeCard _fromCard, RuntimeCard _toCard)
        {
            var pos1 = _fromCard.cardPosition;
            var pos2 = _toCard.cardPosition;

            var card1 = _fromCard;
            var card2 = _toCard;

            _fromCard = card2;
            _fromCard.cardPosition = pos1;

            _toCard = card1;
            _toCard.cardPosition = pos2;

            _fromCard.card.Initialize(_fromCard);
            _toCard.card.Initialize(_toCard);
        }
    }
}
