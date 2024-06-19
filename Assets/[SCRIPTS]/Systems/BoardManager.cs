using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace slotJupiter
{
    [RequireComponent(typeof(BoardLayout))]
    public class BoardManager : MonoBehaviour
    {
        public Card cardPrefab;
        public BoardLayout boardLayout;
        public SerializedDictionary<Vector2Int, RuntimeCard> MainCardList = new();

        public IEnumerator CardAppear()
        {
            GameController.instance.GameProcess = EGAME_PROCESS.WAIT_TO_COMPLETE;
            foreach (var card in MainCardList)
            {
                yield return card.Value.card.Appear();
                if (card.Key == new Vector2Int(boardLayout.columns - 1, boardLayout.rows - 1))
                    yield return GameController.instance.OnGameProcess(EGAME_PROCESS.PROCESS_COMPLETE);
            }
        }

        public IEnumerator Initialize(int enemyAmount)
        {
            int cardAmount = boardLayout.columns * boardLayout.rows;
            if (cardAmount % 2 != 0)
            {
                Debug.LogError("Card amount is not an even number");
                yield return null;
            }
            yield return SetupCardsOnBoard(cardAmount, enemyAmount);
        }

        public IEnumerator SetupCardsOnBoard(int cardAmount, int enemyAmount)
        {
            int _enemyPairNeeded = enemyAmount * 2;
            int _abilityPairNeeded = (cardAmount - _enemyPairNeeded) / 2;
            int _itemsPairNeeded = _abilityPairNeeded / 2;

            Debug.Log($"Card Setup : Card Amount = {cardAmount}, Enemy = {_enemyPairNeeded}, Ability = {_abilityPairNeeded}, Item = {_itemsPairNeeded}");
           
            //Random item
            for (int i = 0; i < _itemsPairNeeded; i++)
            {
                var _randItem = Utils.RandomPickKeyFromDict(GameDatabase.base_ItemData);
                GameManager.instance.selectedItem.Add(GameDatabase.base_ItemData[_randItem]);
            }
            yield return new WaitUntil(() => GameManager.instance.selectedItem.Count == _itemsPairNeeded);

            List<RuntimeCard> _tempCardList = new();

            //Create Item
            foreach (var item in GameManager.instance.selectedItem)
            {
                _tempCardList.Add(CardFactory.Create(item.name));
                _tempCardList.Add(CardFactory.Create(item.name));
            }
            yield return new WaitUntil(() => _tempCardList.Count == _itemsPairNeeded * 2);

            //Create Enemy Card
            foreach (var enemy in GameManager.instance.RuntimeEnemies)
            {
                _tempCardList.Add(CardFactory.Create(enemy));
                _tempCardList.Add(CardFactory.Create(enemy));
            }

            //Create Ability Card
            for (int i = 0; i < _abilityPairNeeded; i++)
            {
                _tempCardList.Add(CardFactory.Create(ref GameManager.instance.RuntimePlayer.characterData.characterAbility));
            }

            Debug.Log($"Temp card count = {_tempCardList.Count}");

            yield return new WaitUntil(() => _tempCardList.Count == cardAmount);

            //*Create Card Prefabs.
            bool _createCard = true;
            Utils.ShuffleList(_tempCardList);

            int _rowsCount = 0;
            int _colCount = 0;
            for (int i = 0; i < _tempCardList.Count; i++)
            {
                CardFactory.CreateCardWidget(_tempCardList[i], transform, new Vector2Int(_colCount, _rowsCount));
                MainCardList.Add(new Vector2Int(_colCount, _rowsCount), _tempCardList[i]);

                if (_tempCardList[i] == _tempCardList.Last()) _createCard = false;

                if (_rowsCount > boardLayout.rows - 1)
                    _rowsCount = 0;
                if (_colCount >= boardLayout.columns - 1)
                {
                    _colCount = 0;
                    _rowsCount++;
                }
                else
                    _colCount++;
            }

            yield return new WaitUntil(() => !_createCard);
        }
    }
}
